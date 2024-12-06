using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvtoSavdo
{
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
        }

        private void malumotlarBazasiBilanBoglashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "MDF files (*.mdf)|*.mdf|All files (*.*)|*.*";
            openFileDialog1.Title = "Ma'lumotlar bazasini tanlang";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string databaseFileName = openFileDialog1.FileName;

                // Tanlangan fayl mavjudligini tekshirish
                if (System.IO.File.Exists(databaseFileName))
                {
                    // Ma'lumotlar bazasiga ulanish
                    connectToDatabase(databaseFileName);
                    connectionString = databaseFileName;
                }
                else
                {
                    // Fayl mavjud emas, yangi ma'lumotlar bazasini yaratish
                    DialogResult result = MessageBox.Show("Tanlangan ma'lumotlar bazasi fayli topilmadi. Yangi ma'lumotlar bazasini yaratishni xohlaysizmi?", "Ma'lumotlar bazasi mavjud emas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Trigger creation of a new database
                        yangiMalumotlarBazasiYaratishToolStripMenuItem_Click(sender, e);
                    }
                }
            }
        }

        private void connectToDatabase(string databaseFileName)
        {
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databaseFileName};Integrated Security=True;Connect Timeout=30";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    saveConnectionString(connectionString);
                    //MessageBox.Show("Ma'lumotlar bazasiga muvaffaqiyatli ulanildi.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xatolik: " + ex.Message);
                }
            }
        }

        private void createDatabase(string databaseFileName)
        {
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True;";
            string databaseName = System.IO.Path.GetFileNameWithoutExtension(databaseFileName);
            string connectionString1 = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // CREATE DATABASE so'rovi
                    string createDatabaseQuery = $"CREATE DATABASE {databaseName} ON PRIMARY (NAME={databaseName}, FILENAME='{databaseFileName}')";
                    SqlCommand command = new SqlCommand(createDatabaseQuery, connection);
                    command.ExecuteNonQuery();

                    //MessageBox.Show("Ma'lumotlar bazasi muvaffaqiyatli yaratildi.");

                    // Ma'lumotlar bazasi yaratildi, endi bog'lanish stringini saqlash
                    connectionString1 = $"Data Source=(LocalDB)\\MSSQLLocalDB;Integrated Security=True;Initial Catalog={databaseName}";

                    // saveConnectionString funksiyasiga connectionString1 ni yuborish
                    saveConnectionString(connectionString1);

                    createTable(connectionString1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xatolik: " + ex.Message);
                }
            }
        }

        private void saveConnectionString(string connectionString)
        {
            string fileName = "connectstring.txt";

            try
            {
                // Faylni yaratish yoki ochish
                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);

                // Faylga connectionString yozish
                sw.WriteLine(connectionString);

                // Resurslarni tozalash
                sw.Close();
                fs.Close();

                //MessageBox.Show("ConnectionString faylga saqlandi: " + fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik yuz berdi: " + ex.Message);
            }
        }

        private string loadConnectionString()
        {
            string fileName = "connectstring.txt";
            string connectionString = "";

            try
            {
                // Faylni tekshirish
                if (File.Exists(fileName))
                {
                    // Faylni ochish
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs);

                    // Fayldan ConnectionString o'qish
                    connectionString = sr.ReadLine();

                    // Resurslarni tozalash
                    sr.Close();
                    fs.Close();

                    //MessageBox.Show("ConnectionString fayldan yuklandi: " + fileName);
                }
                else
                {
                    MessageBox.Show("Fayl mavjud emas: " + fileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik yuz berdi: " + ex.Message);
            }

            return connectionString;
        }

        string connectionString = string.Empty;

        private void admin_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            imagerotateflip(picsettingshowhide);
            // Asosiy.cs dan connection stringni olish
            connectionString = loadConnectionString();

            // Ma'lumotlar bazasiga ulanishni tekshirish
            if (!checkDatabaseConnection(connectionString))
            {
                MessageBox.Show("Ma'lumotlar bazasi joylashuvi aniqlanmadi.\n" +
                        "Ma'lumotlar bazasini qayta bog'lang yoki yarating!",
                        "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                malumotlarBazasiBilanBoglashToolStripMenuItem_Click(sender, e);
            }
            else
            {
                // Bog'lanish muvaffaqiyatli, ehtiyotqismlar jadvalini tekshirish
                if (!checkTableExistence(connectionString))
                {
                    MessageBox.Show("Ma'lumotlar bazasida 'ehtiyotqismlar' jadvali mavjud emas. Jadvalni yaratilmoqda...");
                    createTable(connectionString);
                }
            }
            connectionString = loadConnectionString();

            addproduct(pnlehtiyotqismlar);
        }

        private bool checkDatabaseConnection(string connectionString)
        {
            bool isConnected = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    isConnected = true;
                    //MessageBox.Show("Ma'lumotlar bazasiga muvaffaqiyatli ulanildi.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message);
            }

            return isConnected;
        }

        private bool checkTableExistence(string connectionString)
        {
            bool tableExists = false;
            string tableName = "ehtiyotqismlar";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Jadval mavjudligini tekshirish
                    string query = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'";
                    SqlCommand command = new SqlCommand(query, connection);
                    int count = (int)command.ExecuteScalar();

                    tableExists = (count > 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message, "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return tableExists;
        }

        private void createTable(string connectionString1)
        {
            using (SqlConnection connection = new SqlConnection(connectionString1))
            {
                try
                {
                    connection.Open();

                    // CREATE TABLE so'rovi
                    string createTableQuery = @"
       CREATE TABLE [dbo].[ehtiyotqismlar]
       (
           [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
           [Nom] VARCHAR(MAX) NULL, 
           [Narx] VARCHAR(MAX) NULL, 
           [Rasm] IMAGE NULL, 
           [Miqdor] VARCHAR(MAX) NULL, 
           [Izoh] VARCHAR(MAX) NULL, 
           [Kategoriya] VARCHAR(MAX) NULL, 
           [koordinataX] VARCHAR(MAX) NULL, 
           [koordinataY] VARCHAR(MAX) NULL
       )";
                    SqlCommand command = new SqlCommand(createTableQuery, connection);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Ehtiyotqismlar jadvali muvaffaqiyatli yaratildi.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xatolik: " + ex.Message);
                }
            }
        }

        private void yangiMalumotlarBazasiYaratishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Papka joylashuvini tanlash
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowser.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
                {
                    string folderPath = folderBrowser.SelectedPath;

                    // Ma'lumotlar bazasi nomini so'rash
                    string databaseName = Interaction.InputBox("Ma'lumotlar bazasi nomini kiriting:", "Ma'lumotlar bazasi nomi", "avtoehtiyotqismlar");
                    if (string.IsNullOrWhiteSpace(databaseName))
                        return; // Foydalanuvchi nom kiritmagan

                    // Ma'lumotlar bazasini yaratish
                    string databaseFileName = Path.Combine(folderPath, databaseName + ".mdf");
                    createDatabase(databaseFileName);
                }
            }
        }

        private void adminOynasidanChiqishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void parolniAlmashtirishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Prompt the user to enter the old password
            string oldPassword = Microsoft.VisualBasic.Interaction.InputBox("Eski parolni kiriting:", "Parolni almashtirish");

            // Read the current password from the parol.txt file
            string currentPassword = ReadPasswordFromFile();

            if (oldPassword != currentPassword)
            {
                MessageBox.Show("Eski parol noto'g'ri. Parolni almashtirish bekor qilindi.", "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Prompt the user to enter the new password
            string newPassword = Microsoft.VisualBasic.Interaction.InputBox("Yangi parolni kiriting:", "Parolni almashtirish");

            // Prompt the user to confirm the new password
            string confirmNewPassword = Microsoft.VisualBasic.Interaction.InputBox("Yangi parolni qaytadan kiriting:", "Parolni almashtirish");

            // Check if the new password matches the confirmed password
            if (newPassword != confirmNewPassword)
            {
                MessageBox.Show("Yangi parollar mos kelmadi. Parolni almashtirish bekor qilindi.", "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Write the new password to the parol.txt file
            WritePasswordToFile(newPassword);

            MessageBox.Show("Yangi parol muvaffaqiyatli saqlandi.", "Tasdiq", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string ReadPasswordFromFile()
        {
            string fileName = "parol.txt";
            string password = "";

            try
            {
                if (System.IO.File.Exists(fileName))
                {
                    password = System.IO.File.ReadAllText(fileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message, "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return password;
        }

        private void WritePasswordToFile(string newPassword)
        {
            string fileName = "parol.txt";

            try
            {
                System.IO.File.WriteAllText(fileName, newPassword);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message, "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picboxshowhide_Click(object sender, EventArgs e)
        {
            pnlehtiyotqismlar.Visible = !pnlehtiyotqismlar.Visible;
            imagerotateflip(picboxshowhide);
            if (pnlehtiyotqismlar.Visible)
            {
                addproduct(pnlehtiyotqismlar);
            }
        }
        private void imagerotateflip(PictureBox pictureBox)
        {
            Image originalImage = pictureBox.Image;
            Image mirroredImage = (Image)originalImage.Clone();
            mirroredImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pictureBox.Image = mirroredImage;
        }


        private void addproduct(Panel productpanel)
        {
            try
            {
                // Ma'lumotlar bazasidagi ma'lumotlarni panelga joylash
                productpanel.Controls.Clear(); // Obyektlarni tozalash

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string query = "SELECT * FROM ehtiyotqismlar"; // Barcha ma'lumotlarni olish so'rovi
                        SqlCommand command = new SqlCommand(query, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            // Har bir ma'lumot uchun panelga yangi obyekt qo'shish
                            string id = reader["Id"].ToString();
                            string nom = reader["Nom"].ToString();
                            string narx = reader["Narx"].ToString();
                            byte[] imageByte = (byte[])reader["Rasm"]; // "Rasm" ustunining byte[] qiymatini olamiz
                            Image rasm = ByteArrayToImage(imageByte); // ByteArrayToImage funktsiyasini chaqiramiz
                            string miqdor = reader["Miqdor"].ToString();
                            string izoh = reader["Izoh"].ToString();
                            string kategoriya = reader["Kategoriya"].ToString();
                            string koordinataX = (this.Width * Convert.ToDouble(reader["koordinataX"])).ToString();
                            string koordinataY = (this.Height * Convert.ToDouble(reader["koordinataY"])).ToString();

                            Panel ppanel1 = new System.Windows.Forms.Panel();
                            Label plabel4 = new System.Windows.Forms.Label();
                            Label plabel5 = new System.Windows.Forms.Label();
                            Label plabel3 = new System.Windows.Forms.Label();
                            Label plabel6 = new System.Windows.Forms.Label();
                            Label plabel7 = new System.Windows.Forms.Label();
                            Label plabel8 = new System.Windows.Forms.Label();
                            PictureBox ppictureBox1 = new System.Windows.Forms.PictureBox();
                            Label plabel2 = new System.Windows.Forms.Label();
                            Label plabel1 = new System.Windows.Forms.Label();
                            // 
                            // panel1
                            // 
                            ppanel1.AutoSize = true;
                            ppanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(140)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
                            ppanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                            ppanel1.Controls.Add(plabel7);
                            ppanel1.Controls.Add(plabel6);
                            ppanel1.Controls.Add(plabel4);
                            ppanel1.Controls.Add(plabel5);
                            ppanel1.Controls.Add(plabel3);
                            ppanel1.Controls.Add(ppictureBox1);
                            ppanel1.Controls.Add(plabel2);
                            ppanel1.Controls.Add(plabel1);
                            ppanel1.Controls.Add(plabel8);
                            ppanel1.Dock = DockStyle.Top;
                            ppanel1.Name = "panel1";
                            ppanel1.Size = new System.Drawing.Size(280, 350);
                            ppanel1.TabIndex = 0;
                            // 
                            // label4
                            // 
                            plabel4.Dock = System.Windows.Forms.DockStyle.Top;
                            plabel4.Font = new System.Drawing.Font("Lucida Sans Typewriter", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            plabel4.Location = new System.Drawing.Point(0, 332);
                            plabel4.Name = "label4";
                            plabel4.Size = new System.Drawing.Size(278, 18);
                            plabel4.TabIndex = 7;
                            plabel4.Text = "Kategoriya: " + kategoriya;
                            plabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                            // 
                            // label5
                            // 
                            plabel5.Dock = System.Windows.Forms.DockStyle.Top;
                            plabel5.Font = new System.Drawing.Font("Lucida Sans Typewriter", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            plabel5.Location = new System.Drawing.Point(0, 272);
                            plabel5.Name = "label5";
                            plabel5.Size = new System.Drawing.Size(278, 60);
                            plabel5.TabIndex = 6;
                            plabel5.Text = "Izoh: " + izoh;
                            // 
                            // label3
                            // 
                            plabel3.Dock = System.Windows.Forms.DockStyle.Top;
                            plabel3.Font = new System.Drawing.Font("Lucida Sans Typewriter", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            plabel3.Location = new System.Drawing.Point(0, 214);
                            plabel3.Name = "label3";
                            plabel3.Size = new System.Drawing.Size(278, 18);
                            plabel3.TabIndex = 3;
                            plabel3.Text = "Mavjud: " + miqdor;
                            plabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                            // 
                            // label6
                            // 
                            plabel6.Dock = System.Windows.Forms.DockStyle.Top;
                            plabel6.Font = new System.Drawing.Font("Lucida Sans Typewriter", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            plabel6.Location = new System.Drawing.Point(0, 214);
                            plabel6.Name = "label6";
                            plabel6.Size = new System.Drawing.Size(278, 18);
                            plabel6.TabIndex = 8;
                            plabel6.Text = "x: " + koordinataX;
                            plabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                            // 
                            // label7
                            // 
                            plabel7.Dock = System.Windows.Forms.DockStyle.Top;
                            plabel7.Font = new System.Drawing.Font("Lucida Sans Typewriter", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            plabel7.Location = new System.Drawing.Point(0, 214);
                            plabel7.Name = "label7";
                            plabel7.Size = new System.Drawing.Size(278, 18);
                            plabel7.TabIndex = 9;
                            plabel7.Text = "y: " + koordinataY;
                            plabel7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                            // 
                            // label8
                            // 
                            plabel8.Dock = System.Windows.Forms.DockStyle.Top;
                            plabel8.Font = new System.Drawing.Font("Lucida Sans Typewriter", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            plabel8.Location = new System.Drawing.Point(0, 214);
                            plabel8.Name = "label8";
                            plabel8.Size = new System.Drawing.Size(278, 15);
                            plabel8.TabIndex = 0;
                            plabel8.Text = "Id: " + id;
                            plabel8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                            // 
                            // pictureBox1
                            // 
                            ppictureBox1.BackColor = System.Drawing.Color.White;
                            ppictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
                            ppictureBox1.Location = new System.Drawing.Point(0, 74);
                            ppictureBox1.Name = "pictureBox1";
                            ppictureBox1.Size = new System.Drawing.Size(278, 140);
                            ppictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                            ppictureBox1.Image = rasm;
                            ppictureBox1.TabIndex = 2;
                            ppictureBox1.TabStop = false;
                            // 
                            // label2
                            // 
                            plabel2.Dock = System.Windows.Forms.DockStyle.Top;
                            plabel2.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            plabel2.Location = new System.Drawing.Point(0, 51);
                            plabel2.Name = "label2";
                            plabel2.Size = new System.Drawing.Size(278, 23);
                            plabel2.TabIndex = 1;
                            plabel2.Text = "Narxi: " + narx + " so'm";
                            plabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                            // 
                            // label1
                            // 
                            plabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                            plabel1.Dock = System.Windows.Forms.DockStyle.Top;
                            plabel1.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            plabel1.Location = new System.Drawing.Point(0, 0);
                            plabel1.Name = "label1";
                            plabel1.Size = new System.Drawing.Size(278, 51);
                            plabel1.TabIndex = 0;
                            plabel1.Text = nom;
                            plabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                            foreach (Control control in ppanel1.Controls)
                            {
                                // Har bir kontrol uchun click hodisasini qo'shish
                                control.Click += (sender, e) =>
                                {
                                    productClick(id);
                                };
                            }
                            productpanel.Controls.Add(ppanel1);

                            Panel panelprobel = new Panel();
                            panelprobel.BackColor = Color.Transparent;
                            panelprobel.Size = new Size(200, 10);
                            panelprobel.Margin = new Padding(10);
                            panelprobel.Dock = DockStyle.Top;
                            productpanel.Controls.Add(panelprobel);
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xatolik: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message);
            }
        }

        string selectId = string.Empty;
        private void productClick(string textId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Ma'lumotlarni olish uchun SQL so'rov
                    string selectQuery = "SELECT * FROM ehtiyotqismlar WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@Id", textId);

                    // Ma'lumotlarni olish
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        // Ma'lumotlarni olish
                        string nom1 = reader["Nom"].ToString();
                        string narx1 = reader["Narx"].ToString();
                        byte[] imageByte1 = (byte[])reader["Rasm"]; // "Rasm" ustunining byte[] qiymatini olamiz
                        Image rasm1 = ByteArrayToImage(imageByte1);
                        string miqdor1 = reader["Miqdor"].ToString();
                        string izoh1 = reader["Izoh"].ToString();
                        string kategoriya1 = reader["Kategoriya"].ToString();
                        string koordinataX1 = (this.Width * Convert.ToDouble(reader["koordinataX"])).ToString();
                        string koordinataY1 = (this.Height * Convert.ToDouble(reader["koordinataY"])).ToString();

                        selectId = textId;
                        mid.Text = "ID: " + textId;
                        mnomi.Text = nom1;
                        mnarxi.Text = narx1;
                        KeyPressEventArgs keyPressEventArgs = new KeyPressEventArgs((char)Keys.Space);
                        verguljoylash(mnarxi, keyPressEventArgs);
                        mrasmi.Image = rasm1;
                        mmavjud.Text = miqdor1;
                        KeyPressEventArgs keyPressEventArgs1 = new KeyPressEventArgs((char)Keys.Space);
                        verguljoylash(mmavjud, keyPressEventArgs1);
                        mizoh.Text = izoh1;
                        mkategoriya.Text = kategoriya1;
                        mx.Text = koordinataX1;
                        my.Text = koordinataY1;
                        if (!pnlproductsettings.Visible)
                            picsettingshowhide_Click(null, EventArgs.Empty);
                    }
                    else
                    {
                        MessageBox.Show("Ma'lumot topilmadi.", "Xatolik");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xatolik: " + ex.Message, "Xatolik");
                }
            }
        }


        private void picsettingshowhide_Click(object sender, EventArgs e)
        {
            // pnlproductsettings.Visible qiymatini o'zgartirish
            pnlproductsettings.Visible = !pnlproductsettings.Visible;

            // PictureBox o'zgartirilganligini tekshirish va ifodalash
            imagerotateflip(picsettingshowhide);

            // pnlproductsettings.Visible true bo'lsa va mkategoriya ComboBox-ida elementlar hali yuklanmagan bo'lsa
            if (pnlproductsettings.Visible && mkategoriya.Items.Count == 0)
            {
                // Ma'lumotlar bazasidan kategoriyalar ro'yxatini olish
                List<string> categories = GetUniqueCategoriesFromDatabase();

                // Kategoriyalarni ComboBoxga qo'shish
                foreach (string category in categories)
                {
                    mkategoriya.Items.Add(category);
                }
            }
        }

        // Ma'lumotlar bazasidan kategoriyalarni olish uchun metod
        private List<string> GetUniqueCategoriesFromDatabase()
        {
            List<string> categories = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Ma'lumotlar bazasidan kategoriyalar ustunini olish
                    string query = "SELECT DISTINCT Kategoriya FROM ehtiyotqismlar";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    // Ma'lumotlar ro'yxatini to'plab olish
                    while (reader.Read())
                    {
                        string category = reader["Kategoriya"].ToString();
                        categories.Add(category);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xatolik: " + ex.Message);
                }
            }

            return categories;
        }


        string snomi = string.Empty;
        string snarxi = string.Empty;
        Image simage = null;
        string smavjud = string.Empty;
        string sizoh = string.Empty;
        string skategoriya = string.Empty;
        string smx = string.Empty;
        string smy = string.Empty;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            snomi = mnomi.Text;
            snarxi = vergultozalash(mnarxi);
            simage = mrasmi.Image;
            byte[] imageBytes = ImageToByteArray(simage);
            smavjud = vergultozalash(mmavjud);
            sizoh = mizoh.Text;
            skategoriya = mkategoriya.Text;
            smx = mx.Text;
            smy = my.Text;

            // Ma'lumotlarning bo'sh emasligini tekshirish
            if (string.IsNullOrWhiteSpace(snomi) ||
                string.IsNullOrWhiteSpace(snarxi) ||
                simage == null ||
                string.IsNullOrWhiteSpace(smavjud) ||
                string.IsNullOrWhiteSpace(sizoh) ||
                string.IsNullOrWhiteSpace(skategoriya) ||
                string.IsNullOrWhiteSpace(smx) ||
                string.IsNullOrWhiteSpace(smy))
            {
                MessageBox.Show("Iltimos, barcha ma'lumotlarni to'ldiring.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // INSERT so'rovi tayyorlash
                    string insertQuery = @"
                    INSERT INTO ehtiyotqismlar (Nom, Narx, Rasm, Miqdor, Izoh, Kategoriya, koordinataX, koordinataY)
                    VALUES (@Nom, @Narx, @Rasm, @Miqdor, @Izoh, @Kategoriya, @koordinataX, @koordinataY)
                    ";

                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@Nom", snomi);
                    command.Parameters.AddWithValue("@Narx", snarxi);

                    // Rasmni byte[] massiviga aylantirish
                    command.Parameters.AddWithValue("@Rasm", imageBytes);

                    command.Parameters.AddWithValue("@Miqdor", smavjud);
                    command.Parameters.AddWithValue("@Izoh", sizoh);
                    command.Parameters.AddWithValue("@Kategoriya", skategoriya);
                    command.Parameters.AddWithValue("@koordinataX", relativeX.ToString());
                    command.Parameters.AddWithValue("@koordinataY", relativeY.ToString());

                    // So'rovni bajaring
                    command.ExecuteNonQuery();

                    MessageBox.Show("Ma'lumotlar bazasiga muvaffaqiyatli qo'shildi.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xatolik: " + ex.Message);
                }
            }
            addproduct(pnlehtiyotqismlar);
        }


        // Image obyektini byte[] massiviga aylantiruvchi metod
        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                return memoryStream.ToArray();
            }
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                Image image = Image.FromStream(memoryStream);
                return image;
            }
        }


        private void mrasmi_DoubleClick(object sender, EventArgs e)
        {
            // OpenFileDialog obyekti yaratish
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Fayllarni filtrlash uchun parametrlar
            openFileDialog.Filter = "Rasm fayllari|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            // Dialog sarlavhasini belgilash
            openFileDialog.Title = "Rasm faylini tanlang";

            // Faylni tanlash va foydalanuvchi faylni tanlaganini tekshirish
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Tanlangan faylning manzili
                    string rasmManzili = openFileDialog.FileName;

                    // Tanlangan fayl rasm fayli ekanligini tekshirish
                    if (RasmFayliEkanliginiTekshir(rasmManzili))
                    {
                        // Faylni yuklash
                        Image tanlanganRasm = Image.FromFile(rasmManzili);

                        // PictureBoxning Image xususiyatiga tanlangan rasmni joylash
                        mrasmi.Image = tanlanganRasm;
                    }
                    else
                    {
                        MessageBox.Show("Iltimos, mavjud rasm formatida fayl tanlang.", "Noto'g'ri Fayl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xatolik: " + ex.Message, "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Faylning rasm fayli ekanligini tekshiruvchi yordamchi usul
        private bool RasmFayliEkanliginiTekshir(string faylManzili)
        {
            string kengaytma = Path.GetExtension(faylManzili)?.ToLower();
            return kengaytma == ".jpg" || kengaytma == ".jpeg" || kengaytma == ".png" || kengaytma == ".gif" || kengaytma == ".bmp";
        }

        private void mnarxi_KeyPress(object sender, KeyPressEventArgs e)
        {
            verguljoylash(sender, e);
        }

        private void verguljoylash(object sender, KeyPressEventArgs e)
        {
            // Faqat raqamlarni qabul qilish
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Hodisa bajarilmaydi
            }
            if (sender is TextBox textBox)
            {
                //TextBox textBox = (TextBox)sender;
                string text = textBox.Text.Replace(",", ""); // Vergullarni olib tashlash

                // Yangi harf kiritilganda va matn uzunligi 0 dan katta bo'lsa
                if (text.Length > 0)
                {
                    int len = text.Length;
                    int groupCount = len / 3 - 1; // 3 ta raqamdan keyingi vergullar soni
                    int startIndex = len % 3; // Birinchi verguldan keyin keladigan raqamlar soni
                    StringBuilder sb = new StringBuilder(text);
                    if (char.IsDigit(e.KeyChar))
                    {
                        for (int i = groupCount; i >= 0; i--)
                        {
                            sb.Insert(startIndex + i * 3 + 1, ","); // Har 3 raqamdan keyin vergul qo'shish
                        }
                    }
                    else
                    {
                        for (int i = groupCount; i >= 0; i--)
                        {
                            sb.Insert(startIndex + i * 3, ","); // Har 3 raqamdan keyin vergul qo'shish
                        }
                    }
                    textBox.Text = sb.ToString();
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private void mmavjud_KeyPress(object sender, KeyPressEventArgs e)
        {
            verguljoylash(sender, e);
        }

        private string vergultozalash(object sender)
        {
            if (sender is TextBox textBox)
            {
                string text = textBox.Text.Replace(",", "");
                return text;
            }

            return string.Empty;
        }

        bool xy = false;
        private void btnxy_Click(object sender, EventArgs e)
        {
            if (pnlehtiyotqismlar.Visible)
            {
                picboxshowhide_Click(sender, e);
            }
            if (pnlproductsettings.Visible)
            {
                picsettingshowhide_Click(sender, e);
            }
            xy = true;



        }
        double relativeX = 0;
        double relativeY = 0;
        private void admin_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && xy == true)
            {
                Point point = this.PointToClient(Control.MousePosition);

                // Formaning o'lchami
                int formWidth = this.Width;
                int formHeight = this.Height;

                // Bosilgan nuqta koordinatalari
                int xCoordinate = point.X;
                int yCoordinate = point.Y;

                // Koordinatalarni nisbatini hisoblash
                relativeX = (double)xCoordinate / formWidth;
                relativeY = (double)yCoordinate / formHeight;

                // Koordinatalarni ekranga chiqarish
                mx.Text = (relativeX * formWidth).ToString("0.########"); // 8 xona bilan chiqarish
                my.Text = (relativeY * formHeight).ToString("0.########"); // 8 xona bilan chiqarish
                picsettingshowhide_Click(sender, e);
                xy = false;
            }
        }

        private void admin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27)
            {
                xy = false;
                picsettingshowhide_Click(sender, e);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            mid.Text = "ID: ";
            mnomi.Clear();
            mnarxi.Clear();
            mrasmi.Image = null;
            mmavjud.Clear();
            mizoh.Clear();
            mkategoriya.Text = String.Empty;
            mkategoriya.SelectedIndex = -1;
            mx.Clear();
            my.Clear();
        }

        private void mnarxi_TextChanged(object sender, EventArgs e)
        {

        }

        private void Malumotlarbazasijoylashuvinikorish_Click(object sender, EventArgs e)
        {
            MessageBox.Show(connectionString, "Ma'lumotlar bazasi joylashuvi");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectId))
            {
                if (mid.Text == "ID: ")
                {
                    MessageBox.Show("Iltimos, birorta mahsulot tanlang!");
                    return;
                }
                // Ma'lumotlar bazasidan selectId ga mos ma'lumotni o'chirish
                DeleteData(selectId);
                addproduct(pnlehtiyotqismlar);
            }
        }

        private void DeleteData(string id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // DELETE so'rovi tayyorlash
                    string deleteQuery = "DELETE FROM ehtiyotqismlar WHERE Id = @Id";

                    SqlCommand command = new SqlCommand(deleteQuery, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    // So'rovni bajaring
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Ma'lumot muvaffaqiyatli o'chirildi.");
                        // O'chirilgan ma'lumotni ko'rsatish uchun kerakli amallar
                    }
                    else
                    {
                        MessageBox.Show("O'chirishda xatolik yuz berdi. Ma'lumot topilmadi yoki o'chirilmadi.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xatolik: " + ex.Message);
                }
            }
        }

        private void btnUpgrade_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectId))
            {
                if (mid.Text == "ID: ")
                {
                    MessageBox.Show("Iltimos, birorta mahsulot tanlang!");
                    return;
                }
                // Ma'lumotlarni yangilash uchun selectId ga mos ma'lumotlarni o'zgaruvchilarga o'rnating
                snomi = mnomi.Text;
                snarxi = vergultozalash(mnarxi);
                simage = mrasmi.Image;
                smavjud = vergultozalash(mmavjud);
                sizoh = mizoh.Text;
                skategoriya = mkategoriya.Text;
                smx = mx.Text;
                smy = my.Text;
                if (string.IsNullOrWhiteSpace(snomi) ||
                string.IsNullOrWhiteSpace(snarxi) ||
                simage == null ||
                string.IsNullOrWhiteSpace(smavjud) ||
                string.IsNullOrWhiteSpace(sizoh) ||
                string.IsNullOrWhiteSpace(skategoriya) ||
                string.IsNullOrWhiteSpace(relativeX.ToString()) ||
                string.IsNullOrWhiteSpace(relativeY.ToString()))
                {
                    MessageBox.Show("Iltimos, barcha ma'lumotlarni to'ldiring.");
                    return;
                }
                // Ma'lumotlarni ma'lumotlar bazasida yangilash
                UpdateData(selectId);
                addproduct(pnlehtiyotqismlar);
            }
        }

        private void UpdateData(string id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // UPDATE so'rovi tayyorlash
                    string updateQuery = @"
                UPDATE ehtiyotqismlar 
                SET Nom = @Nom, 
                    Narx = @Narx, 
                    Rasm = @Rasm, 
                    Miqdor = @Miqdor, 
                    Izoh = @Izoh, 
                    Kategoriya = @Kategoriya, 
                    koordinataX = @koordinataX, 
                    koordinataY = @koordinataY 
                WHERE Id = @Id
            ";

                    SqlCommand command = new SqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@Nom", snomi);
                    command.Parameters.AddWithValue("@Narx", snarxi);

                    // Rasmni byte[] massiviga aylantirish
                    byte[] imageBytes = ImageToByteArray(simage);
                    command.Parameters.AddWithValue("@Rasm", imageBytes);

                    command.Parameters.AddWithValue("@Miqdor", smavjud);
                    command.Parameters.AddWithValue("@Izoh", sizoh);
                    command.Parameters.AddWithValue("@Kategoriya", skategoriya);
                    command.Parameters.AddWithValue("@koordinataX", relativeX.ToString());
                    command.Parameters.AddWithValue("@koordinataY", relativeY.ToString());
                    command.Parameters.AddWithValue("@Id", id);

                    // So'rovni bajaring
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Ma'lumotlar muvaffaqiyatli yangilandi.");
                        // Yangilangan ma'lumotlarni ko'rsatish uchun kerakli amallar
                    }
                    else
                    {
                        MessageBox.Show("Yangilashda xatolik yuz berdi. Ma'lumot topilmadi yoki yangilanmadi.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xatolik: " + ex.Message);
                }
            }
        }

        private void admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Close();
                    saveConnectionString(connectionString);
                    //MessageBox.Show("Ma'lumotlar bazasiga muvaffaqiyatli ulanildi.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xatolik: " + ex.Message);
                }
            }
        }
    }
}
