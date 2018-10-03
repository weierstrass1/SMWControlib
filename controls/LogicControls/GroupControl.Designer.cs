namespace SMWControlibControls.LogicControls
{
    partial class GroupControl
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
            this.description = new System.Windows.Forms.Label();
            this.colort = new System.Windows.Forms.Label();
            this.red = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.green = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.blue = new System.Windows.Forms.NumericUpDown();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.color = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.red)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.green)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.color)).BeginInit();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Dock = System.Windows.Forms.DockStyle.Top;
            this.name.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.name.Location = new System.Drawing.Point(0, 0);
            this.name.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.name.Name = "name";
            this.name.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.name.Size = new System.Drawing.Size(64, 30);
            this.name.TabIndex = 2;
            this.name.Text = "Name";
            // 
            // description
            // 
            this.description.Dock = System.Windows.Forms.DockStyle.Top;
            this.description.Location = new System.Drawing.Point(0, 30);
            this.description.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.description.Name = "description";
            this.description.Padding = new System.Windows.Forms.Padding(16, 3, 6, 3);
            this.description.Size = new System.Drawing.Size(351, 37);
            this.description.TabIndex = 30;
            this.description.Text = "Description Description Description Description Description Description Descripti" +
    "on Description Description Description Description Description Description";
            // 
            // colort
            // 
            this.colort.AutoSize = true;
            this.colort.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.colort.Location = new System.Drawing.Point(42, 74);
            this.colort.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.colort.Name = "colort";
            this.colort.Size = new System.Drawing.Size(43, 15);
            this.colort.TabIndex = 32;
            this.colort.Text = "Color";
            // 
            // red
            // 
            this.red.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.red.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.red.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(25)))), ((int)(((byte)(75)))));
            this.red.Location = new System.Drawing.Point(20, 98);
            this.red.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.red.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.red.Name = "red";
            this.red.Size = new System.Drawing.Size(45, 17);
            this.red.TabIndex = 33;
            this.red.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(71, 98);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 15);
            this.label1.TabIndex = 34;
            this.label1.Text = "Red";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(160, 98);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 36;
            this.label2.Text = "Green";
            // 
            // green
            // 
            this.green.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.green.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.green.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(25)))), ((int)(((byte)(75)))));
            this.green.Location = new System.Drawing.Point(109, 98);
            this.green.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.green.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.green.Name = "green";
            this.green.Size = new System.Drawing.Size(45, 17);
            this.green.TabIndex = 35;
            this.green.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(264, 98);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 15);
            this.label3.TabIndex = 38;
            this.label3.Text = "Blue";
            // 
            // blue
            // 
            this.blue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.blue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.blue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(25)))), ((int)(((byte)(75)))));
            this.blue.Location = new System.Drawing.Point(213, 98);
            this.blue.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.blue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.blue.Name = "blue";
            this.blue.Size = new System.Drawing.Size(45, 17);
            this.blue.TabIndex = 37;
            this.blue.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // colorDialog1
            // 
            this.colorDialog1.FullOpen = true;
            // 
            // color
            // 
            this.color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.color.Location = new System.Drawing.Point(20, 73);
            this.color.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.color.Name = "color";
            this.color.Size = new System.Drawing.Size(16, 16);
            this.color.TabIndex = 31;
            this.color.TabStop = false;
            // 
            // GroupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(180)))));
            this.Controls.Add(this.label3);
            this.Controls.Add(this.blue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.green);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.red);
            this.Controls.Add(this.colort);
            this.Controls.Add(this.color);
            this.Controls.Add(this.description);
            this.Controls.Add(this.name);
            this.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "GroupControl";
            this.Size = new System.Drawing.Size(351, 128);
            ((System.ComponentModel.ISupportInitialize)(this.red)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.green)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.color)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label description;
        private System.Windows.Forms.Label colort;
        private System.Windows.Forms.PictureBox color;
        private System.Windows.Forms.NumericUpDown red;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown green;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown blue;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}
