namespace SMWControlibControls.LogicControls
{
    partial class NewHDMADialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.accept = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.type = new SMWControlibControls.GraphicsControls.ColoreableBorderComboBox();
            this.typeDesc = new System.Windows.Forms.TextBox();
            this.option = new SMWControlibControls.GraphicsControls.ColoreableBorderComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.optionDesc = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(248)))));
            this.name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(32)))), ((int)(((byte)(176)))));
            this.name.Location = new System.Drawing.Point(79, 13);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(222, 21);
            this.name.TabIndex = 19;
            this.name.Text = "HDMA0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 18;
            this.label1.Text = "Name: ";
            // 
            // accept
            // 
            this.accept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(40)))), ((int)(((byte)(150)))));
            this.accept.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(20)))), ((int)(((byte)(75)))));
            this.accept.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(60)))), ((int)(((byte)(224)))));
            this.accept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.accept.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.accept.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.accept.Location = new System.Drawing.Point(127, 226);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 21;
            this.accept.Text = "OK";
            this.accept.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(15, 41);
            this.label2.Margin = new System.Windows.Forms.Padding(6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 22;
            this.label2.Text = "Type:";
            // 
            // type
            // 
            this.type.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.type.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(38)))), ((int)(((byte)(105)))));
            this.type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.type.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.type.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.type.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(50)))), ((int)(((byte)(150)))));
            this.type.FormattingEnabled = true;
            this.type.Location = new System.Drawing.Point(79, 40);
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(222, 20);
            this.type.TabIndex = 26;
            // 
            // typeDesc
            // 
            this.typeDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(180)))));
            this.typeDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.typeDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.typeDesc.Location = new System.Drawing.Point(18, 66);
            this.typeDesc.Multiline = true;
            this.typeDesc.Name = "typeDesc";
            this.typeDesc.ReadOnly = true;
            this.typeDesc.Size = new System.Drawing.Size(283, 60);
            this.typeDesc.TabIndex = 27;
            // 
            // option
            // 
            this.option.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.option.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(38)))), ((int)(((byte)(105)))));
            this.option.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.option.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.option.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.option.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(50)))), ((int)(((byte)(150)))));
            this.option.FormattingEnabled = true;
            this.option.Location = new System.Drawing.Point(79, 134);
            this.option.Name = "option";
            this.option.Size = new System.Drawing.Size(222, 20);
            this.option.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(15, 135);
            this.label3.Margin = new System.Windows.Forms.Padding(6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 15);
            this.label3.TabIndex = 28;
            this.label3.Text = "Option:";
            // 
            // optionDesc
            // 
            this.optionDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(180)))));
            this.optionDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.optionDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.optionDesc.Location = new System.Drawing.Point(18, 160);
            this.optionDesc.Multiline = true;
            this.optionDesc.Name = "optionDesc";
            this.optionDesc.ReadOnly = true;
            this.optionDesc.Size = new System.Drawing.Size(283, 60);
            this.optionDesc.TabIndex = 30;
            // 
            // NewHDMADialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(180)))));
            this.ClientSize = new System.Drawing.Size(313, 261);
            this.Controls.Add(this.optionDesc);
            this.Controls.Add(this.option);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.typeDesc);
            this.Controls.Add(this.type);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NewHDMADialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New HDMA";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Label label2;
        private GraphicsControls.ColoreableBorderComboBox type;
        private System.Windows.Forms.TextBox typeDesc;
        private GraphicsControls.ColoreableBorderComboBox option;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox optionDesc;
    }
}