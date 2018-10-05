namespace SMWControlibControls.LogicControls
{
    partial class OptionValueControl
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
            this.options = new SMWControlibControls.GraphicsControls.ColoreableBorderComboBox();
            this.SuspendLayout();
            // 
            // options
            // 
            this.options.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.options.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(38)))), ((int)(((byte)(105)))));
            this.options.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.options.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.options.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.options.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(50)))), ((int)(((byte)(150)))));
            this.options.FormattingEnabled = true;
            this.options.Items.AddRange(new object[] {
            "X1",
            "X2",
            "X3",
            "X4",
            "X5",
            "X6",
            "X7",
            "X8"});
            this.options.Location = new System.Drawing.Point(125, 99);
            this.options.Name = "options";
            this.options.Size = new System.Drawing.Size(179, 20);
            this.options.TabIndex = 35;
            // 
            // OptionValueControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.Controls.Add(this.options);
            this.Name = "OptionValueControl";
            this.Controls.SetChildIndex(this.options, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GraphicsControls.ColoreableBorderComboBox options;
    }
}
