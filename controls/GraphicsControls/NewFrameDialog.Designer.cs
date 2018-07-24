namespace SMWControlibControls.GraphicsControls
{
    partial class NewFrameDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.isDuplicate = new System.Windows.Forms.CheckBox();
            this.frameSelector = new System.Windows.Forms.ComboBox();
            this.accept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name: ";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(71, 14);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(230, 20);
            this.name.TabIndex = 1;
            this.name.Text = "Frame1";
            // 
            // isDuplicate
            // 
            this.isDuplicate.AutoSize = true;
            this.isDuplicate.Location = new System.Drawing.Point(18, 40);
            this.isDuplicate.Name = "isDuplicate";
            this.isDuplicate.Size = new System.Drawing.Size(77, 17);
            this.isDuplicate.TabIndex = 2;
            this.isDuplicate.Text = "Duplicate?";
            this.isDuplicate.UseVisualStyleBackColor = true;
            // 
            // frameSelector
            // 
            this.frameSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.frameSelector.Enabled = false;
            this.frameSelector.FormattingEnabled = true;
            this.frameSelector.Location = new System.Drawing.Point(101, 38);
            this.frameSelector.Name = "frameSelector";
            this.frameSelector.Size = new System.Drawing.Size(200, 21);
            this.frameSelector.TabIndex = 3;
            // 
            // accept
            // 
            this.accept.Location = new System.Drawing.Point(124, 65);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 4;
            this.accept.Text = "Accept";
            this.accept.UseVisualStyleBackColor = true;
            // 
            // NewFrameDialog
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 100);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.frameSelector);
            this.Controls.Add(this.isDuplicate);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NewFrameDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Frame";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.CheckBox isDuplicate;
        private System.Windows.Forms.ComboBox frameSelector;
        private System.Windows.Forms.Button accept;
    }
}