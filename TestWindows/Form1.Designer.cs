namespace TestWindows
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.gfxBox1 = new controls.GFXBox();
            ((System.ComponentModel.ISupportInitialize)(this.gfxBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gfxBox1
            // 
            this.gfxBox1.BehindBitmap = ((System.Drawing.Bitmap)(resources.GetObject("gfxBox1.BehindBitmap")));
            this.gfxBox1.Location = new System.Drawing.Point(255, 101);
            this.gfxBox1.Name = "gfxBox1";
            this.gfxBox1.Selection = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.gfxBox1.SelectionAccuracy = 1;
            this.gfxBox1.SelectionMinSize = 1;
            this.gfxBox1.Size = new System.Drawing.Size(212, 158);
            this.gfxBox1.TabIndex = 0;
            this.gfxBox1.TabStop = false;
            this.gfxBox1.Zoom = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gfxBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.gfxBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private controls.GFXBox gfxBox1;
    }
}

