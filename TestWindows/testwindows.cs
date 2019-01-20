using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SMWControlibBackend;
using SMWControlibBackend.Graphics;
using SMWControlibBackend.Graphics.Frames;
using SMWControlibBackend.Interaction;
using SMWControlibControls.GraphicsControls;
using SMWControlibControls.LogicControls;

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
            try
            {
                spriteGFXBox1.LoadGFX("./Resources/GFX00.bin", 0);
                spriteGFXBox1.LoadGFX("./Resources/GFX01.bin", 64);
                spriteGFXBox2.LoadGFX("./Resources/GFX13.bin", 0);
                spriteGFXBox2.LoadGFX("./Resources/GFX09.bin", 64);
            }
            catch
            {
            }
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
            interactionMenu1.AddText += interactionMenu1AddText;
            interactionMenu1.GotoText += interactionMenu1GotoText;
            interactionMenu1.DelText += interactionMenu1DelText;
            extratTo.Click += extratToClick;
            save.Click += saveClick;
            saveAs.Click += saveAsClick;
            loadProj.Click += loadProjClick;
            frameCreator1.FrameAdded += frameCreatorFrameAdded;
            frameCreator1.FrameDeleted += frameCreator1FrameDeleted;
            FormClosing += formClosing;
            resizeableSpriteGridController1.MidX = 136;
            resizeableSpriteGridController1.MidY = 120;
            try
            {
                codeEditorController1.CodeEditor.CanUndoRedo = false;
                StringBuilder sb = new StringBuilder();
                sb.Append(File.ReadAllText(@".\ASM\Main.asm"));
                codeEditorController1.CodeEditor.AppendText(sb.ToString());
                codeEditorController1.CodeEditor.CanUndoRedo = true;
            }
            catch
            {
            }
        }

        private void frameCreator1FrameDeleted(Frame obj)
        {
            animationCreator1.RemoveFrame(obj);
            animationEditor1.Animation = animationEditor1.Animation;
        }

        private void formClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save the project?", 
                "Dyzen Sprite Maker", 
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                save.PerformClick();
            }
            else if(result== DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void interactionMenu1DelText(string obj)
        {
            RefreshCode();
            Match m = Regex.Match(codeEditorController1.CodeEditor.Text,
                @";>Action( |\t)*(\r\n|\n)(|.*(\r\n|\n))" + obj +
                ":( |\t)*(\r\n|\n)(|.*(\r\n|\n))*;>End Action");
            if (m.Success)
            {
                codeEditorController1.CodeEditor.DeleteRange(m.Index, m.Length);
                MessageBox.Show("Action " + obj + " was deleted.", "Action Deleted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                interactionMenu1.GetActions(codeEditorController1.CodeEditor.Text);
            }
            else
            {
                MessageBox.Show("This action cant be deleted.", "Action Deleted Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void interactionMenu1GotoText(string obj)
        {
            RefreshCode();
            Match m = Regex.Match(codeEditorController1.CodeEditor.Text,
                "\n" + obj + ":");
            if (m.Success)
            {
                tabControl1.SelectedIndex = 3;
                int LineInd = 
                    codeEditorController1.CodeEditor.LineFromPosition(m.Index);
                LineInd += 5;
                if (LineInd >= codeEditorController1.CodeEditor.Lines.Count)
                {
                    LineInd = codeEditorController1.CodeEditor.Lines.Count - 1;
                }
                codeEditorController1.CodeEditor.GotoPosition(codeEditorController1.CodeEditor.Lines[LineInd].Position);
            }
        }

        private void interactionMenu1AddText(string obj)
        {
            codeEditorController1.CodeEditor.InsertText(codeEditorController1.CodeEditor.TextLength, obj);
            RefreshCode();
            interactionMenu1.GetActions(codeEditorController1.CodeEditor.Text);
        }

        private void frameCreatorFrameAdded(Frame obj)
        {
            obj.MidX = resizeableSpriteGridController1.MidX;
            obj.MidY = resizeableSpriteGridController1.MidY;
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
            if (folderdialog.ShowDialog(this) != DialogResult.OK)
                return;

            ExtractResourcesDialog.LockedFlipX = 
                Animation.RequireFlipX(animationCreator1.Animations);
            ExtractResourcesDialog.LockedFlipY =
                Animation.RequireFlipY(animationCreator1.Animations);
            if (ExtractResourcesDialog.Show(this) != DialogResult.OK)
                return;
            string path = folderdialog.SelectedPath;
            path += "\\";

            int boolcounter = 0;
            if (ExtractResourcesDialog.Code) boolcounter++;
            if (ExtractResourcesDialog.SP1) boolcounter++;
            if (ExtractResourcesDialog.SP2) boolcounter++;
            if (ExtractResourcesDialog.SP3) boolcounter++;
            if (ExtractResourcesDialog.SP4) boolcounter++;
            if (ExtractResourcesDialog.Palette) boolcounter++;

            if (boolcounter == 0) MessageBox.Show("Congratulations, you extract nothing, eat a cookie.", "?????????", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            else if (boolcounter > 1)
            {
                path += ExtractResourcesDialog.ProjectName;
                if (!Directory.Exists(path)) 
                {
                    Directory.CreateDirectory(path);
                }
                path += "\\";
            }

            if (ExtractResourcesDialog.Code)
            {
                RefreshCode();
                string s = codeEditorController1.CodeEditor.Text;
                Frame.GetFramesIndexs(frameCreator1.Frames);

                HitBox[] hbs = Frame.GetFramesHitboxesFromFrameList(frameCreator1.Frames,
                    ExtractResourcesDialog.FlipX,
                    ExtractResourcesDialog.FlipY);
                s = s.Replace("dw >intAdd.",
                    Frame.GetFramesFlippersFromFrameList(frameCreator1.Frames,
                    ExtractResourcesDialog.FlipX,
                    ExtractResourcesDialog.FlipY));
                s = s.Replace("dw >hbst.",
                    HitBox.GetHitboxesStartsFromArray(hbs));
                s = s.Replace("dw >fhbsInd.",
                    Frame.GetFramesHitboxesIndexersFromFrameList(frameCreator1.Frames,
                    ExtractResourcesDialog.FlipX,
                    ExtractResourcesDialog.FlipY));
                s = s.Replace("db >fhbs.",
                    Frame.GetFramesHitboxesIdsFromFrameList(frameCreator1.Frames, hbs,
                    ExtractResourcesDialog.FlipX,
                    ExtractResourcesDialog.FlipY));
                s = s.Replace("db >hbs.",
                    HitBox.GetHitboxesFromArray(hbs, interactionMenu1.GetActionNames()));
                s = s.Replace("dw >hbacts.",
                    interactionMenu1.GetActionString(codeEditorController1.CodeEditor.Text));
                s = s.Replace("dw >fls.",
                    Frame.GetFramesLengthFromFrameList(frameCreator1.Frames,
                    ExtractResourcesDialog.FlipX,
                    ExtractResourcesDialog.FlipY));
                s = s.Replace("dw >ffps.",
                    Frame.GetFramesFlippersFromFrameList(frameCreator1.Frames,
                    ExtractResourcesDialog.FlipX,
                    ExtractResourcesDialog.FlipY));
                s = s.Replace("dw >fsp.",
                    Frame.GetFramesStartsFromFrameList(frameCreator1.Frames,
                    ExtractResourcesDialog.FlipX,
                    ExtractResourcesDialog.FlipY));
                s = s.Replace("dw >fep.",
                    Frame.GetFramesEndsFromFrameList(frameCreator1.Frames,
                    ExtractResourcesDialog.FlipX,
                    ExtractResourcesDialog.FlipY));
                s = s.Replace("db >tiles.",
                    Frame.GetTilesCodesFromFrameList(frameCreator1.Frames,
                    ExtractResourcesDialog.FlipX,
                    ExtractResourcesDialog.FlipY));
                bool sameprop = Frame.SameProp(frameCreator1.Frames);
                s = s.Replace("db >props.",
                    Frame.GetTilesPropertiesFromFrameList(frameCreator1.Frames,
                    ExtractResourcesDialog.FlipX,
                    ExtractResourcesDialog.FlipY,
                    sameprop));
                s = s.Replace("db >xdisps.",
                    Frame.GetTilesXDispFromFrameList(frameCreator1.Frames,
                    ExtractResourcesDialog.FlipX,
                    ExtractResourcesDialog.FlipY));
                s = s.Replace("db >ydisps.",
                    Frame.GetTilesYDispFromFrameList(frameCreator1.Frames,
                    ExtractResourcesDialog.FlipX,
                    ExtractResourcesDialog.FlipY));
                s = s.Replace("db >sizes.",
                    Frame.GetTilesSizesFromFrameList(frameCreator1.Frames,
                    ExtractResourcesDialog.FlipX,
                    ExtractResourcesDialog.FlipY));
                s = s.Replace("dw >anl.",
                    Animation.GetAnimationLenghts(animationCreator1.Animations));
                s = s.Replace("dw >alt.",
                    Animation.GetAnimationLastTransitions(animationCreator1.Animations));
                s = s.Replace("dw >ai.",
                    Animation.GetAnimationIndexers(animationCreator1.Animations));
                s = s.Replace("db >af.",
                    Animation.GetAnimationFrames(animationCreator1.Animations));
                s = s.Replace("db >aft.",
                    Animation.GetAnimationTimes(animationCreator1.Animations));
                s = s.Replace("db >aff.",
                    Animation.GetAnimationFlips(animationCreator1.Animations));

                s = File.ReadAllText(@".\ASM\Defines.asm") + "\n" + s;
                if (File.Exists(path + ExtractResourcesDialog.ProjectName + ".asm"))
                    File.Delete(path + ExtractResourcesDialog.ProjectName + ".asm");
                File.WriteAllText(path + ExtractResourcesDialog.ProjectName + ".asm", s);
            }
            if (ExtractResourcesDialog.SP1 || ExtractResourcesDialog.SP2)
            {
                byte[] sp12 = spriteGFXBox1.GetGFX();
                if (ExtractResourcesDialog.SP1)
                {
                    byte[] sp1 = new byte[sp12.Length / 2];

                    for (int i = 0; i < sp1.Length; i++)
                    {
                        sp1[i] = sp12[i];
                    }
                    if (File.Exists(path + ExtractResourcesDialog.ProjectName + "SP1.bin"))
                        File.Delete(path + ExtractResourcesDialog.ProjectName + "SP1.bin");
                    File.WriteAllBytes(path + ExtractResourcesDialog.ProjectName + "SP1.bin", sp1);
                }
                if (ExtractResourcesDialog.SP2)
                {
                    byte[] sp2 = new byte[sp12.Length / 2];

                    for (int i = 0; i < sp2.Length; i++)
                    {
                        sp2[i] = sp12[i + sp2.Length];
                    }
                    if (File.Exists(path + ExtractResourcesDialog.ProjectName + "SP2.bin"))
                        File.Delete(path + ExtractResourcesDialog.ProjectName + "SP2.bin");
                    File.WriteAllBytes(path + ExtractResourcesDialog.ProjectName + "SP2.bin", sp2);
                }
            }
            if (ExtractResourcesDialog.SP3 || ExtractResourcesDialog.SP4)
            {
                byte[] sp34 = spriteGFXBox2.GetGFX();
                if (ExtractResourcesDialog.SP3)
                {
                    byte[] sp3 = new byte[sp34.Length / 2];

                    for (int i = 0; i < sp3.Length; i++)
                    {
                        sp3[i] = sp34[i];
                    }
                    if (File.Exists(path + ExtractResourcesDialog.ProjectName + "SP3.bin"))
                        File.Delete(path + ExtractResourcesDialog.ProjectName + "SP3.bin");
                    File.WriteAllBytes(path + ExtractResourcesDialog.ProjectName + "SP3.bin", sp3);
                }
                if (ExtractResourcesDialog.SP4)
                {
                    byte[] sp4 = new byte[sp34.Length / 2];

                    for (int i = 0; i < sp4.Length; i++)
                    {
                        sp4[i] = sp34[i + sp4.Length];
                    }
                    if (File.Exists(path + ExtractResourcesDialog.ProjectName + "SP4.bin"))
                        File.Delete(path + ExtractResourcesDialog.ProjectName + "SP4.bin");
                    File.WriteAllBytes(path + ExtractResourcesDialog.ProjectName + "SP4.bin", sp4);
                }
            }
            if(ExtractResourcesDialog.Palette)
            {
                byte[] pal = ColorPalette.SaveGlobalPalette();
                if (File.Exists(path + ExtractResourcesDialog.ProjectName + "Palette.pal"))
                    File.Delete(path + ExtractResourcesDialog.ProjectName + "Palette.pal");
                File.WriteAllBytes(path + ExtractResourcesDialog.ProjectName + "Palette.pal", pal);
            }
            MessageBox.Show("Resources extracted successfully", "Extract Resources", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        string secGrString = @"\;\>Section Graphics(.*\n)*\;\>End Graphics Section";
        string grCallString = @"JSR( |\t)+GraphicRoutine";
        string secAnString = @"\;\>Section Animations(.*\n)*\;\>End Animations Section";
        string anCallString = @"JSR( |\t)+AnimationRoutine";
        string anCallString2 = @"JSL( |\t)+InitWrapperChangeAnimationFromStart";
        string secHBIntString = @"\;\>Section Hitboxes Interaction(.*\n)*\;\>End Hitboxes Interaction Section";
        string hbIntCallString = @"JSR( |\t)+InteractMarioSprite";
        private void selectedIndexChanged(object sender, System.EventArgs e)
        {
            frameSelector1.Frames = frameCreator1.Frames;
            frameSelector1.BuildTable();
            animationEditor1.Animation = animationEditor1.Animation;
            if (tabControl1.SelectedIndex == 2)
            {
                interactionMenu1.GetActions(codeEditorController1.CodeEditor.Text);
                interactionMenu1.UpdateFrameList(frameCreator1.Frames);
            }
            else if (tabControl1.SelectedIndex == 3) 
            {
                ExtractResourcesDialog.LockedFlipX =
                    Animation.RequireFlipX(animationCreator1.Animations);
                ExtractResourcesDialog.LockedFlipY =
                    Animation.RequireFlipY(animationCreator1.Animations);
                RefreshCode();
            }
        }

        string tagPat = @"\<tag\>(|.*\n?)*\<\/tag\>";
        string tagDelPat = @"\<\/?tag\>";
        public string UseTag(string target, string tag, string replace)
        {
            string newt = target;
            string tp = tagPat.Replace("tag", tag);
            newt = Regex.Replace(newt, tp, replace);
            return newt;
        }

        public string DeleteTag(string target, string tag)
        {
            string newt = target;
            string tp = tagDelPat.Replace("tag", tag);
            newt = Regex.Replace(newt, tp, "");
            return newt;
        }

        public void RefreshCode()
        {
            Match m = Regex.Match(codeEditorController1.CodeEditor.Text,
                        secGrString);
            MatchCollection ms;

            bool validFrames = Frame.ValidArray(frameCreator1.Frames);

            if (m.Success)
            {
                codeEditorController1.CodeEditor.DeleteRange(m.Index,
                    m.Length);
                if (validFrames)
                {
                    string grRout = File.ReadAllText(@".\ASM\GraphicRoutine.asm");

                    if (ExtractResourcesDialog.LockedFlipX ||
                        ExtractResourcesDialog.LockedFlipY)
                        grRout = DeleteTag(grRout, "localflip");
                    else
                        grRout = UseTag(grRout, "localflip", "");

                    if (ExtractResourcesDialog.FlipX ||
                        ExtractResourcesDialog.FlipY)
                        grRout = DeleteTag(grRout, "globalflip");
                    else
                        grRout = UseTag(grRout, "globalflip", "");

                    if (Frame.SameLenght(frameCreator1.Frames))
                    {
                        grRout = UseTag(grRout, "samelength1", "\tLDA #$$" +
                            (frameCreator1.Frames[0].Tiles.Count - 1).ToString("X4") + "\n");
                        grRout = UseTag(grRout, "samelength2", "");
                    }
                    else
                    {
                        grRout = DeleteTag(grRout, "samelength1");
                        grRout = DeleteTag(grRout, "samelength2");
                    }

                    if (frameCreator1.Frames.Length == 1 && !(ExtractResourcesDialog.FlipX ||
                        ExtractResourcesDialog.FlipY))
                    {
                        grRout = UseTag(grRout, "oneframe1", "\tLDA #$$0000\n");
                        grRout = UseTag(grRout, "oneframe2", "\tLDA #$$" +
                            (frameCreator1.Frames[0].Tiles.Count - 1).ToString("X4") + "\n");
                        grRout = UseTag(grRout, "oneframe3", "");
                    }
                    else
                    {
                        grRout = DeleteTag(grRout, "oneframe1");
                        grRout = DeleteTag(grRout, "oneframe2");
                        grRout = DeleteTag(grRout, "oneframe3");
                    }

                    if(Frame.SameTile(frameCreator1.Frames))
                    {
                        grRout = UseTag(grRout, "sametile1", "\tLDA #$"+
                            Frame.FirstTile(frameCreator1.Frames) + "\n");
                        grRout = UseTag(grRout, "sametile2", "");
                    }
                    else
                    {
                        grRout = DeleteTag(grRout, "sametile1");
                        grRout = DeleteTag(grRout, "sametile2");
                    }

                    if(Frame.SameProp(frameCreator1.Frames))
                    {
                        grRout = UseTag(grRout, "sameprop1", "\tLDA #$"+
                            Frame.FirstProperty(frameCreator1.Frames) + "\n");
                        grRout = UseTag(grRout, "sameprop2", "");
                        grRout = DeleteTag(grRout, "sameprop3");
                    }
                    else
                    {
                        grRout = DeleteTag(grRout, "sameprop1");
                        grRout = DeleteTag(grRout, "sameprop2");
                        grRout = UseTag(grRout, "sameprop3", "");
                    }

                    if(Frame.SameSize(frameCreator1.Frames))
                    {
                        grRout = UseTag(grRout, "samesize1", "");
                        grRout = UseTag(grRout, "samesize2", "\tLDY #$"+
                             Frame.FirstSize(frameCreator1.Frames) + "\n");
                        grRout = UseTag(grRout, "samesize3", "");
                    }
                    else
                    {
                        grRout = DeleteTag(grRout, "samesize1");
                        grRout = DeleteTag(grRout, "samesize2");
                        grRout = DeleteTag(grRout, "samesize3");
                    }

                    if (Frame.SameXDisp(frameCreator1.Frames, ExtractResourcesDialog.FlipX))
                    {
                        grRout = UseTag(grRout, "samexdisp1", "\tADC #$" +
                            Frame.FirstXDisp(frameCreator1.Frames) + "\n");
                        grRout = UseTag(grRout, "samexdisp2", "");
                    }
                    else
                    {
                        grRout = DeleteTag(grRout, "samexdisp1");
                        grRout = DeleteTag(grRout, "samexdisp2");
                    }

                    if (Frame.SameYDisp(frameCreator1.Frames, ExtractResourcesDialog.FlipY))
                    {
                        grRout = UseTag(grRout, "sameydisp1", "\tADC #$" +
                            Frame.FirstYDisp(frameCreator1.Frames) + "\n");
                        grRout = UseTag(grRout, "sameydisp2", "");
                    }
                    else
                    {
                        grRout = DeleteTag(grRout, "sameydisp1");
                        grRout = DeleteTag(grRout, "sameydisp2");
                    }

                    codeEditorController1.CodeEditor.InsertText(m.Index,
                        grRout);
                    ms = Regex.Matches(codeEditorController1.CodeEditor.Text,
                        ";( |\t)*" + grCallString);
                    foreach (Match ma in ms)
                    {
                        codeEditorController1.CodeEditor.DeleteRange(ma.Index,
                            ma.Length);
                        codeEditorController1.CodeEditor.InsertText(ma.Index,
                            "JSR GraphicRoutine");
                    }
                }
                else
                {
                    codeEditorController1.CodeEditor.InsertText(m.Index,
                            ";>Section Graphics\n;>End Graphics Section");
                    ms = Regex.Matches(codeEditorController1.CodeEditor.Text,
                        "(;( |\t)*)?" + grCallString);
                    foreach (Match ma in ms)
                    {
                        codeEditorController1.CodeEditor.DeleteRange(ma.Index,
                            ma.Length);
                        codeEditorController1.CodeEditor.InsertText(ma.Index,
                                ";JSR GraphicRoutine");
                    }
                }
            }

            m = Regex.Match(codeEditorController1.CodeEditor.Text,
                    secAnString);

            if (m.Success)
            {
                codeEditorController1.CodeEditor.DeleteRange(m.Index,
                            m.Length);
                if (animationCreator1.Animations.Length > 0 && validFrames)
                {
                    string anRout = File.ReadAllText(@".\ASM\AnimationRoutine.asm").Replace(">ChangeRoutines.",
                        Animation.GetAnimationChangeRoutine(animationCreator1.Animations));

                    if (!(ExtractResourcesDialog.LockedFlipX ||
                        ExtractResourcesDialog.LockedFlipY))
                    {
                        anRout = UseTag(anRout, "localflip", "");
                    }
                    else
                    {
                        anRout = DeleteTag(anRout, "localflip");
                    }

                    codeEditorController1.CodeEditor.InsertText(m.Index,
                        anRout);
                    ms = Regex.Matches(codeEditorController1.CodeEditor.Text,
                       ";( |\t)*" + anCallString);
                    foreach (Match ma in ms)
                    {
                        codeEditorController1.CodeEditor.DeleteRange(ma.Index,
                            ma.Length);
                        codeEditorController1.CodeEditor.InsertText(ma.Index,
                            "JSR AnimationRoutine");
                    }
                    ms = Regex.Matches(codeEditorController1.CodeEditor.Text,
                       ";( |\t)*" + anCallString2);
                    foreach (Match ma in ms)
                    {
                        codeEditorController1.CodeEditor.DeleteRange(ma.Index,
                            ma.Length);
                        codeEditorController1.CodeEditor.InsertText(ma.Index,
                            "JSL InitWrapperChangeAnimationFromStart");
                    }
                }
                else
                {
                    codeEditorController1.CodeEditor.InsertText(m.Index,
                        ";>Section Animations\n;>End Animations Section");
                    ms = Regex.Matches(codeEditorController1.CodeEditor.Text,
                        "(;( |\t)*)?" + anCallString);
                    foreach (Match ma in ms)
                    {
                        codeEditorController1.CodeEditor.DeleteRange(ma.Index,
                            ma.Length);
                        codeEditorController1.CodeEditor.InsertText(ma.Index,
                                ";JSR AnimationRoutine");
                    }
                    ms = Regex.Matches(codeEditorController1.CodeEditor.Text,
                       "(;( |\t)*)?" + anCallString2);
                    foreach (Match ma in ms)
                    {
                        codeEditorController1.CodeEditor.DeleteRange(ma.Index,
                            ma.Length);
                        codeEditorController1.CodeEditor.InsertText(ma.Index,
                            ";JSL InitWrapperChangeAnimationFromStart");
                    }
                }
            }

            m = Regex.Match(codeEditorController1.CodeEditor.Text,
                secHBIntString);
            if (m.Success)
            {
                codeEditorController1.CodeEditor.DeleteRange(m.Index,
                            m.Length);
                if (Frame.HaveHitboxInteraction(frameCreator1.Frames))
                {
                    string intRout = File.ReadAllText(@".\ASM\HitboxInteractionRoutine.asm");

                    interactionMenu1.GetActions(codeEditorController1.CodeEditor.Text);
                    string[] an = interactionMenu1.GetActionNames();

                    if (an.Length < 2)
                    {
                        intRout = UseTag(intRout, "onlydefaultaction1", "" +
                            "\tREP #$$10\n\tBCS ++\n\tPLY\n\tPLY\n\tINY\n\tJMP -\n++\n" +
                            "\tPLY\n\tPLY\n\tLDA !ScratchE\n\tORA #$$01\n\tSTA !ScratchE\n\t" +
                            "SEP #$$10\n\tLDX !SpriteIndex\n\tSEC\n\tRTS\n");
                        intRout = UseTag(intRout, "onlydefaultaction2", "");
                    }
                    else
                    {
                        intRout = DeleteTag(intRout, "onlydefaultaction1");
                        intRout = DeleteTag(intRout, "onlydefaultaction2");
                    }

                    if(!(ExtractResourcesDialog.FlipX ||
                        ExtractResourcesDialog.FlipY))
                    {
                        intRout = UseTag(intRout, "globalflip", "");
                    }
                    else
                    {
                        intRout = DeleteTag(intRout, "globalflip");
                    }

                    if(!(ExtractResourcesDialog.LockedFlipX || 
                        ExtractResourcesDialog.LockedFlipY))
                    {
                        intRout = UseTag(intRout, "localflip", "");
                    }
                    else
                    {
                        intRout = DeleteTag(intRout, "localflip");
                    }

                    codeEditorController1.CodeEditor.InsertText(m.Index,
                        intRout);
                    ms = Regex.Matches(codeEditorController1.CodeEditor.Text,
                       ";( |\t)*" + hbIntCallString);
                    foreach (Match ma in ms)
                    {
                        codeEditorController1.CodeEditor.DeleteRange(ma.Index,
                            ma.Length);
                        codeEditorController1.CodeEditor.InsertText(ma.Index,
                            "JSR InteractMarioSprite");
                    }
                }
                else
                {
                    codeEditorController1.CodeEditor.InsertText(m.Index,
                        ";>Section Hitboxes Interaction\n;>End Hitboxes Interaction Section");
                    ms = Regex.Matches(codeEditorController1.CodeEditor.Text,
                       "(;( |\t)*)?" + hbIntCallString);
                    foreach (Match ma in ms)
                    {
                        codeEditorController1.CodeEditor.DeleteRange(ma.Index,
                            ma.Length);
                        codeEditorController1.CodeEditor.InsertText(ma.Index,
                            ";JSR InteractMarioSprite");
                    }
                }
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
