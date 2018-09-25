using AutocompleteMenuNS;
using ScintillaNET;
using SMWControlibBackend.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SMWControlibControls.LogicControls
{
    public partial class TextEditor : Scintilla
    {
        private UndoRedoDynamicArray<UndoRedoStruct> undoRedoList;
        private Code code;
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

        private const int bookmarkMargin = 1; // Conventionally the symbol margin
        private const int bookmarkMarker = 3;

        AutocompleteMenu autocom;
        public TextEditor()
        {
            InitializeComponent();
            undoRedoList = new UndoRedoDynamicArray<UndoRedoStruct>(20, 20);
            CaretForeColor = Color.White;

            StylesContainer sc = StylesContainer.Deserialize(@"Settings/ScriptnesStyles.set");
            sc.MarginBackColorRed = 96;
            sc.MarginBackColorGreen = 96;
            sc.MarginBackColorBlue = 112;


            Styles[Style.Default].Font = sc.Font;
            Styles[Style.Default].ForeColor = 
                Color.FromArgb(sc.ForeColorRed[0], 
                sc.ForeColorGreen[0],
                sc.ForeColorBlue[0]);
            Styles[Style.Default].Bold = sc.Bold;
            Styles[Style.Default].BackColor =
                Color.FromArgb(sc.BackColorRed,
                sc.BackColorGreen,
                sc.BackColorBlue);
            Styles[Style.Default].Size = sc.Size;
            StyleClearAll();

            for (int i = 0; i < 256; i++)
            {
                Styles[i].ForeColor =
                    Color.FromArgb(sc.ForeColorRed[i],
                    sc.ForeColorGreen[i],
                    sc.ForeColorBlue[i]);
                Styles[i].BackColor =
                    Color.FromArgb(sc.BackColorRed,
                    sc.BackColorGreen,
                    sc.BackColorBlue);
            }
            Styles[255].BackColor =
                Color.FromArgb(sc.MarginBackColorRed,
                sc.MarginBackColorGreen,
                sc.MarginBackColorBlue);
            sc.Serialize(@"Settings/ScriptnesStyles.set");

            code = new Code(@"CSVs\Syntax\args.csv", @"CSVs\Syntax\commands.csv",
    @"CSVs\Syntax\groups.csv");
            code.ImportDefines(@".\ASM\Defines.asm");
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
            Line curL = Lines[CurrentLine];
            string line = curL.Text;
            int lineInd = curL.Position;
            int linel = line.Length;
            int lineN = LineFromPosition(lineInd);

            code.ClearFlags();
            code.DeleteLinesAt(lineN, lineInd, e.Position, deltaLines, line);

            highlightLine(line, CurrentLine);

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

        private void deleteBookMark(int Line)
        {
            Line line = Lines[Line];
            line.MarkerDelete(bookmarkMarker);
        }

        private void addBookMark(int Line)
        {
            Line line = Lines[Line];
            line.MarkerAdd(bookmarkMarker);
        }

        public void SuperSnescriptUndo()
        {
            UndoRedoStruct e = undoRedoList.Undo();
            if (e == default(UndoRedoStruct)) return;
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
            int pos = Lines[linePos].Position;


            int endL = Lines.Count - 1;
            if (endL < 0) return;

            Line curL;
            string line;
            int lineInd;
            int linel;

            for (int i = linePos; i < linePos + newLines.Length; i++)
            {
                curL = Lines[i];
                line = curL.Text;
                lineInd = curL.Position;
                linel = line.Length;
                highlightLine(line, i);
            }

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
