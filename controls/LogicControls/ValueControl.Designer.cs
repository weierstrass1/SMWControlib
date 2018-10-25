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
            this.name = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.typeDesc = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.name.Location = new System.Drawing.Point(27, 6);
            this.name.Margin = new System.Windows.Forms.Padding(0, 6, 6, 6);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(43, 15);
            this.name.TabIndex = 19;
            this.name.Text = "name";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 0, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 18);
            this.label2.TabIndex = 29;
            this.label2.Text = "-";
            // 
            // typeDesc
            // 
            this.typeDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.typeDesc.Location = new System.Drawing.Point(8, 27);
            this.typeDesc.Name = "typeDesc";
            this.typeDesc.Size = new System.Drawing.Size(295, 60);
            this.typeDesc.TabIndex = 30;
            // 
            // ValueControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(180)))));
            this.Controls.Add(this.typeDesc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.name);
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

        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label typeDesc;
    }
}
