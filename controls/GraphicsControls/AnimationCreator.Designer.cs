namespace SMWControlibControls.GraphicsControls
{
    partial class AnimationCreator
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
            this.create = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.rename = new System.Windows.Forms.Button();
            this.info = new System.Windows.Forms.Button();
            this.onlyOnce = new System.Windows.Forms.RadioButton();
            this.continuous = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.animationSelector = new SMWControlibControls.GraphicsControls.ColoreableBorderComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Animations";
            // 
            // create
            // 
            this.create.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(102)))), ((int)(((byte)(64)))));
            this.create.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(51)))), ((int)(((byte)(32)))));
            this.create.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(153)))), ((int)(((byte)(96)))));
            this.create.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.create.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.create.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(255)))), ((int)(((byte)(224)))));
            this.create.Location = new System.Drawing.Point(9, 61);
            this.create.Name = "create";
            this.create.Size = new System.Drawing.Size(92, 23);
            this.create.TabIndex = 3;
            this.create.Text = "New";
            this.create.UseVisualStyleBackColor = false;
            // 
            // delete
            // 
            this.delete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(32)))), ((int)(((byte)(176)))));
            this.delete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(16)))), ((int)(((byte)(88)))));
            this.delete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(48)))), ((int)(((byte)(255)))));
            this.delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delete.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.delete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.delete.Location = new System.Drawing.Point(107, 61);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(92, 23);
            this.delete.TabIndex = 4;
            this.delete.Text = "Delete";
            this.delete.UseVisualStyleBackColor = false;
            // 
            // rename
            // 
            this.rename.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(160)))), ((int)(((byte)(32)))));
            this.rename.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(80)))), ((int)(((byte)(16)))));
            this.rename.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(200)))), ((int)(((byte)(48)))));
            this.rename.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rename.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.rename.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
            this.rename.Location = new System.Drawing.Point(9, 90);
            this.rename.Name = "rename";
            this.rename.Size = new System.Drawing.Size(92, 23);
            this.rename.TabIndex = 5;
            this.rename.Text = "Rename";
            this.rename.UseVisualStyleBackColor = false;
            // 
            // info
            // 
            this.info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(16)))), ((int)(((byte)(32)))));
            this.info.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(8)))), ((int)(((byte)(16)))));
            this.info.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(24)))), ((int)(((byte)(48)))));
            this.info.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.info.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.info.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(160)))), ((int)(((byte)(224)))));
            this.info.Location = new System.Drawing.Point(107, 90);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(92, 23);
            this.info.TabIndex = 6;
            this.info.Text = "Info";
            this.info.UseVisualStyleBackColor = false;
            // 
            // onlyOnce
            // 
            this.onlyOnce.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(40)))), ((int)(((byte)(150)))));
            this.onlyOnce.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.onlyOnce.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.onlyOnce.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(40)))), ((int)(((byte)(150)))));
            this.onlyOnce.Location = new System.Drawing.Point(9, 122);
            this.onlyOnce.Margin = new System.Windows.Forms.Padding(6, 6, 3, 6);
            this.onlyOnce.Name = "onlyOnce";
            this.onlyOnce.Size = new System.Drawing.Size(12, 12);
            this.onlyOnce.TabIndex = 7;
            this.onlyOnce.TabStop = true;
            this.onlyOnce.UseVisualStyleBackColor = true;
            // 
            // continuous
            // 
            this.continuous.Checked = true;
            this.continuous.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(40)))), ((int)(((byte)(150)))));
            this.continuous.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continuous.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.continuous.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(40)))), ((int)(((byte)(150)))));
            this.continuous.Location = new System.Drawing.Point(9, 146);
            this.continuous.Margin = new System.Windows.Forms.Padding(6, 6, 3, 6);
            this.continuous.Name = "continuous";
            this.continuous.Size = new System.Drawing.Size(12, 12);
            this.continuous.TabIndex = 8;
            this.continuous.TabStop = true;
            this.continuous.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label2.Location = new System.Drawing.Point(30, 122);
            this.label2.Margin = new System.Windows.Forms.Padding(6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "Only once";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label3.Location = new System.Drawing.Point(30, 146);
            this.label3.Margin = new System.Windows.Forms.Padding(6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "Continuous";
            // 
            // animationSelector
            // 
            this.animationSelector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.animationSelector.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(38)))), ((int)(((byte)(105)))));
            this.animationSelector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.animationSelector.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.animationSelector.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(50)))), ((int)(((byte)(150)))));
            this.animationSelector.FormattingEnabled = true;
            this.animationSelector.Location = new System.Drawing.Point(9, 35);
            this.animationSelector.Name = "animationSelector";
            this.animationSelector.Size = new System.Drawing.Size(190, 20);
            this.animationSelector.TabIndex = 11;
            // 
            // AnimationCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.animationSelector);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.continuous);
            this.Controls.Add(this.onlyOnce);
            this.Controls.Add(this.info);
            this.Controls.Add(this.rename);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.create);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.Name = "AnimationCreator";
            this.Size = new System.Drawing.Size(221, 182);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button create;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Button rename;
        private System.Windows.Forms.Button info;
        private System.Windows.Forms.RadioButton onlyOnce;
        private System.Windows.Forms.RadioButton continuous;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private ColoreableBorderComboBox animationSelector;
    }
}
