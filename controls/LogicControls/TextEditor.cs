using AutocompleteMenuNS;
using ScintillaNET;
using SMWControlibBackend.Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMWControlibControls.LogicControls
{
    public partial class TextEditor : Scintilla
    {
        private UndoRedoDynamicArray<UndoRedoStruct> undoRedoList;
        private Code code;
        private AutocompleteMenu autocom;
        public bool CanUndoRedo = false;
        public event Action<Dictionary<int, List<Error>>> ErrorsAdded
        {
            add
            {
                code.ErrorsAdded += value;
            }
            remove
            {
                code.ErrorsAdded -= value;
            }
        }
        public Group[] Groups
        {
            get
            {
                return code.Groups;
            }
        }

        private const int bookmarkMargin = 1; // Conventionally the symbol margin
        private const int bookmarkMarker = 3;
        public StylesContainer StylesContainer { get; private set; }
        public TextEditor()
        {
            InitializeComponent();
            undoRedoList = new UndoRedoDynamicArray<UndoRedoStruct>(20, 20);
            CaretForeColor = Color.White;

            try
            {
                StylesContainer = StylesContainer.Deserialize(@"Settings/ScriptnesStyles.set");
                StylesContainer.MarginBackColorRed = 96;
                StylesContainer.MarginBackColorGreen = 96;
                StylesContainer.MarginBackColorBlue = 112;


                Styles[Style.Default].Font = StylesContainer.Font;
                Styles[Style.Default].ForeColor =
                    Color.FromArgb(StylesContainer.ForeColorRed[0],
                    StylesContainer.ForeColorGreen[0],
                    StylesContainer.ForeColorBlue[0]);
                Styles[Style.Default].Bold = StylesContainer.Bold;
                Styles[Style.Default].BackColor =
                    Color.FromArgb(StylesContainer.BackColorRed,
                    StylesContainer.BackColorGreen,
                    StylesContainer.BackColorBlue);
                Styles[Style.Default].Size = StylesContainer.Size;
                StyleClearAll();

                for (int i = 0; i < 256; i++)
                {
                    Styles[i].ForeColor =
                        Color.FromArgb(StylesContainer.ForeColorRed[i],
                        StylesContainer.ForeColorGreen[i],
                        StylesContainer.ForeColorBlue[i]);
                    Styles[i].BackColor =
                        Color.FromArgb(StylesContainer.BackColorRed,
                        StylesContainer.BackColorGreen,
                        StylesContainer.BackColorBlue);
                }
                Styles[255].BackColor =
                    Color.FromArgb(StylesContainer.MarginBackColorRed,
                    StylesContainer.MarginBackColorGreen,
                    StylesContainer.MarginBackColorBlue);
                StylesContainer.Serialize(@"Settings/ScriptnesStyles.set");

                code = new Code(@"CSVs\Syntax\args.csv", @"CSVs\Syntax\commands.csv",
                    @"CSVs\Syntax\groups.csv");
                code.ImportDefines();
            }
            catch (Exception)
            {
            }
            autocom = new AutocompleteMenu
            {
                SearchPattern = @"\!",
                TargetControlWrapper = new ScintillaWrapper(this),
                AutoPopup = true,
                LeftPadding = 0,
            };

            CanUndoRedo = true;

            BeforeInsert += beforeInsert;
            Insert += insert;

            BeforeDelete += beforeDelete;
            Delete += delete;

            CharAdded += charAdded;
            autocom.Selected += selected;
            ClearCmdKey(Keys.Control | Keys.Z);
            ClearCmdKey(Keys.Control | Keys.Y);
            ClearCmdKey(Keys.Control | Keys.Q);
            ClearCmdKey(Keys.Control | Keys.W);
            ClearCmdKey(Keys.Control | Keys.E);
            ClearCmdKey(Keys.Control | Keys.R);
            ClearCmdKey(Keys.Control | Keys.T);
            ClearCmdKey(Keys.Control | Keys.Y);
            ClearCmdKey(Keys.Control | Keys.U);
            ClearCmdKey(Keys.Control | Keys.I);
            ClearCmdKey(Keys.Control | Keys.O);
            ClearCmdKey(Keys.Control | Keys.P);
            ClearCmdKey(Keys.Control | Keys.A);
            ClearCmdKey(Keys.Control | Keys.S);
            ClearCmdKey(Keys.Control | Keys.D);
            ClearCmdKey(Keys.Control | Keys.F);
            ClearCmdKey(Keys.Control | Keys.G);
            ClearCmdKey(Keys.Control | Keys.H);
            ClearCmdKey(Keys.Control | Keys.J);
            ClearCmdKey(Keys.Control | Keys.K);
            ClearCmdKey(Keys.Control | Keys.B);
            ClearCmdKey(Keys.Control | Keys.N);
            Margin margin = Margins[bookmarkMargin];
            margin.Width = 16;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = Marker.MaskAll;
            margin.Cursor = MarginCursor.Arrow;
            Margins[0].Type = MarginType.RightText;
            Margins[0].Width = 35;

            /*StylesContainer sc = new StylesContainer
            {
                Font = font,
                Size = size,
                BackColorRed = backColor.R,
                BackColorGreen = backColor.G,
                BackColorBlue = backColor.B,
                Bold = bold
            };


            sc.ForeColorRed = new int[256];
            sc.ForeColorGreen = new int[256];
            sc.ForeColorBlue = new int[256];
            for (int r = 0; r < 256; r++)
            {
                sc.ForeColorRed[r] = Styles[r].ForeColor.R;
                sc.ForeColorGreen[r] = Styles[r].ForeColor.G;
                sc.ForeColorBlue[r] = Styles[r].ForeColor.B;
            }
            sc.Serialize(@"Settings/ScriptnesStyles.set");*/


            Marker marker = Markers[bookmarkMarker];
            marker.Symbol = MarkerSymbol.SmallRect;
            marker.SetBackColor(Color.FromArgb(105, 75, 224));
            marker.SetForeColor(Color.FromArgb(70, 50, 150));
            TextChanged += textChanged;
            
        } 

        private int maxLineNumberCharLength = 1;
        private void textChanged(object sender, EventArgs e)
        {
            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxline = Lines.Count.ToString().Length;
            if (maxline == maxLineNumberCharLength)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            Margins[0].Width = TextWidth(Style.LineNumber, new string('9', maxline + 1)) + padding;
            maxLineNumberCharLength = maxline;
        }

        private void updateLineNumbers(int startingAtLine)
        {
            // Starting at the specified line index, update each
            // subsequent line margin text with a hex line number.
            for (int i = startingAtLine; i < Lines.Count; i++)
            {
                Lines[i].MarginStyle = 255;
                Lines[i].MarginText = "" + i;
            }
        }

        private void delete(object sender, ModificationEventArgs e)
        {
            if (e.LinesAdded != 0)
                updateLineNumbers(LineFromPosition(e.Position));
            int deltaLines = curLinesLen - Lines.Count;
            int lind = LineFromPosition(e.Position);
            Line curL = Lines[lind];
            string line = curL.Text;
            int lineInd = curL.Position;
            int linel;
            int lineN = LineFromPosition(lineInd);

            code.ClearFlags();
            code.DeleteLinesAt(lineN, deltaLines);

            highlightLine(line, lind);

            List<int> anLines = code.EndAnalysis();

            foreach (int tl in anLines)
            {
                curL = Lines[tl];
                line = curL.Text;
                lineInd = curL.Position;
                linel = line.Length;
                highlightLine(line, tl);
            }
        }

        public void SuperSnescriptUndo()
        {
            UndoRedoStruct e = undoRedoList.Undo();
            if (e == default) return;
            CanUndoRedo = false;
            if (e.UndoRedoAction == UndoRedoAction.Delete)
            {
                InsertText(e.Position, e.Text);
                CurrentPosition = e.Position + e.Text.Length;
                SelectionStart = CurrentPosition;
                SelectionEnd = SelectionStart;
            }
            else if(e.UndoRedoAction == UndoRedoAction.Insert)
            {
                DeleteRange(e.Position, e.Text.Length);
                CurrentPosition = e.Position;
                SelectionStart = CurrentPosition;
                SelectionEnd = SelectionStart;
            }
            CanUndoRedo = true;
        }

        private void selected(object sender, SelectedEventArgs e)
        {
            DeleteRange(adpos, curpos - adpos);
            adding = false;
        }

        public void OpenTab()
        {
            if (code.AutoCompleteTexts == null) return;

            List<AutocompleteItem> items = new List<AutocompleteItem>();

            foreach (string def in code.AutoCompleteTexts)
            {
                items.Add(new SnippetAutocompleteItem(def));
            }
            autocom.SetAutocompleteItems(items);
            autocom.CaptureFocus = true;
        }

        StringBuilder sb = new StringBuilder();
        bool adding = false;
        int adpos = 0;
        int curpos = 0;

        private void charAdded(object sender, CharAddedEventArgs e)
        {
            if(e.Char=='!')
            {
                sb.Clear();
                sb.Append(@"^\!");
                adding = true;
                adpos = CurrentPosition - 1;
            }
            else if(adding)
            {
                if (e.Char == '\n')
                {
                    adding = false;
                }
                sb.Append("" + (char)e.Char);
            }
            curpos = CurrentPosition;
            autocom.SearchPattern = sb.ToString();
            List<AutocompleteItem> items = new List<AutocompleteItem>();

            List<string> s = code.FilterAutocompleteWords(autocom.SearchPattern);

            foreach (string def in s)
            {
                items.Add(new SnippetAutocompleteItem(def));
            }
            autocom.SetAutocompleteItems(items);
            autocom.CaptureFocus = false;
            autocom.TargetControlWrapper.TargetControl.Location = new Point(0, 0);
            autocom.Show(this, false);
        }

        int curLinesLen = 0;
        private void beforeDelete(object sender, BeforeModificationEventArgs e)
        {
            if(CanUndoRedo)
            {
                UndoRedoStruct ne = new UndoRedoStruct(e, UndoRedoAction.Delete);
                undoRedoList.Add(ne);
            }
            if (adding && e.Text.Length == 1)
            {
                if (sb.Length > 3)
                {
                    sb.Remove(sb.Length - 1, 1);
                    autocom.SearchPattern = sb.ToString();
                    List<AutocompleteItem> items = new List<AutocompleteItem>();

                    List<string> s = code.FilterAutocompleteWords(autocom.SearchPattern);

                    foreach (string def in s)
                    {
                        items.Add(new SnippetAutocompleteItem(def));
                    }
                    curpos = CurrentPosition;
                    autocom.SetAutocompleteItems(items);
                    autocom.CaptureFocus = false;
                    autocom.Show(this, true);
                }
                else
                {
                    autocom.Close();
                }
            }
            curLinesLen = Lines.Count;
        }

        private void insert(object sender, ModificationEventArgs e)
        {
            if (e.LinesAdded != 0)
                updateLineNumbers(LineFromPosition(e.Position));
            string[] newLines = e.Text.Replace("\r", "").Split('\n');
            int linePos = LineFromPosition(e.Position);


            int endL = Lines.Count - 1;
            if (endL < 0) return;

            Line curL;
            string line;

            for (int i = linePos; i < linePos + newLines.Length; i++)
            {
                curL = Lines[i];
                line = curL.Text;
                highlightLine(line, i);
            }

            List<int> anLines = code.EndAnalysis();

            foreach (int tl in anLines)
            {
                curL = Lines[tl];
                line = curL.Text;
                highlightLine(line, tl);
            }
        }

        private void beforeInsert(object sender, BeforeModificationEventArgs e)
        {
            if (CanUndoRedo)
            {
                UndoRedoStruct ne = new UndoRedoStruct(e, UndoRedoAction.Insert);
                undoRedoList.Add(ne);
            }
            int lineIndex = LineFromPosition(e.Position);
            Line Curl = Lines[lineIndex];
            int linePos = Curl.Position;
            code.ClearFlags();
            code.AddLinesAt(lineIndex, linePos, e.Position, e.Text);
        }

        private void highlightLine(string line, int lineInd)
        {
            List<CodePointer> pointers = code.GetPointersFromLine(lineInd, line);
            paintPointers(lineInd, pointers);
        }

        private void paintPointers(int ind, List<CodePointer> pointers)
        {
            try
            {
                int st = Lines[ind].Position;

                foreach (CodePointer pointer in pointers)
                {
                    StartStyling(st + pointer.Start);
                    SetStyling(pointer.Code.Length, pointer.Group.Style);
                }
            }
            catch
            {
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
