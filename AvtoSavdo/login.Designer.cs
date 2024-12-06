using System.Drawing;

namespace AvtoSavdo
{
    partial class login
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
            this.txtparol = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnkirish = new System.Windows.Forms.Button();
            this.linkparoltiklash = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.linkdasturdanchiqish = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // txtparol
            // 
            this.txtparol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtparol.Location = new System.Drawing.Point(64, 55);
            this.txtparol.Name = "txtparol";
            this.txtparol.PasswordChar = '*';
            this.txtparol.Size = new System.Drawing.Size(354, 34);
            this.txtparol.TabIndex = 0;
            this.txtparol.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtparol_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Parolni kiriting:";
            // 
            // btnkirish
            // 
            this.btnkirish.Location = new System.Drawing.Point(161, 95);
            this.btnkirish.Name = "btnkirish";
            this.btnkirish.Size = new System.Drawing.Size(160, 38);
            this.btnkirish.TabIndex = 2;
            this.btnkirish.Text = "Kirish";
            this.btnkirish.UseVisualStyleBackColor = true;
            this.btnkirish.Click += new System.EventHandler(this.btnkirish_Click);
            // 
            // linkparoltiklash
            // 
            this.linkparoltiklash.AutoSize = true;
            this.linkparoltiklash.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkparoltiklash.Location = new System.Drawing.Point(280, 158);
            this.linkparoltiklash.Name = "linkparoltiklash";
            this.linkparoltiklash.Size = new System.Drawing.Size(186, 20);
            this.linkparoltiklash.TabIndex = 3;
            this.linkparoltiklash.TabStop = true;
            this.linkparoltiklash.Text = "Parol esingizda yo\'qmi?";
            this.linkparoltiklash.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkparoltiklash_LinkClicked);
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::AvtoSavdo.Properties.Resources.view;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Location = new System.Drawing.Point(424, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 34);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // linkdasturdanchiqish
            // 
            this.linkdasturdanchiqish.AutoSize = true;
            this.linkdasturdanchiqish.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkdasturdanchiqish.Location = new System.Drawing.Point(12, 158);
            this.linkdasturdanchiqish.Name = "linkdasturdanchiqish";
            this.linkdasturdanchiqish.Size = new System.Drawing.Size(145, 20);
            this.linkdasturdanchiqish.TabIndex = 3;
            this.linkdasturdanchiqish.TabStop = true;
            this.linkdasturdanchiqish.Text = "Dasturdan chiqish";
            this.linkdasturdanchiqish.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkdasturdanchiqish_LinkClicked);
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(478, 187);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.linkdasturdanchiqish);
            this.Controls.Add(this.linkparoltiklash);
            this.Controls.Add(this.btnkirish);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtparol);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "login";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "login";
            this.Load += new System.EventHandler(this.login_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.login_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtparol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnkirish;
        private System.Windows.Forms.LinkLabel linkparoltiklash;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.LinkLabel linkdasturdanchiqish;
    }
}