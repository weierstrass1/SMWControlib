using ImageTileCropper;
using SMWControlibBackend.Graphics;
using SMWControlibBackend.Graphics.Frames;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMWControlibControls.GraphicsControls
{
    public partial class FrameOptimizerDialog : Form
    {
        private int _max8x8Tiles;
        private int _maxExtChildrens;
        private float _heuristicWeight;
        private int _max8x8TilesDef;
        private int _maxExtChildrensDef;
        private int _heuristicWeightDef;
        private string baseName;
        private Bitmap[] bitmaps;
        private int _currentBP;
        Task t;
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        CancellationToken token;
        private bool cancelFrame;
        private List<Frame> newFrames;
        int mx, my;
        Dictionary<Int32, byte> pal;
        Stopwatch sw;
        ulong totaltime;
        private int currentBP 
        { 
            get => _currentBP; 
            set

            {
                if (value < bitmaps.Length)
                {
                    _currentBP = value;
                    using (Graphics g = Graphics.FromImage(frameShower.Image))
                    {
                        g.Clear(Color.FromArgb(0, 0, 0, 0));
                        g.DrawImage(bitmaps[currentBP], new Point(
                            (frameShower.Width - bitmaps[currentBP].Width) / 2,
                            (frameShower.Height - bitmaps[currentBP].Height) / 2));
                    }
                    frameShower.Refresh();
                }
            } 
        }
        public FrameOptimizerDialog(Bitmap[] bps, List<Frame> nF, int Mx, int My, Dictionary<Int32, byte> p, string name)
        {
            InitializeComponent();
            frameShower.Image = new Bitmap(frameShower.Width, frameShower.Height);
            mx = Mx;
            my = My;
            pal = p;
            baseName = name;
            newFrames = nF;
            bitmaps = bps;
            token = tokenSource.Token;
            max8x8Tiles.SelectedIndexChanged += max8x8TilesSelectedIndexChanged;
            maxExtraChildrens.SelectedIndexChanged += maxExtraChildrensSelectedIndexChanged;
            hweight.SelectedIndexChanged += hweightSelectedIndexChanged;
            currentBP = 0;
            cancelFrame = true;

            max8x8Tiles.SelectedIndex = 1;
            maxExtraChildrens.SelectedIndex = 14;
            hweight.SelectedIndex = 0;

            max8x8Tiles.Enabled = true;
            maxExtraChildrens.Enabled = true;
            hweight.Enabled = true;

            timer1.Tick += tick;
            run.Click += runClick;
            frameCounter.Text = "Frame: " + (currentBP + 1) + "/" + bitmaps.Length;
            totaltime = 0;
            sw = new Stopwatch();
            FormClosing += formClosing;
            cancel.Click += cancelClick;
            timer1.Stop();
            keepConfig.CheckedChanged += keepConfigCheckedChanged;
        }

        private void keepConfigCheckedChanged(object sender, EventArgs e)
        {
            if(keepConfig.Checked)
            {
                _max8x8TilesDef = max8x8Tiles.SelectedIndex;
                _maxExtChildrensDef = maxExtraChildrens.SelectedIndex;
                _heuristicWeightDef = hweight.SelectedIndex;
            }
        }

        private void cancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void formClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
                newFrames.Clear();
        }

        private void runClick(object sender, EventArgs e)
        {
            if(cancelFrame)
            {
                cancelFrame = false;
                run.Text = "Stop";

                max8x8Tiles.Enabled = false;
                maxExtraChildrens.Enabled = false;
                hweight.Enabled = false;

                sw.Stop();
                sw.Reset();
                sw.Start();
                t = Task.Run(() => addFrame(bitmaps[currentBP]), token);
                timer1.Start();
            }
            else
            {
                cancelFrame = true;
                run.Text = "Continue";

                max8x8Tiles.Enabled = true;
                maxExtraChildrens.Enabled = true;
                hweight.Enabled = true;

                tokenSource.Cancel();
                sw.Stop();
                sw.Reset();
                sw.Start();
                timer1.Stop();
            }
        }

        private void tick(object sender, EventArgs e)
        {
            if (t.IsCompleted)
            {
                max8x8Tiles.SelectedIndex = _max8x8TilesDef;
                maxExtraChildrens.SelectedIndex = _maxExtChildrensDef;
                hweight.SelectedIndex = _heuristicWeightDef;
                sw.Stop();
                totaltime += (ulong)sw.ElapsedMilliseconds;

                if (currentBP >= bitmaps.Length - 1)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    currentBP++;
                    sw.Reset();
                    sw.Start();

                    t = Task.Run(() => addFrame(bitmaps[currentBP]), token);
                    frameCounter.Text = "Frame: " + (currentBP + 1) + "/" + bitmaps.Length + " Estimated: " + (((totaltime * (ulong)(bitmaps.Length - currentBP)) / (ulong)currentBP) / 1000) + " secs";
                }
            }
        }

        int[] _max8x8TilesOptions = { int.MaxValue,
            16,15,14, 13,
            12,11,10, 9,
            8, 7, 6, 5,
            4, 3, 2, 1,
            0 };
        private void max8x8TilesSelectedIndexChanged(object sender, EventArgs e)
        {
            _max8x8Tiles = _max8x8TilesOptions[max8x8Tiles.SelectedIndex];
            TileOptimizer.Node.SmallSizeTileLimit = _max8x8Tiles;
            if (keepConfig.Checked)
                _max8x8TilesDef = max8x8Tiles.SelectedIndex;
        }
        int[] _maxExtChildrensOptions = { 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
        private void maxExtraChildrensSelectedIndexChanged(object sender, EventArgs e)
        {
            _maxExtChildrens = _maxExtChildrensOptions[maxExtraChildrens.SelectedIndex];
            CropNode.MaxAddedPerCycle = _maxExtChildrens;
            if (keepConfig.Checked)
                _maxExtChildrensDef = maxExtraChildrens.SelectedIndex;
        }
        float[] _heuristicWeightOptions = { 1, 1.1f, 1.2f, 1.3f, 1.4f, 1.5f, 1.6f, 1.7f, 1.8f, 1.9f, 2 };
        private void hweightSelectedIndexChanged(object sender, EventArgs e)
        {
            _heuristicWeight = _heuristicWeightOptions[hweight.SelectedIndex];
            CropNode.HeuristicWeight = _heuristicWeight;
            if (keepConfig.Checked)
                _heuristicWeightDef = hweight.SelectedIndex;
        }
        private void addFrame(Bitmap b)
        {
            newFrames.Add(
                        ImageProcessor.FromSpriteSheetToFrames(b, mx, my, pal, baseName+currentBP, DynamicSpriteSizeDialog.DynSize, ColorPalette.SelectedPalette));

        }
        public static new DialogResult Show(IWin32Window Owner, Bitmap[] bps, List<Frame> nF, int Mx, int My, Dictionary<Int32, byte> p, string name)
        {
            FrameOptimizerDialog fod = new FrameOptimizerDialog(bps, nF, Mx, My, p, name);

            return fod.ShowDialog(Owner);
        }
    }
}
