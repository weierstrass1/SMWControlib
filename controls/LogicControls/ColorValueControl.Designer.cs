namespace SMWControlibControls.LogicControls
{
    partial class ColorValueControl
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
            this.color = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.color)).BeginInit();
            this.SuspendLayout();
            // 
            // color
            // 
            this.color.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.color.Location = new System.Drawing.Point(130, 99);
            this.color.Margin = new System.Windows.Forms.Padding(8, 3, 3, 3);
            this.color.Name = "color";
            this.color.Size = new System.Drawing.Size(16, 16);
            this.color.TabIndex = 36;
            this.color.TabStop = false;
            // 
            // ColorValueControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.Controls.Add(this.color);
            this.Name = "ColorValueControl";
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.value, 0);
            this.Controls.SetChildIndex(this.color, 0);
            ((System.ComponentModel.ISupportInitialize)(this.value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.color)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.PictureBox color;
    }
}
