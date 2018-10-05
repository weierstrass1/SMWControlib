namespace SMWControlibControls.LogicControls
{
    partial class BooleanValueControl
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
            this.label2 = new System.Windows.Forms.Label();
            this.check = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(6, 99);
            this.label2.Margin = new System.Windows.Forms.Padding(6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 15);
            this.label2.TabIndex = 30;
            this.label2.Text = "Value:";
            // 
            // check
            // 
            this.check.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.check.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(50)))), ((int)(((byte)(150)))));
            this.check.Location = new System.Drawing.Point(65, 102);
            this.check.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.check.Name = "check";
            this.check.Size = new System.Drawing.Size(12, 12);
            this.check.TabIndex = 31;
            this.check.UseVisualStyleBackColor = true;
            // 
            // BooleanValueControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.Controls.Add(this.check);
            this.Controls.Add(this.label2);
            this.Name = "BooleanValueControl";
            this.Size = new System.Drawing.Size(313, 130);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.check, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox check;
    }
}
