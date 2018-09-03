namespace SMWControlibControls.GraphicsControls
{
    partial class SpriteGridController
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpriteGridController));
            this.panel1 = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.spriteGrid1 = new SMWControlibControls.GraphicsControls.SpriteGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cellSize = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.zoom = new System.Windows.Forms.ComboBox();
            this.settings = new System.Windows.Forms.Button();
            this.grid = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.mirrorH = new SMWControlibControls.GraphicsControls.ImageButton();
            this.mirrorV = new SMWControlibControls.GraphicsControls.ImageButton();
            this.moveDown = new SMWControlibControls.GraphicsControls.ImageButton();
            this.moveLeft = new SMWControlibControls.GraphicsControls.ImageButton();
            this.moveUp = new SMWControlibControls.GraphicsControls.ImageButton();
            this.moveRight = new SMWControlibControls.GraphicsControls.ImageButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.trackBar2);
            this.panel1.Controls.Add(this.spriteGrid1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(561, 489);
            this.panel1.TabIndex = 1;
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(11, 0);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(0);
            this.trackBar1.Maximum = 256;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(535, 20);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.Value = 136;
            // 
            // trackBar2
            // 
            this.trackBar2.AutoSize = false;
            this.trackBar2.LargeChange = 1;
            this.trackBar2.Location = new System.Drawing.Point(0, 11);
            this.trackBar2.Margin = new System.Windows.Forms.Padding(0);
            this.trackBar2.Maximum = 240;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.trackBar2.Size = new System.Drawing.Size(20, 504);
            this.trackBar2.TabIndex = 2;
            this.trackBar2.Value = 120;
            // 
            // spriteGrid1
            // 
            this.spriteGrid1.ActivateCenterSquare = true;
            this.spriteGrid1.ActivateGrid = false;
            this.spriteGrid1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("spriteGrid1.BackgroundImage")));
            this.spriteGrid1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.spriteGrid1.CenterSquareColor = System.Drawing.SystemColors.WindowFrame;
            this.spriteGrid1.GridAccuracy = 8;
            this.spriteGrid1.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.spriteGrid1.GridTypeUsed = 0;
            this.spriteGrid1.Location = new System.Drawing.Point(20, 20);
            this.spriteGrid1.Margin = new System.Windows.Forms.Padding(0);
            this.spriteGrid1.MaximumSize = new System.Drawing.Size(518, 486);
            this.spriteGrid1.MidX = 0;
            this.spriteGrid1.MidY = 0;
            this.spriteGrid1.Name = "spriteGrid1";
            this.spriteGrid1.SelectionBorderColor = System.Drawing.SystemColors.AppWorkspace;
            this.spriteGrid1.SelectionFillColor = System.Drawing.SystemColors.WindowFrame;
            this.spriteGrid1.Size = new System.Drawing.Size(518, 486);
            this.spriteGrid1.TabIndex = 0;
            this.spriteGrid1.Zoom = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cellSize);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.zoom);
            this.panel2.Controls.Add(this.settings);
            this.panel2.Controls.Add(this.grid);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 527);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(561, 35);
            this.panel2.TabIndex = 2;
            // 
            // cellSize
            // 
            this.cellSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cellSize.FormattingEnabled = true;
            this.cellSize.Items.AddRange(new object[] {
            "1X1",
            "2X2",
            "4X4",
            "8X8",
            "16X16"});
            this.cellSize.Location = new System.Drawing.Point(349, 8);
            this.cellSize.Margin = new System.Windows.Forms.Padding(3, 8, 6, 3);
            this.cellSize.Name = "cellSize";
            this.cellSize.Size = new System.Drawing.Size(76, 21);
            this.cellSize.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(281, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(6);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(6);
            this.label2.Size = new System.Drawing.Size(59, 25);
            this.label2.TabIndex = 11;
            this.label2.Text = "Cell Size";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // zoom
            // 
            this.zoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.zoom.FormattingEnabled = true;
            this.zoom.Items.AddRange(new object[] {
            "X1",
            "X2",
            "X3",
            "X4"});
            this.zoom.Location = new System.Drawing.Point(193, 8);
            this.zoom.Margin = new System.Windows.Forms.Padding(3, 8, 6, 3);
            this.zoom.Name = "zoom";
            this.zoom.Size = new System.Drawing.Size(76, 21);
            this.zoom.TabIndex = 10;
            // 
            // settings
            // 
            this.settings.Location = new System.Drawing.Point(54, 6);
            this.settings.Name = "settings";
            this.settings.Size = new System.Drawing.Size(75, 23);
            this.settings.TabIndex = 1;
            this.settings.Text = "Grid Settings";
            this.settings.UseVisualStyleBackColor = true;
            // 
            // grid
            // 
            this.grid.AutoSize = true;
            this.grid.Checked = true;
            this.grid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.grid.Location = new System.Drawing.Point(3, 10);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(45, 17);
            this.grid.TabIndex = 0;
            this.grid.Text = "Grid";
            this.grid.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(138, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(6);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(6);
            this.label1.Size = new System.Drawing.Size(46, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Zoom";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(561, 38);
            this.panel3.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.mirrorH);
            this.panel5.Controls.Add(this.mirrorV);
            this.panel5.Controls.Add(this.moveDown);
            this.panel5.Controls.Add(this.moveLeft);
            this.panel5.Controls.Add(this.moveUp);
            this.panel5.Controls.Add(this.moveRight);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(156, 0);
            this.panel5.MaximumSize = new System.Drawing.Size(234, 38);
            this.panel5.MinimumSize = new System.Drawing.Size(234, 38);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(234, 38);
            this.panel5.TabIndex = 7;
            // 
            // mirrorH
            // 
            this.mirrorH.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mirrorH.BackgroundImage")));
            this.mirrorH.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.mirrorH.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.mirrorH.FlatAppearance.BorderSize = 0;
            this.mirrorH.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.mirrorH.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.mirrorH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mirrorH.Location = new System.Drawing.Point(6, 3);
            this.mirrorH.Name = "mirrorH";
            this.mirrorH.OffSetX1 = 1;
            this.mirrorH.OffSetX2 = 4;
            this.mirrorH.OffSetY1 = 1;
            this.mirrorH.OffSetY2 = 4;
            this.mirrorH.Size = new System.Drawing.Size(32, 32);
            this.mirrorH.Source = global::SMWControlibControls.Properties.Resources.mirrorH;
            this.mirrorH.TabIndex = 0;
            this.mirrorH.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.mirrorH.UseVisualStyleBackColor = true;
            this.mirrorH.Zoom = 2;
            // 
            // mirrorV
            // 
            this.mirrorV.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mirrorV.BackgroundImage")));
            this.mirrorV.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.mirrorV.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.mirrorV.FlatAppearance.BorderSize = 0;
            this.mirrorV.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.mirrorV.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.mirrorV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mirrorV.Location = new System.Drawing.Point(44, 3);
            this.mirrorV.Name = "mirrorV";
            this.mirrorV.OffSetX1 = 1;
            this.mirrorV.OffSetX2 = 4;
            this.mirrorV.OffSetY1 = 1;
            this.mirrorV.OffSetY2 = 4;
            this.mirrorV.Size = new System.Drawing.Size(32, 32);
            this.mirrorV.Source = global::SMWControlibControls.Properties.Resources.mirrorV;
            this.mirrorV.TabIndex = 1;
            this.mirrorV.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.mirrorV.UseVisualStyleBackColor = true;
            this.mirrorV.Zoom = 2;
            // 
            // moveDown
            // 
            this.moveDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("moveDown.BackgroundImage")));
            this.moveDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.moveDown.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.moveDown.FlatAppearance.BorderSize = 0;
            this.moveDown.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.moveDown.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.moveDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moveDown.Location = new System.Drawing.Point(158, 3);
            this.moveDown.Name = "moveDown";
            this.moveDown.OffSetX1 = 1;
            this.moveDown.OffSetX2 = 4;
            this.moveDown.OffSetY1 = 1;
            this.moveDown.OffSetY2 = 4;
            this.moveDown.Size = new System.Drawing.Size(32, 32);
            this.moveDown.Source = global::SMWControlibControls.Properties.Resources.down;
            this.moveDown.TabIndex = 5;
            this.moveDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.moveDown.UseVisualStyleBackColor = true;
            this.moveDown.Zoom = 2;
            // 
            // moveLeft
            // 
            this.moveLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("moveLeft.BackgroundImage")));
            this.moveLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.moveLeft.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.moveLeft.FlatAppearance.BorderSize = 0;
            this.moveLeft.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.moveLeft.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.moveLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moveLeft.Location = new System.Drawing.Point(82, 3);
            this.moveLeft.Name = "moveLeft";
            this.moveLeft.OffSetX1 = 1;
            this.moveLeft.OffSetX2 = 4;
            this.moveLeft.OffSetY1 = 1;
            this.moveLeft.OffSetY2 = 4;
            this.moveLeft.Size = new System.Drawing.Size(32, 32);
            this.moveLeft.Source = global::SMWControlibControls.Properties.Resources.previus;
            this.moveLeft.TabIndex = 2;
            this.moveLeft.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.moveLeft.UseVisualStyleBackColor = true;
            this.moveLeft.Zoom = 2;
            // 
            // moveUp
            // 
            this.moveUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("moveUp.BackgroundImage")));
            this.moveUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.moveUp.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.moveUp.FlatAppearance.BorderSize = 0;
            this.moveUp.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.moveUp.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.moveUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moveUp.Location = new System.Drawing.Point(120, 3);
            this.moveUp.Name = "moveUp";
            this.moveUp.OffSetX1 = 1;
            this.moveUp.OffSetX2 = 4;
            this.moveUp.OffSetY1 = 1;
            this.moveUp.OffSetY2 = 4;
            this.moveUp.Size = new System.Drawing.Size(32, 32);
            this.moveUp.Source = global::SMWControlibControls.Properties.Resources.up;
            this.moveUp.TabIndex = 4;
            this.moveUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.moveUp.UseVisualStyleBackColor = true;
            this.moveUp.Zoom = 2;
            // 
            // moveRight
            // 
            this.moveRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("moveRight.BackgroundImage")));
            this.moveRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.moveRight.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.moveRight.FlatAppearance.BorderSize = 0;
            this.moveRight.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.moveRight.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.moveRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moveRight.Location = new System.Drawing.Point(196, 3);
            this.moveRight.Name = "moveRight";
            this.moveRight.OffSetX1 = 1;
            this.moveRight.OffSetX2 = 4;
            this.moveRight.OffSetY1 = 1;
            this.moveRight.OffSetY2 = 4;
            this.moveRight.Size = new System.Drawing.Size(32, 32);
            this.moveRight.Source = global::SMWControlibControls.Properties.Resources.next;
            this.moveRight.TabIndex = 3;
            this.moveRight.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.moveRight.UseVisualStyleBackColor = true;
            this.moveRight.Zoom = 2;
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(405, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(156, 38);
            this.panel6.TabIndex = 8;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(156, 38);
            this.panel4.TabIndex = 6;
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 3000;
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.InitialDelay = 1500;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 1500;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Info";
            // 
            // SpriteGridController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.MaximumSize = new System.Drawing.Size(561, 562);
            this.MinimumSize = new System.Drawing.Size(135, 120);
            this.Name = "SpriteGridController";
            this.Size = new System.Drawing.Size(561, 562);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SpriteGrid spriteGrid1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox grid;
        private System.Windows.Forms.Button settings;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Panel panel3;
        private ImageButton mirrorH;
        private ImageButton moveRight;
        private ImageButton moveLeft;
        private ImageButton mirrorV;
        private ImageButton moveUp;
        private ImageButton moveDown;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox zoom;
        private System.Windows.Forms.ComboBox cellSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
