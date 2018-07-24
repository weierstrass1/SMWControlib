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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpriteGridController));
            this.spriteGrid1 = new SMWControlibControls.GraphicsControls.SpriteGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grid = new System.Windows.Forms.CheckBox();
            this.settings = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.spriteGrid1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // spriteGrid1
            // 
            this.spriteGrid1.ActivateGrid = false;
            this.spriteGrid1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.spriteGrid1.GridAccuracy = 8;
            this.spriteGrid1.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.spriteGrid1.GridTypeUsed = 0;
            this.spriteGrid1.Image = ((System.Drawing.Image)(resources.GetObject("spriteGrid1.Image")));
            this.spriteGrid1.Location = new System.Drawing.Point(0, 0);
            this.spriteGrid1.Margin = new System.Windows.Forms.Padding(0);
            this.spriteGrid1.MaximumSize = new System.Drawing.Size(512, 480);
            this.spriteGrid1.Name = "spriteGrid1";
            this.spriteGrid1.SelectionBorderColor = System.Drawing.SystemColors.AppWorkspace;
            this.spriteGrid1.SelectionFillColor = System.Drawing.SystemColors.WindowFrame;
            this.spriteGrid1.Size = new System.Drawing.Size(512, 480);
            this.spriteGrid1.TabIndex = 0;
            this.spriteGrid1.TabStop = false;
            this.spriteGrid1.Zoom = 2;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.spriteGrid1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(512, 480);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.settings);
            this.panel2.Controls.Add(this.grid);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 480);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(512, 35);
            this.panel2.TabIndex = 2;
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
            // settings
            // 
            this.settings.Location = new System.Drawing.Point(54, 6);
            this.settings.Name = "settings";
            this.settings.Size = new System.Drawing.Size(75, 23);
            this.settings.TabIndex = 1;
            this.settings.Text = "Grid Settings";
            this.settings.UseVisualStyleBackColor = true;
            // 
            // SpriteGridController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.MaximumSize = new System.Drawing.Size(512, 515);
            this.MinimumSize = new System.Drawing.Size(135, 120);
            this.Name = "SpriteGridController";
            this.Size = new System.Drawing.Size(512, 515);
            ((System.ComponentModel.ISupportInitialize)(this.spriteGrid1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SpriteGrid spriteGrid1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox grid;
        private System.Windows.Forms.Button settings;
    }
}
