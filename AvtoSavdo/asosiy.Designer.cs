using System.Drawing;

namespace AvtoSavdo
{
    partial class asosiy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(asosiy));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tovarlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chiqishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sozlamalarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.picboxshowhide = new System.Windows.Forms.PictureBox();
            this.pnlehtiyotqismlar = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlsavat = new System.Windows.Forms.Panel();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.picsavat = new System.Windows.Forms.PictureBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picboxshowhide)).BeginInit();
            this.pnlehtiyotqismlar.SuspendLayout();
            this.pnlsavat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picsavat)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tovarlarToolStripMenuItem,
            this.savatToolStripMenuItem,
            this.chiqishToolStripMenuItem,
            this.sozlamalarToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1146, 36);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tovarlarToolStripMenuItem
            // 
            this.tovarlarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("tovarlarToolStripMenuItem.Image")));
            this.tovarlarToolStripMenuItem.Name = "tovarlarToolStripMenuItem";
            this.tovarlarToolStripMenuItem.Size = new System.Drawing.Size(177, 32);
            this.tovarlarToolStripMenuItem.Text = "Ehtiyot qismlar";
            this.tovarlarToolStripMenuItem.Click += new System.EventHandler(this.tovarlarToolStripMenuItem_Click);
            // 
            // savatToolStripMenuItem
            // 
            this.savatToolStripMenuItem.Image = global::AvtoSavdo.Properties.Resources.savat;
            this.savatToolStripMenuItem.Name = "savatToolStripMenuItem";
            this.savatToolStripMenuItem.Size = new System.Drawing.Size(94, 32);
            this.savatToolStripMenuItem.Text = "Savat";
            this.savatToolStripMenuItem.Click += new System.EventHandler(this.savatToolStripMenuItem_Click);
            // 
            // chiqishToolStripMenuItem
            // 
            this.chiqishToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.chiqishToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.chiqishToolStripMenuItem.Image = global::AvtoSavdo.Properties.Resources.chiqish;
            this.chiqishToolStripMenuItem.Name = "chiqishToolStripMenuItem";
            this.chiqishToolStripMenuItem.Size = new System.Drawing.Size(110, 32);
            this.chiqishToolStripMenuItem.Text = "Chiqish";
            this.chiqishToolStripMenuItem.Click += new System.EventHandler(this.chiqishToolStripMenuItem_Click);
            // 
            // sozlamalarToolStripMenuItem1
            // 
            this.sozlamalarToolStripMenuItem1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.sozlamalarToolStripMenuItem1.Image = global::AvtoSavdo.Properties.Resources.sozlamalar;
            this.sozlamalarToolStripMenuItem1.Name = "sozlamalarToolStripMenuItem1";
            this.sozlamalarToolStripMenuItem1.Size = new System.Drawing.Size(142, 32);
            this.sozlamalarToolStripMenuItem1.Text = "Sozlamalar";
            this.sozlamalarToolStripMenuItem1.Click += new System.EventHandler(this.sozlamalarToolStripMenuItem1_Click);
            // 
            // picboxshowhide
            // 
            this.picboxshowhide.BackColor = System.Drawing.Color.Transparent;
            this.picboxshowhide.Dock = System.Windows.Forms.DockStyle.Left;
            this.picboxshowhide.Image = global::AvtoSavdo.Properties.Resources.strelkashowhide;
            this.picboxshowhide.Location = new System.Drawing.Point(300, 36);
            this.picboxshowhide.Name = "picboxshowhide";
            this.picboxshowhide.Size = new System.Drawing.Size(30, 541);
            this.picboxshowhide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picboxshowhide.TabIndex = 3;
            this.picboxshowhide.TabStop = false;
            this.picboxshowhide.Click += new System.EventHandler(this.tovarlarToolStripMenuItem_Click);
            // 
            // pnlehtiyotqismlar
            // 
            this.pnlehtiyotqismlar.AutoScroll = true;
            this.pnlehtiyotqismlar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlehtiyotqismlar.Controls.Add(this.comboBox1);
            this.pnlehtiyotqismlar.Controls.Add(this.label13);
            this.pnlehtiyotqismlar.Controls.Add(this.textBox5);
            this.pnlehtiyotqismlar.Controls.Add(this.label12);
            this.pnlehtiyotqismlar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlehtiyotqismlar.Location = new System.Drawing.Point(0, 36);
            this.pnlehtiyotqismlar.Name = "pnlehtiyotqismlar";
            this.pnlehtiyotqismlar.Padding = new System.Windows.Forms.Padding(10);
            this.pnlehtiyotqismlar.Size = new System.Drawing.Size(300, 541);
            this.pnlehtiyotqismlar.TabIndex = 2;
            this.pnlehtiyotqismlar.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox1.Font = new System.Drawing.Font("Lucida Sans Typewriter", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(10, 77);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(280, 23);
            this.comboBox1.Sorted = true;
            this.comboBox1.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(140)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(10, 57);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(280, 20);
            this.label13.TabIndex = 8;
            this.label13.Text = "Mahsulot kategoriyasi";
            this.label13.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // textBox5
            // 
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox5.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.Location = new System.Drawing.Point(10, 30);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(280, 27);
            this.textBox5.TabIndex = 7;
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(140)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(10, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(280, 20);
            this.label12.TabIndex = 6;
            this.label12.Text = "Qidirish";
            this.label12.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // pnlsavat
            // 
            this.pnlsavat.AutoScroll = true;
            this.pnlsavat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlsavat.Controls.Add(this.btnAdd);
            this.pnlsavat.Controls.Add(this.comboBox2);
            this.pnlsavat.Controls.Add(this.label1);
            this.pnlsavat.Controls.Add(this.textBox1);
            this.pnlsavat.Controls.Add(this.label2);
            this.pnlsavat.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlsavat.Location = new System.Drawing.Point(846, 36);
            this.pnlsavat.Name = "pnlsavat";
            this.pnlsavat.Padding = new System.Windows.Forms.Padding(10);
            this.pnlsavat.Size = new System.Drawing.Size(300, 541);
            this.pnlsavat.TabIndex = 4;
            this.pnlsavat.Visible = false;
            // 
            // comboBox2
            // 
            this.comboBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox2.Font = new System.Drawing.Font("Lucida Sans Typewriter", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(10, 77);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(280, 23);
            this.comboBox2.Sorted = true;
            this.comboBox2.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(140)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(10, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Mahsulot kategoriyasi";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(10, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(280, 27);
            this.textBox1.TabIndex = 7;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(140)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(10, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(280, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Qidirish";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // picsavat
            // 
            this.picsavat.BackColor = System.Drawing.Color.Transparent;
            this.picsavat.Dock = System.Windows.Forms.DockStyle.Right;
            this.picsavat.Image = global::AvtoSavdo.Properties.Resources.strelkashowhide;
            this.picsavat.Location = new System.Drawing.Point(816, 36);
            this.picsavat.Name = "picsavat";
            this.picsavat.Size = new System.Drawing.Size(30, 541);
            this.picsavat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picsavat.TabIndex = 5;
            this.picsavat.TabStop = false;
            this.picsavat.Click += new System.EventHandler(this.savatToolStripMenuItem_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = true;
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Lucida Sans Typewriter", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(10, 100);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(280, 39);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "(+) Qo\'shish";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnSotish_Click);
            // 
            // asosiy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::AvtoSavdo.Properties.Resources._157019460811;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1146, 577);
            this.Controls.Add(this.picsavat);
            this.Controls.Add(this.pnlsavat);
            this.Controls.Add(this.picboxshowhide);
            this.Controls.Add(this.pnlehtiyotqismlar);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1106, 614);
            this.Name = "asosiy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AvtoSavdo";
            this.Activated += new System.EventHandler(this.asosiy_Activated);
            this.Load += new System.EventHandler(this.asosiy_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.asosiy_MouseClick);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picboxshowhide)).EndInit();
            this.pnlehtiyotqismlar.ResumeLayout(false);
            this.pnlehtiyotqismlar.PerformLayout();
            this.pnlsavat.ResumeLayout(false);
            this.pnlsavat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picsavat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tovarlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sozlamalarToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem chiqishToolStripMenuItem;
        private System.Windows.Forms.PictureBox picboxshowhide;
        private System.Windows.Forms.Panel pnlehtiyotqismlar;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel pnlsavat;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picsavat;
        private System.Windows.Forms.Button btnAdd;
    }
}

