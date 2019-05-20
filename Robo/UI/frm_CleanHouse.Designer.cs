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
            this.txt_columns = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_rows = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_resize = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbt_VetBan = new System.Windows.Forms.RadioButton();
            this.rbt_Robo = new System.Windows.Forms.RadioButton();
            this.rbt_NgaiVat = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_display
            // 
            this.pnl_display.Location = new System.Drawing.Point(16, 15);
            this.pnl_display.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnl_display.Name = "pnl_display";
            this.pnl_display.Size = new System.Drawing.Size(801, 801);
            this.pnl_display.TabIndex = 0;
            this.pnl_display.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_display_Paint);
            this.pnl_display.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnl_display_MouseClick);
            // 
            // txt_columns
            // 
            this.txt_columns.Location = new System.Drawing.Point(920, 21);
            this.txt_columns.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_columns.Name = "txt_columns";
            this.txt_columns.Size = new System.Drawing.Size(45, 22);
            this.txt_columns.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(840, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "Size";
            // 
            // txt_rows
            // 
            this.txt_rows.Location = new System.Drawing.Point(1013, 21);
            this.txt_rows.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_rows.Name = "txt_rows";
            this.txt_rows.Size = new System.Drawing.Size(45, 22);
            this.txt_rows.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(974, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 31);
            this.label2.TabIndex = 2;
            this.label2.Text = "x";
            // 
            // btn_resize
            // 
            this.btn_resize.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_resize.Location = new System.Drawing.Point(920, 53);
            this.btn_resize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_resize.Name = "btn_resize";
            this.btn_resize.Size = new System.Drawing.Size(140, 43);
            this.btn_resize.TabIndex = 3;
            this.btn_resize.Text = "Resize";
            this.btn_resize.UseVisualStyleBackColor = true;
            this.btn_resize.Click += new System.EventHandler(this.btn_resize_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbt_VetBan);
            this.panel1.Controls.Add(this.rbt_Robo);
            this.panel1.Controls.Add(this.rbt_NgaiVat);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(865, 392);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(137, 158);
            this.panel1.TabIndex = 7;
            // 
            // rbt_VetBan
            // 
            this.rbt_VetBan.AutoSize = true;
            this.rbt_VetBan.Location = new System.Drawing.Point(17, 102);
            this.rbt_VetBan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbt_VetBan.Name = "rbt_VetBan";
            this.rbt_VetBan.Size = new System.Drawing.Size(96, 28);
            this.rbt_VetBan.TabIndex = 2;
            this.rbt_VetBan.TabStop = true;
            this.rbt_VetBan.Text = "Vết bẩn";
            this.rbt_VetBan.UseVisualStyleBackColor = true;
            // 
            // rbt_Robo
            // 
            this.rbt_Robo.AutoSize = true;
            this.rbt_Robo.Location = new System.Drawing.Point(17, 17);
            this.rbt_Robo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbt_Robo.Name = "rbt_Robo";
            this.rbt_Robo.Size = new System.Drawing.Size(77, 28);
            this.rbt_Robo.TabIndex = 1;
            this.rbt_Robo.TabStop = true;
            this.rbt_Robo.Text = "Robo";
            this.rbt_Robo.UseVisualStyleBackColor = true;
            // 
            // rbt_NgaiVat
            // 
            this.rbt_NgaiVat.AutoSize = true;
            this.rbt_NgaiVat.Location = new System.Drawing.Point(17, 63);
            this.rbt_NgaiVat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbt_NgaiVat.Name = "rbt_NgaiVat";
            this.rbt_NgaiVat.Size = new System.Drawing.Size(102, 28);
            this.rbt_NgaiVat.TabIndex = 1;
            this.rbt_NgaiVat.TabStop = true;
            this.rbt_NgaiVat.Text = "Ngại Vật";
            this.rbt_NgaiVat.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(956, 373);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "ver 1.1";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // frm_CleanHouse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 944);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_resize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_rows);
            this.Controls.Add(this.txt_columns);
            this.Controls.Add(this.pnl_display);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "frm_CleanHouse";
            this.Text = "frm_CleanHouse";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_CleanHouse_FormClosing);
            this.Load += new System.EventHandler(this.frm_CleanHouse_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnl_display;
        private System.Windows.Forms.TextBox txt_columns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_rows;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_resize;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbt_VetBan;
        private System.Windows.Forms.RadioButton rbt_Robo;
        private System.Windows.Forms.RadioButton rbt_NgaiVat;
        private System.Windows.Forms.Label label3;
    }
}