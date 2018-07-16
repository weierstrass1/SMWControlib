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
            this.gfxBox1 = new controls.GFXBox();
            this.gfxButton1 = new controls.Graphics_Controls.GFXButton();
            this.gfxButton2 = new controls.Graphics_Controls.GFXButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            // gfxBox1
            // 
            this.gfxBox1.BehindBitmap = ((System.Drawing.Bitmap)(resources.GetObject("gfxBox1.BehindBitmap")));
            this.gfxBox1.Image = ((System.Drawing.Image)(resources.GetObject("gfxBox1.Image")));
            this.gfxBox1.Location = new System.Drawing.Point(42, 47);
            this.gfxBox1.Name = "gfxBox1";
            this.gfxBox1.Selection = new System.Drawing.Rectangle(0, 0, 64, 64);
            this.gfxBox1.SelectionAccuracy = 8;
            this.gfxBox1.SelectionColor = System.Drawing.Color.Lavender;
            this.gfxBox1.SelectionMinSize = 8;
            this.gfxBox1.Size = new System.Drawing.Size(256, 256);
            this.gfxBox1.TabIndex = 0;
            this.gfxBox1.TabStop = false;
            this.gfxBox1.TileZoom = 2;
            this.gfxBox1.Zoom = 2;
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
            this.gfxButton1.Tilesize = 8;
            this.gfxButton1.UseVisualStyleBackColor = true;
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
            this.gfxButton2.Tilesize = 8;
            this.gfxButton2.UseVisualStyleBackColor = true;
            // 
            // testwindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gfxButton2);
            this.Controls.Add(this.gfxButton1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.gfxBox1);
            this.Name = "testwindows";
            this.Text = "testwindows";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gfxBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private controls.GFXBox gfxBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private controls.Graphics_Controls.GFXButton gfxButton1;
        private controls.Graphics_Controls.GFXButton gfxButton2;
    }
}