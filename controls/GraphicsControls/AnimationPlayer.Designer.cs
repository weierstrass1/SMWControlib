namespace SMWControlibControls.GraphicsControls
{
    partial class AnimationPlayer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnimationPlayer));
            this.panel1 = new System.Windows.Forms.Panel();
            this.bottomRight = new System.Windows.Forms.Panel();
            this.bottomLeft = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.top = new System.Windows.Forms.Panel();
            this.midTop = new System.Windows.Forms.Panel();
            this.bottomTop = new System.Windows.Forms.Panel();
            this.topTop = new System.Windows.Forms.Panel();
            this.topRight = new System.Windows.Forms.Panel();
            this.topLeft = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.player = new System.Windows.Forms.PictureBox();
            this.bottom = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.speedBox = new SMWControlibControls.GraphicsControls.ColoreableBorderComboBox();
            this.zoomBox = new SMWControlibControls.GraphicsControls.ColoreableBorderComboBox();
            this.previus = new SMWControlibControls.GraphicsControls.ImageButton();
            this.next = new SMWControlibControls.GraphicsControls.ImageButton();
            this.play = new SMWControlibControls.GraphicsControls.ImageButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.top.SuspendLayout();
            this.midTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            this.bottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bottom);
            this.panel1.Controls.Add(this.bottomRight);
            this.panel1.Controls.Add(this.bottomLeft);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 255);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(529, 52);
            this.panel1.TabIndex = 0;
            // 
            // bottomRight
            // 
            this.bottomRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.bottomRight.Location = new System.Drawing.Point(475, 0);
            this.bottomRight.Name = "bottomRight";
            this.bottomRight.Size = new System.Drawing.Size(54, 52);
            this.bottomRight.TabIndex = 1;
            // 
            // bottomLeft
            // 
            this.bottomLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.bottomLeft.Location = new System.Drawing.Point(0, 0);
            this.bottomLeft.Name = "bottomLeft";
            this.bottomLeft.Size = new System.Drawing.Size(61, 52);
            this.bottomLeft.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.top);
            this.panel2.Controls.Add(this.topRight);
            this.panel2.Controls.Add(this.topLeft);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(529, 255);
            this.panel2.TabIndex = 1;
            // 
            // top
            // 
            this.top.Controls.Add(this.midTop);
            this.top.Controls.Add(this.bottomTop);
            this.top.Controls.Add(this.topTop);
            this.top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.top.Location = new System.Drawing.Point(61, 0);
            this.top.Name = "top";
            this.top.Size = new System.Drawing.Size(414, 255);
            this.top.TabIndex = 2;
            // 
            // midTop
            // 
            this.midTop.AutoScroll = true;
            this.midTop.Controls.Add(this.player);
            this.midTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.midTop.Location = new System.Drawing.Point(0, 36);
            this.midTop.Name = "midTop";
            this.midTop.Size = new System.Drawing.Size(414, 157);
            this.midTop.TabIndex = 3;
            // 
            // bottomTop
            // 
            this.bottomTop.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomTop.Location = new System.Drawing.Point(0, 193);
            this.bottomTop.Name = "bottomTop";
            this.bottomTop.Size = new System.Drawing.Size(414, 62);
            this.bottomTop.TabIndex = 2;
            // 
            // topTop
            // 
            this.topTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.topTop.Location = new System.Drawing.Point(0, 0);
            this.topTop.Name = "topTop";
            this.topTop.Size = new System.Drawing.Size(414, 36);
            this.topTop.TabIndex = 1;
            // 
            // topRight
            // 
            this.topRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.topRight.Location = new System.Drawing.Point(475, 0);
            this.topRight.Name = "topRight";
            this.topRight.Size = new System.Drawing.Size(54, 255);
            this.topRight.TabIndex = 1;
            // 
            // topLeft
            // 
            this.topLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.topLeft.Location = new System.Drawing.Point(0, 0);
            this.topLeft.Name = "topLeft";
            this.topLeft.Size = new System.Drawing.Size(61, 255);
            this.topLeft.TabIndex = 0;
            // 
            // player
            // 
            this.player.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.player.Location = new System.Drawing.Point(0, 0);
            this.player.Margin = new System.Windows.Forms.Padding(0);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(128, 128);
            this.player.TabIndex = 0;
            this.player.TabStop = false;
            // 
            // bottom
            // 
            this.bottom.BackgroundImage = global::SMWControlibControls.Properties.Resources.back1;
            this.bottom.Controls.Add(this.speedBox);
            this.bottom.Controls.Add(this.zoomBox);
            this.bottom.Controls.Add(this.label2);
            this.bottom.Controls.Add(this.label1);
            this.bottom.Controls.Add(this.previus);
            this.bottom.Controls.Add(this.next);
            this.bottom.Controls.Add(this.play);
            this.bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottom.Location = new System.Drawing.Point(61, 0);
            this.bottom.MaximumSize = new System.Drawing.Size(414, 52);
            this.bottom.MinimumSize = new System.Drawing.Size(414, 41);
            this.bottom.Name = "bottom";
            this.bottom.Size = new System.Drawing.Size(414, 52);
            this.bottom.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(180)))));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label2.Location = new System.Drawing.Point(14, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 11, 3, 11);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(6);
            this.label2.Size = new System.Drawing.Size(50, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Zoom";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(180)))));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label1.Location = new System.Drawing.Point(267, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 6, 3, 6);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(6);
            this.label1.Size = new System.Drawing.Size(53, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "Speed";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // speedBox
            // 
            this.speedBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.speedBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(38)))), ((int)(((byte)(105)))));
            this.speedBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.speedBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.speedBox.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.speedBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(50)))), ((int)(((byte)(150)))));
            this.speedBox.FormattingEnabled = true;
            this.speedBox.Items.AddRange(new object[] {
            "X0.125",
            "X0.25",
            "X0.5",
            "X0.75",
            "X1",
            "X1.5",
            "X2",
            "X2.5",
            "X3",
            "X3.5",
            "X4"});
            this.speedBox.Location = new System.Drawing.Point(326, 18);
            this.speedBox.Name = "speedBox";
            this.speedBox.Size = new System.Drawing.Size(71, 20);
            this.speedBox.TabIndex = 11;
            // 
            // zoomBox
            // 
            this.zoomBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.zoomBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(38)))), ((int)(((byte)(105)))));
            this.zoomBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.zoomBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zoomBox.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.zoomBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(50)))), ((int)(((byte)(150)))));
            this.zoomBox.FormattingEnabled = true;
            this.zoomBox.Items.AddRange(new object[] {
            "X1",
            "X2",
            "X3",
            "X4",
            "X5",
            "X6",
            "X7",
            "X8"});
            this.zoomBox.Location = new System.Drawing.Point(70, 17);
            this.zoomBox.Name = "zoomBox";
            this.zoomBox.Size = new System.Drawing.Size(71, 20);
            this.zoomBox.TabIndex = 10;
            // 
            // previus
            // 
            this.previus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("previus.BackgroundImage")));
            this.previus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.previus.Border = ((System.Drawing.Image)(resources.GetObject("previus.Border")));
            this.previus.Clicked = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.previus.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.previus.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.previus.FlatAppearance.BorderSize = 0;
            this.previus.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.previus.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.previus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.previus.Hovered = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.previus.Location = new System.Drawing.Point(150, 11);
            this.previus.Name = "previus";
            this.previus.OffSetX1 = 2;
            this.previus.OffSetX2 = 7;
            this.previus.OffSetY1 = 2;
            this.previus.OffSetY2 = 7;
            this.previus.Size = new System.Drawing.Size(32, 32);
            this.previus.Source = ((System.Drawing.Image)(resources.GetObject("previus.Source")));
            this.previus.TabIndex = 5;
            this.previus.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.previus.UseVisualStyleBackColor = true;
            // 
            // next
            // 
            this.next.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("next.BackgroundImage")));
            this.next.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.next.Border = ((System.Drawing.Image)(resources.GetObject("next.Border")));
            this.next.Clicked = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.next.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.next.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.next.FlatAppearance.BorderSize = 0;
            this.next.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.next.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.next.Hovered = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.next.Location = new System.Drawing.Point(226, 11);
            this.next.Name = "next";
            this.next.OffSetX1 = 2;
            this.next.OffSetX2 = 7;
            this.next.OffSetY1 = 2;
            this.next.OffSetY2 = 7;
            this.next.Size = new System.Drawing.Size(32, 32);
            this.next.Source = ((System.Drawing.Image)(resources.GetObject("next.Source")));
            this.next.TabIndex = 4;
            this.next.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.next.UseVisualStyleBackColor = true;
            // 
            // play
            // 
            this.play.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("play.BackgroundImage")));
            this.play.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.play.Border = ((System.Drawing.Image)(resources.GetObject("play.Border")));
            this.play.Clicked = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.play.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.play.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.play.FlatAppearance.BorderSize = 0;
            this.play.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.play.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.play.Hovered = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.play.Location = new System.Drawing.Point(188, 11);
            this.play.Name = "play";
            this.play.OffSetX1 = 2;
            this.play.OffSetX2 = 7;
            this.play.OffSetY1 = 2;
            this.play.OffSetY2 = 7;
            this.play.Size = new System.Drawing.Size(32, 32);
            this.play.Source = ((System.Drawing.Image)(resources.GetObject("play.Source")));
            this.play.TabIndex = 3;
            this.play.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.play.UseVisualStyleBackColor = true;
            // 
            // AnimationPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("MS Reference Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.Name = "AnimationPlayer";
            this.Size = new System.Drawing.Size(529, 307);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.top.ResumeLayout(false);
            this.midTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            this.bottom.ResumeLayout(false);
            this.bottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel bottom;
        private System.Windows.Forms.Panel bottomRight;
        private System.Windows.Forms.Panel bottomLeft;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel top;
        private System.Windows.Forms.Panel topRight;
        private System.Windows.Forms.Panel topLeft;
        private System.Windows.Forms.PictureBox player;
        private ImageButton play;
        private ImageButton next;
        private ImageButton previus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel midTop;
        private System.Windows.Forms.Panel bottomTop;
        private System.Windows.Forms.Panel topTop;
        private ColoreableBorderComboBox zoomBox;
        private ColoreableBorderComboBox speedBox;
    }
}
