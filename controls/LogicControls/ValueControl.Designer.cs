namespace SMWControlibControls.LogicControls
{
    partial class ValueControl
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.typeDesc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(27, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 6, 6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 19;
            this.label1.Text = "name";
            // 
            // typeDesc
            // 
            this.typeDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(180)))));
            this.typeDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.typeDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.typeDesc.Location = new System.Drawing.Point(9, 27);
            this.typeDesc.Margin = new System.Windows.Forms.Padding(3, 3, 9, 3);
            this.typeDesc.Multiline = true;
            this.typeDesc.Name = "typeDesc";
            this.typeDesc.ReadOnly = true;
            this.typeDesc.Size = new System.Drawing.Size(295, 60);
            this.typeDesc.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 0, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 18);
            this.label2.TabIndex = 29;
            this.label2.Text = "-";
            // 
            // ValueControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(180)))));
            this.Controls.Add(this.label2);
            this.Controls.Add(this.typeDesc);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.MaximumSize = new System.Drawing.Size(313, 1000);
            this.MinimumSize = new System.Drawing.Size(313, 0);
            this.Name = "ValueControl";
            this.Size = new System.Drawing.Size(313, 173);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox typeDesc;
        private System.Windows.Forms.Label label2;
    }
}
