namespace SMWControlibControls.LogicControls
{
    partial class BrightValueControl
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
            this.bcolor = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.color)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bcolor)).BeginInit();
            this.SuspendLayout();
            // 
            // bcolor
            // 
            this.bcolor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.bcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.bcolor.Location = new System.Drawing.Point(245, 99);
            this.bcolor.Name = "bcolor";
            this.bcolor.Size = new System.Drawing.Size(16, 16);
            this.bcolor.TabIndex = 37;
            this.bcolor.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(152, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 15);
            this.label1.TabIndex = 38;
            this.label1.Text = "With Bright:";
            // 
            // BrightValueControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bcolor);
            this.Name = "BrightValueControl";
            this.Controls.SetChildIndex(this.bcolor, 0);
            this.Controls.SetChildIndex(this.value, 0);
            this.Controls.SetChildIndex(this.color, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.color)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bcolor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.PictureBox bcolor;
        private System.Windows.Forms.Label label1;
    }
}
