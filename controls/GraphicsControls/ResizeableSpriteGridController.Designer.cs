namespace SMWControlibControls.GraphicsControls
{
    partial class ResizeableSpriteGridController
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
            this.section1 = new System.Windows.Forms.Panel();
            this.spriteGridController1 = new SMWControlibControls.GraphicsControls.SpriteGridController();
            this.down = new System.Windows.Forms.Panel();
            this.up = new System.Windows.Forms.Panel();
            this.right = new System.Windows.Forms.Panel();
            this.left = new System.Windows.Forms.Panel();
            this.section1.SuspendLayout();
            this.SuspendLayout();
            // 
            // section1
            // 
            this.section1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.section1.Controls.Add(this.spriteGridController1);
            this.section1.Controls.Add(this.down);
            this.section1.Controls.Add(this.up);
            this.section1.Controls.Add(this.right);
            this.section1.Controls.Add(this.left);
            this.section1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.section1.Location = new System.Drawing.Point(0, 0);
            this.section1.Name = "section1";
            this.section1.Size = new System.Drawing.Size(599, 571);
            this.section1.TabIndex = 1;
            // 
            // spriteGridController1
            // 
            this.spriteGridController1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spriteGridController1.Location = new System.Drawing.Point(0, 0);
            this.spriteGridController1.Margin = new System.Windows.Forms.Padding(0);
            this.spriteGridController1.MaximumSize = new System.Drawing.Size(547, 560);
            this.spriteGridController1.MinimumSize = new System.Drawing.Size(439, 120);
            this.spriteGridController1.Name = "spriteGridController1";
            this.spriteGridController1.Size = new System.Drawing.Size(547, 560);
            this.spriteGridController1.TabIndex = 4;
            // 
            // down
            // 
            this.down.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.down.Location = new System.Drawing.Point(0, 567);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(595, 0);
            this.down.TabIndex = 3;
            // 
            // up
            // 
            this.up.Dock = System.Windows.Forms.DockStyle.Top;
            this.up.Location = new System.Drawing.Point(0, 0);
            this.up.Name = "up";
            this.up.Size = new System.Drawing.Size(595, 0);
            this.up.TabIndex = 2;
            // 
            // right
            // 
            this.right.Dock = System.Windows.Forms.DockStyle.Right;
            this.right.Location = new System.Drawing.Point(595, 0);
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(0, 567);
            this.right.TabIndex = 1;
            // 
            // left
            // 
            this.left.Dock = System.Windows.Forms.DockStyle.Left;
            this.left.Location = new System.Drawing.Point(0, 0);
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(0, 567);
            this.left.TabIndex = 0;
            // 
            // ResizeableSpriteGridController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.section1);
            this.Name = "ResizeableSpriteGridController";
            this.Size = new System.Drawing.Size(599, 571);
            this.section1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel section1;
        private SpriteGridController spriteGridController1;
        private System.Windows.Forms.Panel down;
        private System.Windows.Forms.Panel up;
        private System.Windows.Forms.Panel right;
        private System.Windows.Forms.Panel left;
    }
}
