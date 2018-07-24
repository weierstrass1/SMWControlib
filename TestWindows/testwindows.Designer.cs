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
            this.gfxBox1 = new SMWControlibControls.GraphicsControls.GFXBox();
            this.gfxButton2 = new SMWControlibControls.GraphicsControls.GFXButton();
            this.gfxButton1 = new SMWControlibControls.GraphicsControls.GFXButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.paletteButton1 = new SMWControlibControls.GraphicsControls.PaletteButton();
            this.paletteBox1 = new SMWControlibControls.GraphicsControls.PaletteBox();
            this.resizeableSpriteGridController1 = new SMWControlibControls.GraphicsControls.ResizeableSpriteGridController();
            this.frameCreator1 = new SMWControlibControls.GraphicsControls.FrameCreator();
            ((System.ComponentModel.ISupportInitialize)(this.gfxBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paletteBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gfxBox1
            // 
            this.gfxBox1.BehindBitmap = ((System.Drawing.Bitmap)(resources.GetObject("gfxBox1.BehindBitmap")));
            this.gfxBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gfxBox1.Image = ((System.Drawing.Image)(resources.GetObject("gfxBox1.Image")));
            this.gfxBox1.Location = new System.Drawing.Point(3, 3);
            this.gfxBox1.Name = "gfxBox1";
            this.gfxBox1.Selection = new System.Drawing.Rectangle(0, 0, 32, 32);
            this.gfxBox1.SelectionAccuracy = 8;
            this.gfxBox1.SelectionColor = System.Drawing.Color.White;
            this.gfxBox1.SelectionMinSize = 16;
            this.gfxBox1.Size = new System.Drawing.Size(256, 256);
            this.gfxBox1.SP = 0;
            this.gfxBox1.TabIndex = 7;
            this.gfxBox1.TabStop = false;
            this.gfxBox1.TileZoom = 4;
            this.gfxBox1.Zoom = 2;
            // 
            // gfxButton2
            // 
            this.gfxButton2.BaseTile = 8;
            this.gfxButton2.Location = new System.Drawing.Point(84, 265);
            this.gfxButton2.Name = "gfxButton2";
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
            this.gfxButton1.Location = new System.Drawing.Point(3, 265);
            this.gfxButton1.Name = "gfxButton1";
            this.gfxButton1.Size = new System.Drawing.Size(75, 23);
            this.gfxButton1.StartFolder = "";
            this.gfxButton1.TabIndex = 2;
            this.gfxButton1.Text = "Load Top";
            this.gfxButton1.Tilesize = 16;
            this.gfxButton1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gfxBox1);
            this.panel1.Controls.Add(this.gfxButton1);
            this.panel1.Controls.Add(this.gfxButton2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(263, 561);
            this.panel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.frameCreator1);
            this.panel2.Controls.Add(this.paletteButton1);
            this.panel2.Controls.Add(this.paletteBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(779, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(273, 561);
            this.panel2.TabIndex = 10;
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
            this.resizeableSpriteGridController1.Location = new System.Drawing.Point(263, 0);
            this.resizeableSpriteGridController1.Name = "resizeableSpriteGridController1";
            this.resizeableSpriteGridController1.Size = new System.Drawing.Size(516, 561);
            this.resizeableSpriteGridController1.TabIndex = 11;
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
            // testwindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 561);
            this.Controls.Add(this.resizeableSpriteGridController1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(691, 333);
            this.Name = "testwindows";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "testwindows";
            ((System.ComponentModel.ISupportInitialize)(this.gfxBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paletteBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private SMWControlibControls.GraphicsControls.GFXButton gfxButton1;
        private SMWControlibControls.GraphicsControls.GFXButton gfxButton2;
        private SMWControlibControls.GraphicsControls.GFXBox gfxBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private SMWControlibControls.GraphicsControls.PaletteButton paletteButton1;
        private SMWControlibControls.GraphicsControls.PaletteBox paletteBox1;
        private SMWControlibControls.GraphicsControls.ResizeableSpriteGridController resizeableSpriteGridController1;
        private SMWControlibControls.GraphicsControls.FrameCreator frameCreator1;
    }
}