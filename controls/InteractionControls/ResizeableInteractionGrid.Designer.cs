namespace SMWControlibControls.InteractionControls
{
    partial class ResizeableInteractionGrid
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.center = new System.Windows.Forms.Panel();
            this.top = new System.Windows.Forms.Panel();
            this.bottom = new System.Windows.Forms.Panel();
            this.right = new System.Windows.Forms.Panel();
            this.left = new System.Windows.Forms.Panel();
            this.interactionGrid1 = new SMWControlibControls.InteractionControls.InteractionGrid();
            this.panel1.SuspendLayout();
            this.center.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.interactionGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.center);
            this.panel1.Controls.Add(this.top);
            this.panel1.Controls.Add(this.bottom);
            this.panel1.Controls.Add(this.right);
            this.panel1.Controls.Add(this.left);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(638, 380);
            this.panel1.TabIndex = 0;
            // 
            // center
            // 
            this.center.AutoScroll = true;
            this.center.Controls.Add(this.interactionGrid1);
            this.center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.center.Location = new System.Drawing.Point(200, 100);
            this.center.Name = "center";
            this.center.Size = new System.Drawing.Size(238, 180);
            this.center.TabIndex = 4;
            // 
            // top
            // 
            this.top.Dock = System.Windows.Forms.DockStyle.Top;
            this.top.Location = new System.Drawing.Point(200, 0);
            this.top.Name = "top";
            this.top.Size = new System.Drawing.Size(238, 100);
            this.top.TabIndex = 3;
            // 
            // bottom
            // 
            this.bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom.Location = new System.Drawing.Point(200, 280);
            this.bottom.Name = "bottom";
            this.bottom.Size = new System.Drawing.Size(238, 100);
            this.bottom.TabIndex = 2;
            // 
            // right
            // 
            this.right.Dock = System.Windows.Forms.DockStyle.Right;
            this.right.Location = new System.Drawing.Point(438, 0);
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(200, 380);
            this.right.TabIndex = 1;
            // 
            // left
            // 
            this.left.Dock = System.Windows.Forms.DockStyle.Left;
            this.left.Location = new System.Drawing.Point(0, 0);
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(200, 380);
            this.left.TabIndex = 0;
            // 
            // interactionGrid1
            // 
            this.interactionGrid1.ActivateGrid = true;
            this.interactionGrid1.GridAccuracy = 8;
            this.interactionGrid1.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.interactionGrid1.GridTypeUsed = 0;
            this.interactionGrid1.Location = new System.Drawing.Point(0, 0);
            this.interactionGrid1.Name = "interactionGrid1";
            this.interactionGrid1.SelectedFrame = null;
            this.interactionGrid1.SelectedHitbox = null;
            this.interactionGrid1.SelectedInteractionPoint = null;
            this.interactionGrid1.SelectingHitbox = true;
            this.interactionGrid1.Size = new System.Drawing.Size(100, 50);
            this.interactionGrid1.TabIndex = 0;
            this.interactionGrid1.TabStop = false;
            this.interactionGrid1.Zoom = 1;
            // 
            // ResizeableInteractionGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ResizeableInteractionGrid";
            this.Size = new System.Drawing.Size(638, 380);
            this.panel1.ResumeLayout(false);
            this.center.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.interactionGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel top;
        private System.Windows.Forms.Panel bottom;
        private System.Windows.Forms.Panel right;
        private System.Windows.Forms.Panel left;
        private System.Windows.Forms.Panel center;
        private InteractionGrid interactionGrid1;
    }
}
