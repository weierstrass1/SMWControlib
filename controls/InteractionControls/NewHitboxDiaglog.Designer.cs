namespace SMWControlibControls.InteractionControls
{
    partial class NewHitboxDiaglog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewHitboxDiaglog));
            this.name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.accept = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.autosel = new System.Windows.Forms.CheckBox();
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
            this.name.TabIndex = 17;
            this.name.Text = "Hitbox0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 16;
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
            this.accept.Location = new System.Drawing.Point(124, 69);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 20;
            this.accept.Text = "OK";
            this.accept.UseVisualStyleBackColor = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(39, 40);
            this.label19.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.label19.Name = "label19";
            this.label19.Padding = new System.Windows.Forms.Padding(3, 6, 2, 6);
            this.label19.Size = new System.Drawing.Size(81, 27);
            this.label19.TabIndex = 24;
            this.label19.Text = "Autoselect";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // autosel
            // 
            this.autosel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.autosel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(50)))), ((int)(((byte)(150)))));
            this.autosel.Location = new System.Drawing.Point(21, 47);
            this.autosel.Margin = new System.Windows.Forms.Padding(12, 3, 0, 3);
            this.autosel.Name = "autosel";
            this.autosel.Size = new System.Drawing.Size(12, 12);
            this.autosel.TabIndex = 23;
            this.autosel.UseVisualStyleBackColor = true;
            // 
            // NewHitboxDiaglog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(180)))));
            this.ClientSize = new System.Drawing.Size(313, 106);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.autosel);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewHitboxDiaglog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Hitbox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox autosel;
    }
}