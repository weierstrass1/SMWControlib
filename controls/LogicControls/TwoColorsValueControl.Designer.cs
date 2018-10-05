namespace SMWControlibControls.LogicControls
{
    partial class TwoColorsValueControl
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
            this.label17 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.coloreableBorderComboBox1 = new SMWControlibControls.GraphicsControls.ColoreableBorderComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.color)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 119);
            this.label17.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.label17.Name = "label17";
            this.label17.Padding = new System.Windows.Forms.Padding(3, 6, 2, 6);
            this.label17.Size = new System.Drawing.Size(67, 27);
            this.label17.TabIndex = 38;
            this.label17.Text = "Color 2:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.numericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDown1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(25)))), ((int)(((byte)(75)))));
            this.numericUpDown1.Location = new System.Drawing.Point(77, 126);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(54, 17);
            this.numericUpDown1.TabIndex = 39;
            this.numericUpDown1.Value = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            // 
            // coloreableBorderComboBox1
            // 
            this.coloreableBorderComboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.coloreableBorderComboBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(38)))), ((int)(((byte)(105)))));
            this.coloreableBorderComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coloreableBorderComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.coloreableBorderComboBox1.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.coloreableBorderComboBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(50)))), ((int)(((byte)(150)))));
            this.coloreableBorderComboBox1.FormattingEnabled = true;
            this.coloreableBorderComboBox1.Items.AddRange(new object[] {
            "Red",
            "Green",
            "Blue",
            "Yellow",
            "Purple",
            "Cyan"});
            this.coloreableBorderComboBox1.Location = new System.Drawing.Point(164, 124);
            this.coloreableBorderComboBox1.Name = "coloreableBorderComboBox1";
            this.coloreableBorderComboBox1.Size = new System.Drawing.Size(140, 20);
            this.coloreableBorderComboBox1.TabIndex = 40;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(142, 126);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(8, 3, 3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 41;
            this.pictureBox1.TabStop = false;
            // 
            // TwoColorsValueControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.coloreableBorderComboBox1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label17);
            this.Name = "TwoColorsValueControl";
            this.Size = new System.Drawing.Size(313, 154);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.value, 0);
            this.Controls.SetChildIndex(this.color, 0);
            this.Controls.SetChildIndex(this.options, 0);
            this.Controls.SetChildIndex(this.label17, 0);
            this.Controls.SetChildIndex(this.numericUpDown1, 0);
            this.Controls.SetChildIndex(this.coloreableBorderComboBox1, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.color)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        protected GraphicsControls.ColoreableBorderComboBox coloreableBorderComboBox1;
        protected System.Windows.Forms.PictureBox pictureBox1;
    }
}
