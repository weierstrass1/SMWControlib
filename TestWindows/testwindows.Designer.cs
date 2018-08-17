namespace TestWindows
{
    partial class testwindows
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(testwindows));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.spriteGFXBox1 = new SMWControlibControls.GraphicsControls.SpriteGFXBox();
            this.gfxButton2 = new SMWControlibControls.GraphicsControls.GFXButton();
            this.gfxButton1 = new SMWControlibControls.GraphicsControls.GFXButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gfxButton3 = new SMWControlibControls.GraphicsControls.GFXButton();
            this.gfxButton4 = new SMWControlibControls.GraphicsControls.GFXButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.frameCreator1 = new SMWControlibControls.GraphicsControls.FrameCreator();
            this.paletteButton1 = new SMWControlibControls.GraphicsControls.PaletteButton();
            this.paletteBox1 = new SMWControlibControls.GraphicsControls.PaletteBox();
            this.resizeableSpriteGridController1 = new SMWControlibControls.GraphicsControls.ResizeableSpriteGridController();
            this.spriteGFXBox2 = new SMWControlibControls.GraphicsControls.SpriteGFXBox();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spriteGFXBox1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paletteBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spriteGFXBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(272, 561);
            this.panel1.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(270, 485);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.spriteGFXBox1);
            this.tabPage1.Controls.Add(this.gfxButton2);
            this.tabPage1.Controls.Add(this.gfxButton1);
            this.tabPage1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(262, 459);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SP1/SP2";
            // 
            // spriteGFXBox1
            // 
            this.spriteGFXBox1.BehindBitmap = ((System.Drawing.Bitmap)(resources.GetObject("spriteGFXBox1.BehindBitmap")));
            this.spriteGFXBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.spriteGFXBox1.Image = ((System.Drawing.Image)(resources.GetObject("spriteGFXBox1.Image")));
            this.spriteGFXBox1.ImageHeigth = 128;
            this.spriteGFXBox1.ImageWidth = 128;
            this.spriteGFXBox1.Location = new System.Drawing.Point(0, 0);
            this.spriteGFXBox1.Name = "spriteGFXBox1";
            this.spriteGFXBox1.Selection = new System.Drawing.Rectangle(0, 0, 16, 16);
            this.spriteGFXBox1.SelectionAccuracy = 8;
            this.spriteGFXBox1.SelectionColor = System.Drawing.Color.White;
            this.spriteGFXBox1.SelectionMinSize = 8;
            this.spriteGFXBox1.Size = new System.Drawing.Size(260, 260);
            this.spriteGFXBox1.SP = 0;
            this.spriteGFXBox1.TabIndex = 4;
            this.spriteGFXBox1.TabStop = false;
            this.spriteGFXBox1.TileZoom = 1;
            this.spriteGFXBox1.Zoom = 2;
            // 
            // gfxButton2
            // 
            this.gfxButton2.BaseTile = 8;
            this.gfxButton2.Location = new System.Drawing.Point(87, 263);
            this.gfxButton2.Name = "gfxButton2";
            this.gfxButton2.Position = 64;
            this.gfxButton2.Size = new System.Drawing.Size(75, 23);
            this.gfxButton2.StartFolder = "";
            this.gfxButton2.TabIndex = 3;
            this.gfxButton2.Text = "Load Bottom";
            this.gfxButton2.Tilesize = 16;
            this.gfxButton2.UseVisualStyleBackColor = true;
            // 
            // gfxButton1
            // 
            this.gfxButton1.BaseTile = 0;
            this.gfxButton1.Location = new System.Drawing.Point(6, 263);
            this.gfxButton1.Name = "gfxButton1";
            this.gfxButton1.Position = 0;
            this.gfxButton1.Size = new System.Drawing.Size(75, 23);
            this.gfxButton1.StartFolder = "";
            this.gfxButton1.TabIndex = 2;
            this.gfxButton1.Text = "Load Top";
            this.gfxButton1.Tilesize = 16;
            this.gfxButton1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.spriteGFXBox2);
            this.tabPage2.Controls.Add(this.gfxButton3);
            this.tabPage2.Controls.Add(this.gfxButton4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(262, 459);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "SP3/SP4";
            // 
            // gfxButton3
            // 
            this.gfxButton3.BaseTile = 8;
            this.gfxButton3.Location = new System.Drawing.Point(87, 263);
            this.gfxButton3.Name = "gfxButton3";
            this.gfxButton3.Position = 64;
            this.gfxButton3.Size = new System.Drawing.Size(75, 23);
            this.gfxButton3.StartFolder = "";
            this.gfxButton3.TabIndex = 5;
            this.gfxButton3.Text = "Load Bottom";
            this.gfxButton3.Tilesize = 16;
            this.gfxButton3.UseVisualStyleBackColor = true;
            // 
            // gfxButton4
            // 
            this.gfxButton4.BaseTile = 0;
            this.gfxButton4.Location = new System.Drawing.Point(6, 263);
            this.gfxButton4.Name = "gfxButton4";
            this.gfxButton4.Position = 0;
            this.gfxButton4.Size = new System.Drawing.Size(75, 23);
            this.gfxButton4.StartFolder = "";
            this.gfxButton4.TabIndex = 4;
            this.gfxButton4.Text = "Load Top";
            this.gfxButton4.Tilesize = 16;
            this.gfxButton4.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.frameCreator1);
            this.panel2.Controls.Add(this.paletteButton1);
            this.panel2.Controls.Add(this.paletteBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(797, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(267, 561);
            this.panel2.TabIndex = 10;
            // 
            // frameCreator1
            // 
            this.frameCreator1.Location = new System.Drawing.Point(6, 175);
            this.frameCreator1.MaximumSize = new System.Drawing.Size(205, 119);
            this.frameCreator1.MinimumSize = new System.Drawing.Size(205, 119);
            this.frameCreator1.Name = "frameCreator1";
            this.frameCreator1.Size = new System.Drawing.Size(205, 119);
            this.frameCreator1.TabIndex = 2;
            // 
            // paletteButton1
            // 
            this.paletteButton1.Location = new System.Drawing.Point(6, 146);
            this.paletteButton1.Name = "paletteButton1";
            this.paletteButton1.Size = new System.Drawing.Size(75, 23);
            this.paletteButton1.StartFolder = "";
            this.paletteButton1.TabIndex = 1;
            this.paletteButton1.Text = "Load";
            this.paletteButton1.UseVisualStyleBackColor = true;
            // 
            // paletteBox1
            // 
            this.paletteBox1.FirstPaletteToShow = 8;
            this.paletteBox1.Image = ((System.Drawing.Image)(resources.GetObject("paletteBox1.Image")));
            this.paletteBox1.Location = new System.Drawing.Point(6, 12);
            this.paletteBox1.Name = "paletteBox1";
            this.paletteBox1.Size = new System.Drawing.Size(256, 128);
            this.paletteBox1.TabIndex = 0;
            this.paletteBox1.TabStop = false;
            this.paletteBox1.Zoom = 16;
            // 
            // resizeableSpriteGridController1
            // 
            this.resizeableSpriteGridController1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resizeableSpriteGridController1.Location = new System.Drawing.Point(272, 0);
            this.resizeableSpriteGridController1.Name = "resizeableSpriteGridController1";
            this.resizeableSpriteGridController1.Size = new System.Drawing.Size(525, 561);
            this.resizeableSpriteGridController1.TabIndex = 11;
            // 
            // spriteGFXBox2
            // 
            this.spriteGFXBox2.BehindBitmap = ((System.Drawing.Bitmap)(resources.GetObject("spriteGFXBox2.BehindBitmap")));
            this.spriteGFXBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.spriteGFXBox2.Image = ((System.Drawing.Image)(resources.GetObject("spriteGFXBox2.Image")));
            this.spriteGFXBox2.ImageHeigth = 128;
            this.spriteGFXBox2.ImageWidth = 128;
            this.spriteGFXBox2.Location = new System.Drawing.Point(0, 0);
            this.spriteGFXBox2.Name = "spriteGFXBox2";
            this.spriteGFXBox2.Selection = new System.Drawing.Rectangle(0, 0, 16, 16);
            this.spriteGFXBox2.SelectionAccuracy = 8;
            this.spriteGFXBox2.SelectionColor = System.Drawing.Color.White;
            this.spriteGFXBox2.SelectionMinSize = 8;
            this.spriteGFXBox2.Size = new System.Drawing.Size(260, 260);
            this.spriteGFXBox2.SP = 1;
            this.spriteGFXBox2.TabIndex = 6;
            this.spriteGFXBox2.TabStop = false;
            this.spriteGFXBox2.TileZoom = 1;
            this.spriteGFXBox2.Zoom = 2;
            // 
            // testwindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 561);
            this.Controls.Add(this.resizeableSpriteGridController1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(691, 333);
            this.Name = "testwindows";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "testwindows";
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spriteGFXBox1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paletteBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spriteGFXBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private SMWControlibControls.GraphicsControls.GFXButton gfxButton1;
        private SMWControlibControls.GraphicsControls.GFXButton gfxButton2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private SMWControlibControls.GraphicsControls.PaletteButton paletteButton1;
        private SMWControlibControls.GraphicsControls.PaletteBox paletteBox1;
        private SMWControlibControls.GraphicsControls.ResizeableSpriteGridController resizeableSpriteGridController1;
        private SMWControlibControls.GraphicsControls.FrameCreator frameCreator1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private SMWControlibControls.GraphicsControls.GFXButton gfxButton3;
        private SMWControlibControls.GraphicsControls.GFXButton gfxButton4;
        private SMWControlibControls.GraphicsControls.SpriteGFXBox spriteGFXBox1;
        private SMWControlibControls.GraphicsControls.SpriteGFXBox spriteGFXBox2;
    }
}