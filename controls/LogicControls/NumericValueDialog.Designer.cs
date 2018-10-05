namespace SMWControlibControls.LogicControls
{
    partial class NumericValueDialog
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
            this.value = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.value)).BeginInit();
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
            this.label2.TabIndex = 29;
            this.label2.Text = "Value:";
            // 
            // value
            // 
            this.value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.value.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(25)))), ((int)(((byte)(75)))));
            this.value.Location = new System.Drawing.Point(65, 100);
            this.value.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.value.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.value.Name = "value";
            this.value.Size = new System.Drawing.Size(54, 17);
            this.value.TabIndex = 34;
            this.value.Value = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            // 
            // NumericValueDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(180)))));
            this.Controls.Add(this.value);
            this.Controls.Add(this.label2);
            this.Name = "NumericValueDialog";
            this.Size = new System.Drawing.Size(313, 130);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.value, 0);
            ((System.ComponentModel.ISupportInitialize)(this.value)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Label label2;
        protected System.Windows.Forms.NumericUpDown value;
    }
}
