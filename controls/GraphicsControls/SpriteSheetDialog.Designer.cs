namespace SMWControlibControls.GraphicsControls
{
    partial class SpriteSheetDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpriteSheetDialog));
            this.lff = new System.Windows.Forms.Button();
            this.lss = new System.Windows.Forms.Button();
            this.accept = new System.Windows.Forms.Button();
            this.frameSelector = new SMWControlibControls.GraphicsControls.ColoreableBorderComboBox();
            this.lfs = new SMWControlibControls.GraphicsControls.PaletteButton();
            this.paletteBox1 = new SMWControlibControls.GraphicsControls.PaletteBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.w = new System.Windows.Forms.NumericUpDown();
            this.h = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.noLoad = new System.Windows.Forms.CheckBox();
            this.name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.paletteBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.w)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.h)).BeginInit();
            this.SuspendLayout();
            // 
            // lff
            // 
            this.lff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(40)))), ((int)(((byte)(150)))));
            this.lff.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(20)))), ((int)(((byte)(75)))));
            this.lff.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(60)))), ((int)(((byte)(224)))));
            this.lff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lff.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.lff.Location = new System.Drawing.Point(12, 174);
            this.lff.Name = "lff";
            this.lff.Size = new System.Drawing.Size(143, 23);
            this.lff.TabIndex = 20;
            this.lff.Text = "Load From Frame";
            this.lff.UseVisualStyleBackColor = false;
            // 
            // lss
            // 
            this.lss.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(40)))), ((int)(((byte)(150)))));
            this.lss.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(20)))), ((int)(((byte)(75)))));
            this.lss.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(60)))), ((int)(((byte)(224)))));
            this.lss.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lss.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.lss.Location = new System.Drawing.Point(387, 176);
            this.lss.Name = "lss";
            this.lss.Size = new System.Drawing.Size(143, 23);
            this.lss.TabIndex = 22;
            this.lss.Text = "Load Sprite Sheet";
            this.lss.UseVisualStyleBackColor = false;
            // 
            // accept
            // 
            this.accept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(40)))), ((int)(((byte)(150)))));
            this.accept.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(20)))), ((int)(((byte)(75)))));
            this.accept.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(60)))), ((int)(((byte)(224)))));
            this.accept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.accept.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.accept.Location = new System.Drawing.Point(214, 266);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(99, 23);
            this.accept.TabIndex = 23;
            this.accept.Text = "Ok";
            this.accept.UseVisualStyleBackColor = false;
            // 
            // frameSelector
            // 
            this.frameSelector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.frameSelector.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(38)))), ((int)(((byte)(105)))));
            this.frameSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.frameSelector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.frameSelector.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.frameSelector.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(50)))), ((int)(((byte)(150)))));
            this.frameSelector.FormattingEnabled = true;
            this.frameSelector.Location = new System.Drawing.Point(274, 176);
            this.frameSelector.Name = "frameSelector";
            this.frameSelector.Size = new System.Drawing.Size(107, 20);
            this.frameSelector.TabIndex = 19;
            // 
            // lfs
            // 
            this.lfs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(40)))), ((int)(((byte)(150)))));
            this.lfs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(20)))), ((int)(((byte)(75)))));
            this.lfs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(60)))), ((int)(((byte)(224)))));
            this.lfs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lfs.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lfs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(250)))));
            this.lfs.Location = new System.Drawing.Point(12, 146);
            this.lfs.Name = "lfs";
            this.lfs.Size = new System.Drawing.Size(143, 21);
            this.lfs.StartFolder = "";
            this.lfs.TabIndex = 2;
            this.lfs.Text = "Load From Sprite Sheet";
            this.lfs.UseVisualStyleBackColor = false;
            // 
            // paletteBox1
            // 
            this.paletteBox1.FirstPaletteToShow = 8;
            this.paletteBox1.Image = ((System.Drawing.Image)(resources.GetObject("paletteBox1.Image")));
            this.paletteBox1.Location = new System.Drawing.Point(12, 12);
            this.paletteBox1.Name = "paletteBox1";
            this.paletteBox1.Size = new System.Drawing.Size(256, 128);
            this.paletteBox1.TabIndex = 1;
            this.paletteBox1.TabStop = false;
            this.paletteBox1.Zoom = 16;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(274, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(256, 148);
            this.panel1.TabIndex = 24;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 128);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label1.Location = new System.Drawing.Point(270, 205);
            this.label1.Margin = new System.Windows.Forms.Padding(6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 25;
            this.label1.Text = "Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label2.Location = new System.Drawing.Point(270, 237);
            this.label2.Margin = new System.Windows.Forms.Padding(6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 26;
            this.label2.Text = "Height";
            // 
            // w
            // 
            this.w.Location = new System.Drawing.Point(342, 209);
            this.w.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.w.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.w.Name = "w";
            this.w.Size = new System.Drawing.Size(120, 18);
            this.w.TabIndex = 27;
            this.w.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // h
            // 
            this.h.Location = new System.Drawing.Point(342, 237);
            this.h.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.h.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.h.Name = "h";
            this.h.Size = new System.Drawing.Size(120, 18);
            this.h.TabIndex = 28;
            this.h.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label4.Location = new System.Drawing.Point(179, 150);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 12);
            this.label4.TabIndex = 30;
            this.label4.Text = "Don\'t Load";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // noLoad
            // 
            this.noLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.noLoad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(50)))), ((int)(((byte)(150)))));
            this.noLoad.Location = new System.Drawing.Point(161, 150);
            this.noLoad.Name = "noLoad";
            this.noLoad.Size = new System.Drawing.Size(12, 12);
            this.noLoad.TabIndex = 29;
            this.noLoad.UseVisualStyleBackColor = true;
            // 
            // name
            // 
            this.name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(248)))));
            this.name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(32)))), ((int)(((byte)(176)))));
            this.name.Location = new System.Drawing.Point(61, 241);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(187, 18);
            this.name.TabIndex = 32;
            this.name.Text = "Frame";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(180)))));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label3.Location = new System.Drawing.Point(12, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 12);
            this.label3.TabIndex = 31;
            this.label3.Text = "Name:";
            // 
            // SpriteSheetDialog
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(180)))));
            this.ClientSize = new System.Drawing.Size(540, 299);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.noLoad);
            this.Controls.Add(this.h);
            this.Controls.Add(this.w);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.lss);
            this.Controls.Add(this.lff);
            this.Controls.Add(this.frameSelector);
            this.Controls.Add(this.lfs);
            this.Controls.Add(this.paletteBox1);
            this.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SpriteSheetDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SpriteSheetDialog";
            ((System.ComponentModel.ISupportInitialize)(this.paletteBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.w)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.h)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PaletteBox paletteBox1;
        private PaletteButton lfs;
        private ColoreableBorderComboBox frameSelector;
        private System.Windows.Forms.Button lff;
        private System.Windows.Forms.Button lss;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown w;
        private System.Windows.Forms.NumericUpDown h;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox noLoad;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label3;
    }
}