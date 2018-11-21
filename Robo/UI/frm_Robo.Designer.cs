namespace Robo.UI
{
    partial class frm_Robo
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
            this.pnlMatBang = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbt_VetBan = new System.Windows.Forms.RadioButton();
            this.rbt_Robo = new System.Windows.Forms.RadioButton();
            this.rbt_Dich = new System.Windows.Forms.RadioButton();
            this.rbt_NgaiVat = new System.Windows.Forms.RadioButton();
            this.btn_Run = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMatBang
            // 
            this.pnlMatBang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMatBang.Location = new System.Drawing.Point(13, 38);
            this.pnlMatBang.Name = "pnlMatBang";
            this.pnlMatBang.Size = new System.Drawing.Size(537, 496);
            this.pnlMatBang.TabIndex = 0;
            this.pnlMatBang.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMatBang_Paint);
            this.pnlMatBang.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlMatBang_MouseClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbt_VetBan);
            this.panel1.Controls.Add(this.rbt_Robo);
            this.panel1.Controls.Add(this.rbt_Dich);
            this.panel1.Controls.Add(this.rbt_NgaiVat);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(646, 324);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(123, 182);
            this.panel1.TabIndex = 5;
            // 
            // rbt_VetBan
            // 
            this.rbt_VetBan.AutoSize = true;
            this.rbt_VetBan.Location = new System.Drawing.Point(15, 134);
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
            this.rbt_Robo.Location = new System.Drawing.Point(15, 17);
            this.rbt_Robo.Name = "rbt_Robo";
            this.rbt_Robo.Size = new System.Drawing.Size(77, 28);
            this.rbt_Robo.TabIndex = 1;
            this.rbt_Robo.TabStop = true;
            this.rbt_Robo.Text = "Robo";
            this.rbt_Robo.UseVisualStyleBackColor = true;
            // 
            // rbt_Dich
            // 
            this.rbt_Dich.AutoSize = true;
            this.rbt_Dich.Location = new System.Drawing.Point(15, 56);
            this.rbt_Dich.Name = "rbt_Dich";
            this.rbt_Dich.Size = new System.Drawing.Size(69, 28);
            this.rbt_Dich.TabIndex = 1;
            this.rbt_Dich.TabStop = true;
            this.rbt_Dich.Text = "Đích";
            this.rbt_Dich.UseVisualStyleBackColor = true;
            // 
            // rbt_NgaiVat
            // 
            this.rbt_NgaiVat.AutoSize = true;
            this.rbt_NgaiVat.Location = new System.Drawing.Point(15, 95);
            this.rbt_NgaiVat.Name = "rbt_NgaiVat";
            this.rbt_NgaiVat.Size = new System.Drawing.Size(102, 28);
            this.rbt_NgaiVat.TabIndex = 1;
            this.rbt_NgaiVat.TabStop = true;
            this.rbt_NgaiVat.Text = "Ngại Vật";
            this.rbt_NgaiVat.UseVisualStyleBackColor = true;
            // 
            // btn_Run
            // 
            this.btn_Run.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Run.Location = new System.Drawing.Point(646, 287);
            this.btn_Run.Name = "btn_Run";
            this.btn_Run.Size = new System.Drawing.Size(123, 31);
            this.btn_Run.TabIndex = 4;
            this.btn_Run.Text = "Run";
            this.btn_Run.UseVisualStyleBackColor = true;
            this.btn_Run.Click += new System.EventHandler(this.btn_Run_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(646, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 34);
            this.button1.TabIndex = 6;
            this.button1.Text = "New";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frm_Robo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 550);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_Run);
            this.Controls.Add(this.pnlMatBang);
            this.Name = "frm_Robo";
            this.Text = "frm_Robo";
            this.Load += new System.EventHandler(this.frm_Robo_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMatBang;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbt_VetBan;
        private System.Windows.Forms.RadioButton rbt_Robo;
        private System.Windows.Forms.RadioButton rbt_Dich;
        private System.Windows.Forms.RadioButton rbt_NgaiVat;
        private System.Windows.Forms.Button btn_Run;
        private System.Windows.Forms.Button button1;
    }
}