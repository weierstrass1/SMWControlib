namespace SMWControlibControls.GraphicsControls
{
    partial class FrameCreator
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
            this.frameSelector = new System.Windows.Forms.ComboBox();
            this.create = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.settings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Frames";
            // 
            // frameSelector
            // 
            this.frameSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.frameSelector.FormattingEnabled = true;
            this.frameSelector.Location = new System.Drawing.Point(9, 34);
            this.frameSelector.Margin = new System.Windows.Forms.Padding(3, 6, 6, 3);
            this.frameSelector.Name = "frameSelector";
            this.frameSelector.Size = new System.Drawing.Size(190, 21);
            this.frameSelector.TabIndex = 1;
            // 
            // create
            // 
            this.create.Location = new System.Drawing.Point(9, 61);
            this.create.Name = "create";
            this.create.Size = new System.Drawing.Size(92, 23);
            this.create.TabIndex = 2;
            this.create.Text = "New";
            this.create.UseVisualStyleBackColor = true;
            // 
            // delete
            // 
            this.delete.Location = new System.Drawing.Point(107, 61);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(92, 23);
            this.delete.TabIndex = 3;
            this.delete.Text = "Delete";
            this.delete.UseVisualStyleBackColor = true;
            // 
            // settings
            // 
            this.settings.Location = new System.Drawing.Point(57, 90);
            this.settings.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.settings.Name = "settings";
            this.settings.Size = new System.Drawing.Size(92, 23);
            this.settings.TabIndex = 4;
            this.settings.Text = "Settings";
            this.settings.UseVisualStyleBackColor = true;
            // 
            // FrameCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.settings);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.create);
            this.Controls.Add(this.frameSelector);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(205, 119);
            this.MinimumSize = new System.Drawing.Size(205, 119);
            this.Name = "FrameCreator";
            this.Size = new System.Drawing.Size(205, 119);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox frameSelector;
        private System.Windows.Forms.Button create;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Button settings;
    }
}
