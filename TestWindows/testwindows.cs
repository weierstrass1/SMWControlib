using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SMWControlibBackend;
using SMWControlibBackend.Graphics;
using SMWControlibBackend.Graphics.Frames;
using SMWControlibControls.GraphicsControls;

namespace TestWindows
{
    public partial class testwindows : Form
    {
        string projectPath = null;
        public testwindows()
        {
            InitializeComponent();
            gfxButton1.Target = spriteGFXBox1;
            gfxButton2.Target = spriteGFXBox1;
            gfxButton3.Target = spriteGFXBox2;
            gfxButton4.Target = spriteGFXBox2;
            spriteGFXBox1.SelectionChanged += selectionChanged;
            spriteGFXBox2.SelectionChanged += selectionChanged;
            paletteButton1.target = paletteBox1;
            frameCreator1.SelectionChanged += frameCreator1_SelectionChanged;
            spriteGFXBox1.GraphicsLoaded += graphicsLoaded;
            spriteGFXBox2.GraphicsLoaded += graphicsLoaded;
            tabControl1.SelectedIndexChanged += selectedIndexChanged;
            animationEditor1.AddClick += addClick;
            animationEditor1.AnimationChanged += animationChanged;
            resizeableSpriteGridController1.MidChanged += midChanged;
            animationPlayer1.TimeChanged += playerTimeChanged;
            animationCreator1.SelectionChanged += animationCreatorSelectionChanged;
            interactionMenu1.FrameSelectionChanged += interactionMenuFrameSelectionChanged;
            interactionMenu1.HitboxSelectionChanged += interactionMenu1HitboxSelectionChanged;
            interactionMenu1.InteractionPointSelectionChanged += interactionMenuInteractionPointSelectionChanged;
            interactionMenu1.OptionChanged += interactionMenuOptionChanged;
            interactionMenu1.BorderColorChanged += interactionMenuBorderColorChanged;
            interactionMenu1.FillColorChanged += interactionMenuFillColorChanged;
            interactionMenu1.IPColorChanged += interactionMenuIPColorChanged;
            interactionMenu1.ZoomChanged += interactionMenuZoomChanged;
            interactionMenu1.CellSizeChanged += interactionMenuCellSizeChanged;
            extratTo.Click += extratToClick;
            save.Click += saveClick;
            saveAs.Click += saveAsClick;
            loadProj.Click += loadProjClick;
            try
            {
                codeEditorController1.CodeEditor.CanUndoRedo = false;
                StringBuilder sb = new StringBuilder();
                sb.Append(File.ReadAllText(@".\ASM\Main.asm"));
                sb.Append(File.ReadAllText(@".\ASM\GraphicRoutine.asm"));
                codeEditorController1.CodeEditor.AppendText(sb.ToString());
                codeEditorController1.CodeEditor.CanUndoRedo = true;
            }
            catch
            {
            }
        }

        private void saveAsClick(object sender, EventArgs e)
        {
            saveFile.Filter = "Dyzen Project File (*.dyz)|*.dyz";
            saveFile.DefaultExt = "dyz";
            if (projectPath != null)
            {
                saveFile.FileName = Path.GetFileNameWithoutExtension(projectPath);
            }
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                projectPath = saveFile.FileName;
            }
            else
            {
                return;
            }
            if (Path.GetFileNameWithoutExtension(projectPath) == "")
                return;
            if (File.Exists(projectPath)) File.Delete(projectPath);
            ProjectContainer pc = new ProjectContainer();
            pc.GetAttributes(frameCreator1.Frames, animationCreator1.Animations,
                codeEditorController1.CodeEditor.Text, spriteGFXBox1.GetGFX(), spriteGFXBox2.GetGFX());
            pc.Serialize(projectPath);
            MessageBox.Show("Project " + Path.GetFileNameWithoutExtension(projectPath) + " Saved.",
                "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void loadProjClick(object sender, EventArgs e)
        {
            openFile.Multiselect = false;
            openFile.Filter = "Dyzen Project File (*.dyz)|*.dyz";
            openFile.CheckFileExists = true;
            openFile.CheckPathExists = true;
            if (projectPath != null)
            {
                openFile.FileName = Path.GetFileNameWithoutExtension(projectPath);
            }
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                projectPath = openFile.FileName;
            }
            else
            {
                return;
            }
            if (Path.GetFileNameWithoutExtension(projectPath) == "") return;
            ProjectContainer pc = ProjectContainer.Deserialize(projectPath);
            pc.GlobalPalette.ToGlobalColorPalette();
            File.WriteAllBytes("tmp.bin", pc.SP12);
            spriteGFXBox1.LoadGFX("tmp.bin", 0);
            File.Delete("tmp.bin");
            File.WriteAllBytes("tmp.bin", pc.SP34);
            spriteGFXBox2.LoadGFX("tmp.bin", 0);
            File.Delete("tmp.bin");
            if (pc.Frames != null && pc.Frames.Length > 0)
            {
                resizeableSpriteGridController1.MidX = pc.Frames[0].MidX + 136;
                resizeableSpriteGridController1.MidY = pc.Frames[0].MidY + 104;
            }
            frameCreator1.LoadProjectFrames(pc.GetFrames(spriteGFXBox1.Tiles16,
            spriteGFXBox2.Tiles16, spriteGFXBox1.Tiles8, spriteGFXBox2.Tiles8));
            animationCreator1.LoadProject(pc.GetAnimations(frameCreator1.Frames));
            codeEditorController1.CodeEditor.ClearAll();
            codeEditorController1.CodeEditor.AppendText(pc.Code);
            MessageBox.Show("Project " + Path.GetFileNameWithoutExtension(projectPath) + " Loaded.",
                "Load Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void saveClick(object sender, EventArgs e)
        {
            if (projectPath == null)
            {
                saveFile.Filter = "Dyzen Project File (*.dyz)|*.dyz";
                saveFile.DefaultExt = "dyz";
                if (projectPath != null) 
                {
                    saveFile.FileName = Path.GetFileNameWithoutExtension(projectPath);
                }
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    projectPath = saveFile.FileName;
                }
                else
                {
                    return;
                }
            }
            if (Path.GetFileNameWithoutExtension(projectPath) == "")
                return;
            if (File.Exists(projectPath)) File.Delete(projectPath);
            ProjectContainer pc = new ProjectContainer();
            pc.GetAttributes(frameCreator1.Frames, animationCreator1.Animations,
                codeEditorController1.CodeEditor.Text, spriteGFXBox1.GetGFX(), spriteGFXBox2.GetGFX());
            pc.Serialize(projectPath);
            MessageBox.Show("Project " + Path.GetFileNameWithoutExtension(projectPath) + " Saved.",
                "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void extratToClick(object sender, EventArgs e)
        {
            string s = codeEditorController1.CodeEditor.Text;

            s = s.Replace("dw @fls.",
                Frame.GetFramesLengthFromFrameList(frameCreator1.Frames, true, true));
            s = s.Replace("dw @ffps.",
                Frame.GetFramesFlippersFromFrameList(frameCreator1.Frames, true, true));
            s = s.Replace("dw @fsp.",
                Frame.GetFramesStartsFromFrameList(frameCreator1.Frames, true, true));
            s = s.Replace("dw @fep.",
                Frame.GetFramesEndsFromFrameList(frameCreator1.Frames, true, true));
            s = s.Replace("db @tiles.",
                Frame.GetTilesCodesFromFrameList(frameCreator1.Frames, true, true));
            s = s.Replace("db @props.",
                Frame.GetTilesPropertiesFromFrameList(frameCreator1.Frames, true, true));
            s = s.Replace("db @xdisps.",
                Frame.GetTilesXDispFromFrameList(frameCreator1.Frames, true, true));
            s = s.Replace("db @ydisps.",
                Frame.GetTilesYDispFromFrameList(frameCreator1.Frames, true, true));
            s = s.Replace("db @sizes.",
                Frame.GetTilesSizesFromFrameList(frameCreator1.Frames, true, true));


            File.WriteAllText("output.txt", s);
        }

        private void interactionMenuCellSizeChanged(int obj)
        {
            interactionGrid1.CellSize = obj;
        }

        private void interactionMenuZoomChanged(int obj)
        {
            interactionGrid1.Zoom = obj;
        }

        private void interactionMenuIPColorChanged(System.Drawing.Color obj)
        {
            interactionGrid1.SelectedInteractionPointColor = obj;
        }

        private void interactionMenuFillColorChanged(System.Drawing.Color obj)
        {
            interactionGrid1.SelectedHitboxFillColor = obj;
        }

        private void interactionMenuBorderColorChanged(System.Drawing.Color obj)
        {
            interactionGrid1.SelectedHitboxBorderColor = obj;
        }

        private void interactionMenuOptionChanged(bool obj)
        {
            interactionGrid1.SelectingHitbox = obj;
        }

        private void interactionMenuInteractionPointSelectionChanged()
        {
            interactionGrid1.SelectedInteractionPoint = interactionMenu1.SelectedInteractionPoint;
        }

        private void interactionMenu1HitboxSelectionChanged()
        {
            interactionGrid1.SelectedHitbox = interactionMenu1.SelectedHitBox;
        }

        private void interactionMenuFrameSelectionChanged()
        {
            interactionGrid1.SelectedFrame = interactionMenu1.SelectedFrame;
        }

        private void animationCreatorSelectionChanged()
        {
            animationEditor1.Animation = animationCreator1.SelectedAnimation;
            animationPlayer1.Reset();
        }

        private void playerTimeChanged(int arg1, int arg2)
        {
            animationEditor1.SetCurrentFrameAndTime(arg1, arg2);
        }

        private void midChanged()
        {
            frameCreator1.ChangeMid(resizeableSpriteGridController1.MidX,
                resizeableSpriteGridController1.MidY);
        }

        private void animationChanged()
        {
            animationPlayer1.Animation = animationEditor1.Animation;
        }

        private void addClick(AnimationEditor obj)
        {
            animationEditor1.Selection = frameSelector1.GetSelection();
        }

        private void selectedIndexChanged(object sender, System.EventArgs e)
        {
            frameSelector1.Frames = frameCreator1.Frames;
            frameSelector1.BuildTable();
            animationEditor1.Animation = animationEditor1.Animation;
            if (tabControl1.SelectedIndex == 2)
            {
                interactionMenu1.UpdateFrameList(frameCreator1.Frames);
            }
        }

        private void graphicsLoaded()
        {
            resizeableSpriteGridController1.ReDraw();
        }

        private void frameCreator1_SelectionChanged()
        {
            if (frameCreator1.SelectedFrame == null)
                resizeableSpriteGridController1.Tiles = null;
            else
                resizeableSpriteGridController1.Tiles = frameCreator1.SelectedFrame.Tiles;
        }

        private void selectionChanged(object sender)
        {
            SpriteGFXBox gb = (SpriteGFXBox)sender;
            resizeableSpriteGridController1.NewTiles =
                gb.GetBitmapsFromSelectedTiles(false, false, TilePriority.AboveAllLayersP0);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData==Keys.Delete)
            {
                resizeableSpriteGridController1.DeleteSelection();
            }
            if (keyData == Keys.A)
            {
                resizeableSpriteGridController1.MoveLeft();
            }
            if (keyData == Keys.W)
            {
                resizeableSpriteGridController1.MoveUp();
            }
            if (keyData == Keys.S)
            {
                resizeableSpriteGridController1.MoveDown();
            }
            if (keyData == Keys.D)
            {
                resizeableSpriteGridController1.MoveRight();
            }
            if (keyData == (Keys.Control | Keys.Z)) 
            {
                if(tabControl1.SelectedIndex == 3)
                {
                    codeEditorController1.CodeEditor.SuperSnescriptUndo();
                }
            }
            if (keyData == (Keys.Control | Keys.Space))
            {
                if (tabControl1.SelectedIndex == 3)
                {
                    codeEditorController1.CodeEditor.OpenTab();
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
