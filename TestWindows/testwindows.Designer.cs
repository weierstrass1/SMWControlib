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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.paletteBox1 = new SMWControlibControls.GraphicsControls.PaletteBox();
            this.gfxButton2 = new SMWControlibControls.GraphicsControls.GFXButton();
            this.gfxButton1 = new SMWControlibControls.GraphicsControls.GFXButton();
            this.gfxBox1 = new SMWControlibControls.GraphicsControls.GFXBox();
            this.paletteButton1 = new SMWControlibControls.GraphicsControls.PaletteButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paletteBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gfxBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(350, 47);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 256);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // paletteBox1
            // 
            this.paletteBox1.FirstPaletteToShow = 8;
            this.paletteBox1.Image = ((System.Drawing.Image)(resources.GetObject("paletteBox1.Image")));
            this.paletteBox1.Location = new System.Drawing.Point(631, 47);
            this.paletteBox1.Name = "paletteBox1";
            this.paletteBox1.Size = new System.Drawing.Size(256, 128);
            this.paletteBox1.TabIndex = 4;
            this.paletteBox1.TabStop = false;
            this.paletteBox1.Zoom = 16;
            // 
            // gfxButton2
            // 
            this.gfxButton2.BaseTile = 8;
            this.gfxButton2.Location = new System.Drawing.Point(123, 310);
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
            this.gfxButton1.Location = new System.Drawing.Point(42, 310);
            this.gfxButton1.Name = "gfxButton1";
            this.gfxButton1.Size = new System.Drawing.Size(75, 23);
            this.gfxButton1.StartFolder = "";
            this.gfxButton1.TabIndex = 2;
            this.gfxButton1.Text = "Load Top";
            this.gfxButton1.Tilesize = 16;
            this.gfxButton1.UseVisualStyleBackColor = true;
            // 
            // gfxBox1
            // 
            this.gfxBox1.BehindBitmap = ((System.Drawing.Bitmap)(resources.GetObject("gfxBox1.BehindBitmap")));
            this.gfxBox1.Image = ((System.Drawing.Image)(resources.GetObject("gfxBox1.Image")));
            this.gfxBox1.Location = new System.Drawing.Point(42, 47);
            this.gfxBox1.Name = "gfxBox1";
            this.gfxBox1.Selection = new System.Drawing.Rectangle(0, 0, 64, 64);
            this.gfxBox1.SelectionAccuracy = 8;
            this.gfxBox1.SelectionColor = System.Drawing.Color.Lavender;
            this.gfxBox1.SelectionMinSize = 16;
            this.gfxBox1.Size = new System.Drawing.Size(256, 256);
            this.gfxBox1.SP = 0;
            this.gfxBox1.TabIndex = 0;
            this.gfxBox1.TabStop = false;
            this.gfxBox1.TileZoom = 2;
            this.gfxBox1.Zoom = 2;
            // 
            // paletteButton1
            // 
            this.paletteButton1.Location = new System.Drawing.Point(631, 181);
            this.paletteButton1.Name = "paletteButton1";
            this.paletteButton1.Size = new System.Drawing.Size(75, 23);
            this.paletteButton1.StartFolder = "";
            this.paletteButton1.TabIndex = 5;
            this.paletteButton1.Text = "Load Palette";
            this.paletteButton1.UseVisualStyleBackColor = true;
            // 
            // testwindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 450);
            this.Controls.Add(this.paletteButton1);
            this.Controls.Add(this.paletteBox1);
            this.Controls.Add(this.gfxButton2);
            this.Controls.Add(this.gfxButton1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.gfxBox1);
            this.Name = "testwindows";
            this.Text = "testwindows";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paletteBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gfxBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SMWControlibControls.GraphicsControls.GFXBox gfxBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private SMWControlibControls.GraphicsControls.GFXButton gfxButton1;
        private SMWControlibControls.GraphicsControls.GFXButton gfxButton2;
        private SMWControlibControls.GraphicsControls.PaletteBox paletteBox1;
        private SMWControlibControls.GraphicsControls.PaletteButton paletteButton1;
    }
}