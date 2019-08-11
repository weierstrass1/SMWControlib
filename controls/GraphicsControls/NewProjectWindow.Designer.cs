namespace SMWControlibControls.GraphicsControls
{
    partial class NewProjectWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProjectWindow));
            this.panel1 = new System.Windows.Forms.Panel();
            this.player = new System.Windows.Forms.RadioButton();
            this.semidynamic = new System.Windows.Forms.RadioButton();
            this.dynamic = new System.Windows.Forms.RadioButton();
            this.standard = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.accept = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.desc = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.player);
            this.panel1.Controls.Add(this.semidynamic);
            this.panel1.Controls.Add(this.dynamic);
            this.panel1.Controls.Add(this.standard);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(273, 273);
            this.panel1.TabIndex = 4;
            // 
            // player
            // 
            this.player.Appearance = System.Windows.Forms.Appearance.Button;
            this.player.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(40)))), ((int)(((byte)(150)))));
            this.player.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(20)))), ((int)(((byte)(75)))));
            this.player.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(60)))), ((int)(((byte)(224)))));
            this.player.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(60)))), ((int)(((byte)(224)))));
            this.player.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.player.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.player.Image = ((System.Drawing.Image)(resources.GetObject("player.Image")));
            this.player.Location = new System.Drawing.Point(137, 138);
            this.player.Name = "player";
            this.player.Padding = new System.Windows.Forms.Padding(0, 0, 0, 9);
            this.player.Size = new System.Drawing.Size(128, 128);
            this.player.TabIndex = 6;
            this.player.Text = "Player or Power Up";
            this.player.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.player.UseVisualStyleBackColor = false;
            this.player.Visible = false;
            // 
            // semidynamic
            // 
            this.semidynamic.Appearance = System.Windows.Forms.Appearance.Button;
            this.semidynamic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(40)))), ((int)(((byte)(150)))));
            this.semidynamic.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(20)))), ((int)(((byte)(75)))));
            this.semidynamic.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(60)))), ((int)(((byte)(224)))));
            this.semidynamic.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(60)))), ((int)(((byte)(224)))));
            this.semidynamic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.semidynamic.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.semidynamic.Image = ((System.Drawing.Image)(resources.GetObject("semidynamic.Image")));
            this.semidynamic.Location = new System.Drawing.Point(3, 137);
            this.semidynamic.Name = "semidynamic";
            this.semidynamic.Padding = new System.Windows.Forms.Padding(0, 0, 0, 9);
            this.semidynamic.Size = new System.Drawing.Size(128, 128);
            this.semidynamic.TabIndex = 5;
            this.semidynamic.Text = "Semi-Dynamic Sprite";
            this.semidynamic.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.semidynamic.UseVisualStyleBackColor = false;
            this.semidynamic.Visible = false;
            // 
            // dynamic
            // 
            this.dynamic.Appearance = System.Windows.Forms.Appearance.Button;
            this.dynamic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(40)))), ((int)(((byte)(150)))));
            this.dynamic.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(20)))), ((int)(((byte)(75)))));
            this.dynamic.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(60)))), ((int)(((byte)(224)))));
            this.dynamic.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(60)))), ((int)(((byte)(224)))));
            this.dynamic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dynamic.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.dynamic.Image = ((System.Drawing.Image)(resources.GetObject("dynamic.Image")));
            this.dynamic.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.dynamic.Location = new System.Drawing.Point(137, 4);
            this.dynamic.Name = "dynamic";
            this.dynamic.Padding = new System.Windows.Forms.Padding(0, 0, 0, 9);
            this.dynamic.Size = new System.Drawing.Size(128, 128);
            this.dynamic.TabIndex = 4;
            this.dynamic.Text = "Dynamic Sprite";
            this.dynamic.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.dynamic.UseVisualStyleBackColor = false;
            // 
            // standard
            // 
            this.standard.Appearance = System.Windows.Forms.Appearance.Button;
            this.standard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(40)))), ((int)(((byte)(150)))));
            this.standard.Checked = true;
            this.standard.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(20)))), ((int)(((byte)(75)))));
            this.standard.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(60)))), ((int)(((byte)(224)))));
            this.standard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(60)))), ((int)(((byte)(224)))));
            this.standard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.standard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.standard.Image = ((System.Drawing.Image)(resources.GetObject("standard.Image")));
            this.standard.Location = new System.Drawing.Point(3, 3);
            this.standard.Name = "standard";
            this.standard.Padding = new System.Windows.Forms.Padding(0, 0, 0, 9);
            this.standard.Size = new System.Drawing.Size(128, 128);
            this.standard.TabIndex = 3;
            this.standard.TabStop = true;
            this.standard.Text = "Standard Sprite";
            this.standard.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.standard.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(180)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.accept);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 273);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(588, 40);
            this.panel2.TabIndex = 5;
            // 
            // accept
            // 
            this.accept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(40)))), ((int)(((byte)(150)))));
            this.accept.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(20)))), ((int)(((byte)(75)))));
            this.accept.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(60)))), ((int)(((byte)(224)))));
            this.accept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.accept.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.accept.Location = new System.Drawing.Point(244, 6);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(99, 23);
            this.accept.TabIndex = 2;
            this.accept.Text = "Ok";
            this.accept.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(180)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.desc);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(273, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(315, 273);
            this.panel3.TabIndex = 6;
            // 
            // desc
            // 
            this.desc.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.desc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.desc.Location = new System.Drawing.Point(4, 32);
            this.desc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.desc.Name = "desc";
            this.desc.Size = new System.Drawing.Size(297, 232);
            this.desc.TabIndex = 2;
            this.desc.Text = "This is a regular sprite that uses graphics preloaded at the start of the level.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Description";
            // 
            // NewProjectWindow
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(588, 313);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewProjectWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Project";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Label desc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton player;
        private System.Windows.Forms.RadioButton semidynamic;
        private System.Windows.Forms.RadioButton dynamic;
        private System.Windows.Forms.RadioButton standard;
    }
}