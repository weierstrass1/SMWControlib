﻿namespace SMWControlibControls.GraphicsControls
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
            this.animationSelector = new System.Windows.Forms.ComboBox();
            this.create = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.rename = new System.Windows.Forms.Button();
            this.info = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Animations";
            // 
            // animationSelector
            // 
            this.animationSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.animationSelector.FormattingEnabled = true;
            this.animationSelector.Location = new System.Drawing.Point(9, 34);
            this.animationSelector.Margin = new System.Windows.Forms.Padding(3, 6, 6, 3);
            this.animationSelector.Name = "animationSelector";
            this.animationSelector.Size = new System.Drawing.Size(190, 21);
            this.animationSelector.TabIndex = 2;
            // 
            // create
            // 
            this.create.Location = new System.Drawing.Point(9, 61);
            this.create.Name = "create";
            this.create.Size = new System.Drawing.Size(92, 23);
            this.create.TabIndex = 3;
            this.create.Text = "New";
            this.create.UseVisualStyleBackColor = true;
            // 
            // delete
            // 
            this.delete.Location = new System.Drawing.Point(107, 61);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(92, 23);
            this.delete.TabIndex = 4;
            this.delete.Text = "Delete";
            this.delete.UseVisualStyleBackColor = true;
            // 
            // rename
            // 
            this.rename.Location = new System.Drawing.Point(9, 90);
            this.rename.Name = "rename";
            this.rename.Size = new System.Drawing.Size(92, 23);
            this.rename.TabIndex = 5;
            this.rename.Text = "Rename";
            this.rename.UseVisualStyleBackColor = true;
            // 
            // info
            // 
            this.info.Location = new System.Drawing.Point(107, 90);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(92, 23);
            this.info.TabIndex = 6;
            this.info.Text = "Info";
            this.info.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(9, 122);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(6);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Padding = new System.Windows.Forms.Padding(6);
            this.radioButton1.Size = new System.Drawing.Size(87, 29);
            this.radioButton1.TabIndex = 7;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Only Once";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(107, 122);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(6);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Padding = new System.Windows.Forms.Padding(6);
            this.radioButton2.Size = new System.Drawing.Size(90, 29);
            this.radioButton2.TabIndex = 8;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Continuous";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // AnimationCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.info);
            this.Controls.Add(this.rename);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.create);
            this.Controls.Add(this.animationSelector);
            this.Controls.Add(this.label1);
            this.Name = "AnimationCreator";
            this.Size = new System.Drawing.Size(213, 170);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox animationSelector;
        private System.Windows.Forms.Button create;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Button rename;
        private System.Windows.Forms.Button info;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
    }
}
