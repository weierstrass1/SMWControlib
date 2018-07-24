namespace SMWControlibControls.GraphicsControls
{
    partial class SpriteGrid
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
            this.cursorPosition = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // cursorPosition
            // 
            this.cursorPosition.AutoSize = true;
            this.cursorPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cursorPosition.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cursorPosition.Location = new System.Drawing.Point(0, 0);
            this.cursorPosition.Name = "cursorPosition";
            this.cursorPosition.Size = new System.Drawing.Size(100, 23);
            this.cursorPosition.TabIndex = 0;
            this.cursorPosition.Text = "X: $00 - Y: $00";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label cursorPosition;
    }
}
