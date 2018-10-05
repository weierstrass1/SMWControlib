namespace SMWControlibControls.LogicControls
{
    partial class OneColorValueControl
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
            ((System.ComponentModel.ISupportInitialize)(this.color)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.value)).BeginInit();
            this.SuspendLayout();
            // 
            // color
            // 
            this.color.Location = new System.Drawing.Point(142, 96);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 96);
            this.label2.Size = new System.Drawing.Size(62, 15);
            this.label2.Text = "Color 1:";
            // 
            // value
            // 
            this.value.Location = new System.Drawing.Point(77, 97);
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
            "Red",
            "Green",
            "Blue",
            "Yellow",
            "Purple",
            "Cyan",
            "White"});
            this.options.Location = new System.Drawing.Point(164, 94);
            this.options.Name = "options";
            this.options.Size = new System.Drawing.Size(140, 20);
            this.options.TabIndex = 37;
            // 
            // OneColorValueControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.Controls.Add(this.options);
            this.Name = "OneColorValueControl";
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.value, 0);
            this.Controls.SetChildIndex(this.color, 0);
            this.Controls.SetChildIndex(this.options, 0);
            ((System.ComponentModel.ISupportInitialize)(this.color)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.value)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected GraphicsControls.ColoreableBorderComboBox options;
    }
}
