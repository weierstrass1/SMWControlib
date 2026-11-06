
namespace SMWControlibControls.GraphicsControls
{
    partial class FrameOptimizerDialog
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
            this.components = new System.ComponentModel.Container();
            this.frameShower = new System.Windows.Forms.PictureBox();
            this.cancel = new System.Windows.Forms.Button();
            this.keepConfig = new System.Windows.Forms.CheckBox();
            this.max8x8Tiles = new System.Windows.Forms.ComboBox();
            this.hweight = new System.Windows.Forms.ComboBox();
            this.maxExtraChildrens = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.run = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.frameCounter = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.frameShower)).BeginInit();
            this.SuspendLayout();
            // 
            // frameShower
            // 
            this.frameShower.Location = new System.Drawing.Point(12, 12);
            this.frameShower.Name = "frameShower";
            this.frameShower.Size = new System.Drawing.Size(256, 256);
            this.frameShower.TabIndex = 0;
            this.frameShower.TabStop = false;
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(426, 182);
            this.cancel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(130, 40);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // keepConfig
            // 
            this.keepConfig.AutoSize = true;
            this.keepConfig.Location = new System.Drawing.Point(287, 146);
            this.keepConfig.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.keepConfig.Name = "keepConfig";
            this.keepConfig.Size = new System.Drawing.Size(269, 24);
            this.keepConfig.TabIndex = 2;
            this.keepConfig.Text = "Use this config for the next frame";
            this.keepConfig.UseVisualStyleBackColor = true;
            // 
            // max8x8Tiles
            // 
            this.max8x8Tiles.FormattingEnabled = true;
            this.max8x8Tiles.Items.AddRange(new object[] {
            "No Limit",
            "16",
            "15",
            "14",
            "13",
            "12",
            "11",
            "10",
            "9",
            "8",
            "7",
            "6",
            "5",
            "4",
            "3",
            "2",
            "1",
            "0"});
            this.max8x8Tiles.Location = new System.Drawing.Point(470, 38);
            this.max8x8Tiles.Name = "max8x8Tiles";
            this.max8x8Tiles.Size = new System.Drawing.Size(86, 28);
            this.max8x8Tiles.TabIndex = 3;
            // 
            // hweight
            // 
            this.hweight.FormattingEnabled = true;
            this.hweight.Items.AddRange(new object[] {
            "1",
            "1.1",
            "1.2",
            "1.3",
            "1.4",
            "1.5",
            "1.6",
            "1.7",
            "1.8",
            "1.9",
            "2"});
            this.hweight.Location = new System.Drawing.Point(470, 106);
            this.hweight.Name = "hweight";
            this.hweight.Size = new System.Drawing.Size(86, 28);
            this.hweight.TabIndex = 4;
            // 
            // maxExtraChildrens
            // 
            this.maxExtraChildrens.FormattingEnabled = true;
            this.maxExtraChildrens.Items.AddRange(new object[] {
            "16",
            "15",
            "14",
            "13",
            "12",
            "11",
            "10",
            "9",
            "8",
            "7",
            "6",
            "5",
            "4",
            "3",
            "2",
            "1",
            "0"});
            this.maxExtraChildrens.Location = new System.Drawing.Point(470, 72);
            this.maxExtraChildrens.Name = "maxExtraChildrens";
            this.maxExtraChildrens.Size = new System.Drawing.Size(86, 28);
            this.maxExtraChildrens.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(283, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Optimization Options";
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            // 
            // run
            // 
            this.run.Location = new System.Drawing.Point(287, 182);
            this.run.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.run.Name = "run";
            this.run.Size = new System.Drawing.Size(130, 40);
            this.run.TabIndex = 7;
            this.run.Text = "Run";
            this.run.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(283, 41);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Max. # of 8x8 Tiles";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(283, 75);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Max # of  Extra childrens";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(283, 107);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Heuristic Weight";
            // 
            // frameCounter
            // 
            this.frameCounter.AutoSize = true;
            this.frameCounter.Location = new System.Drawing.Point(12, 280);
            this.frameCounter.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.frameCounter.Name = "frameCounter";
            this.frameCounter.Size = new System.Drawing.Size(85, 20);
            this.frameCounter.TabIndex = 11;
            this.frameCounter.Text = "Frame: 0/0";
            // 
            // FrameOptimizerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 312);
            this.Controls.Add(this.frameCounter);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.run);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.maxExtraChildrens);
            this.Controls.Add(this.hweight);
            this.Controls.Add(this.max8x8Tiles);
            this.Controls.Add(this.keepConfig);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.frameShower);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrameOptimizerDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sprite Sheet Ripper";
            ((System.ComponentModel.ISupportInitialize)(this.frameShower)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox frameShower;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.CheckBox keepConfig;
        private System.Windows.Forms.ComboBox max8x8Tiles;
        private System.Windows.Forms.ComboBox hweight;
        private System.Windows.Forms.ComboBox maxExtraChildrens;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button run;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label frameCounter;
    }
}