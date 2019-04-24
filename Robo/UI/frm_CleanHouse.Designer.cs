namespace Robo.UI
{
    partial class frm_CleanHouse
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
            this.pnl_display = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnl_display
            // 
            this.pnl_display.Location = new System.Drawing.Point(12, 12);
            this.pnl_display.Name = "pnl_display";
            this.pnl_display.Size = new System.Drawing.Size(925, 657);
            this.pnl_display.TabIndex = 0;
            // 
            // frm_CleanHouse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 681);
            this.Controls.Add(this.pnl_display);
            this.Name = "frm_CleanHouse";
            this.Text = "frm_CleanHouse";
            this.Load += new System.EventHandler(this.frm_CleanHouse_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_display;
    }
}