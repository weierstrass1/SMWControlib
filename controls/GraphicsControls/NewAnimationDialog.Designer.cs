namespace SMWControlibControls.GraphicsControls
{
    partial class NewAnimationDialog
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
            this.autosel = new System.Windows.Forms.CheckBox();
            this.accept = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // autosel
            // 
            this.autosel.AutoSize = true;
            this.autosel.Location = new System.Drawing.Point(12, 40);
            this.autosel.Name = "autosel";
            this.autosel.Size = new System.Drawing.Size(78, 17);
            this.autosel.TabIndex = 11;
            this.autosel.Text = "AutoSelect";
            this.autosel.UseVisualStyleBackColor = true;
            // 
            // accept
            // 
            this.accept.Location = new System.Drawing.Point(124, 65);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 10;
            this.accept.Text = "Accept";
            this.accept.UseVisualStyleBackColor = true;
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(71, 14);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(230, 20);
            this.name.TabIndex = 7;
            this.name.Text = "Animation0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Name: ";
            // 
            // NewAnimationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 102);
            this.Controls.Add(this.autosel);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NewAnimationDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Animation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox autosel;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label1;
    }
}