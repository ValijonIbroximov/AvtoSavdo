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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace AvtoSavdo
{
    public partial class asosiy : Form
    {
        public static string ConnectionString { get; private set; } = string.Empty;
        //string ConnectionString = string.Empty;
        private static bool _loginparol = false;

        public static bool loginparol
        {
            get { return _loginparol; }
            set { _loginparol = value; }
        }

        public asosiy()
        {
            InitializeComponent();
        }

        private void asosiy_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            imagerotateflip(picsavat);
            ConnectionString = loadConnectionString();
            //MessageBox.Show(ConnectionString, "cstr");
            if (!checkDatabaseConnection(ConnectionString))
            {
                MessageBox.Show("Ma'lumotlar bazasi joylashuvi aniqlanmadi.\n" +
                        "Admin oynasidan ma'lumotlar bazasini qayta bog'lang!",
                        "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sozlamalarToolStripMenuItem1_Click(sender, e);
            }
            //addproduct(pnlehtiyotqismlar);
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
                    MessageBox.Show("Ma'lumotlar bazasi joylashuvi aniqlanmadi.\n" +
                        "Admin oynasidan ma'lumotlar bazasini qayta bog'lang!",
                        "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sozlamalarToolStripMenuItem1_Click(null, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik yuz berdi: " + ex.Message);
            }
            return connectionString;
        }


        private void chiqishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tovarlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlehtiyotqismlar.Visible = !pnlehtiyotqismlar.Visible;
            imagerotateflip(picboxshowhide);
            if (pnlehtiyotqismlar.Visible)
            {
                addproduct(pnlehtiyotqismlar);
            }
        }

        private void sozlamalarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            loginparol = false;
            login signin = new login();
            signin.ShowDialog();
            if (loginparol)
            {
                admin sozlamalar = new admin();
                sozlamalar.ShowDialog();
                asosiy_Load(sender, e);
            }/*
            else
            {
                Application.Exit();
            }*/
        }

        private void imagerotateflip(PictureBox pictureBox)
        {
            Image originalImage = pictureBox.Image;
            Image mirroredImage = (Image)originalImage.Clone();
            mirroredImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pictureBox.Image = mirroredImage;
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                Image image = Image.FromStream(memoryStream);
                return image;
            }
        }

        // Sinf darajasida KeyPress hodisasini aniqlash
        private void ptextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Agar kiritilgan tugma raqam yoki nazorat (Control) tugmasi bo'lmasa, hodisani bekor qilish
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // hodisani bekor qilish
            }
        }

        private void ptextBox1_TextChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;

            // Agar matn 0 dan farqli bo'lsa va matn bo'sh bo'lsa, unga 0 ni qo'shamiz
            if (textBox.Text == "" || textBox.Text == "0")
            {
                textBox.Text = "0";
                textBox.SelectionStart = textBox.Text.Length;
                return;
            }

            // Agar 0 soni yozilgan bo'lsa va keyinroq son kiritilsa, 0 ni o'chiramiz
            if (textBox.Text == "0" && char.IsDigit(textBox.Text[textBox.Text.Length - 1]))
            {
                textBox.Text = textBox.Text.Substring(1);
                textBox.SelectionStart = textBox.Text.Length; // Kursor matn oxiriga o'tkaziladi
            }
        }






        //const int MaxValue = 9999999999;

        private List<Tuple<int, int>> productList = new List<Tuple<int, int>>();

        private void ppictureBox3_Click(object sender, EventArgs e)
        {
            // Find the ptextBox1 in the parent panel (ppanel2)
            Panel parentPanel = ((PictureBox)sender).Parent as Panel;
            System.Windows.Forms.TextBox ptextBox1 = parentPanel.Controls.OfType<System.Windows.Forms.TextBox>().FirstOrDefault();
            Label plabel8 = parentPanel.Parent.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "label8");
            Label plabel3 = parentPanel.Parent.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "label3");

            if (ptextBox1 != null && int.TryParse(ptextBox1.Text, out int currentValue) && plabel3 != null && int.TryParse(plabel3.Text.Replace("Mavjud: ", ""), out int MaxValue))
            {
                // Check if the current value is less than the maximum allowed value
                if (currentValue < MaxValue)
                {
                    // Increment the value by 1
                    currentValue++;
                    // Update the ptextBox1 with the new value
                    ptextBox1.Text = currentValue.ToString();
                    ptextBox1.SelectionStart = ptextBox1.Text.Length;

                    // Update the productList based on the new value
                    if (plabel8 != null && int.TryParse(plabel8.Text.Replace("Id: ", ""), out int id))
                    {
                        var existingProduct = productList.FirstOrDefault(p => p.Item1 == id);
                        if (existingProduct != null)
                        {
                            productList.Remove(existingProduct);
                        }

                        if (currentValue > 0)
                        {
                            productList.Add(new Tuple<int, int>(id, currentValue));
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Mahsulot yetarli emas.\n" +
                        "Siz mahsulot miqdoridan ortiq mahsulot sota olmaysiz!",
                        "Diqqat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void ppictureBox2_Click(object sender, EventArgs e)
        {
            // Find the ptextBox1 in the parent panel (ppanel2)
            Panel parentPanel = ((PictureBox)sender).Parent as Panel;
            System.Windows.Forms.TextBox ptextBox1 = parentPanel.Controls.OfType<System.Windows.Forms.TextBox>().FirstOrDefault();
            Label plabel8 = parentPanel.Parent.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "label8");

            if (ptextBox1 != null && int.TryParse(ptextBox1.Text, out int currentValue))
            {
                // Check if the current value is greater than 0
                if (currentValue > 0)
                {
                    // Decrement the value by 1
                    currentValue--;
                    // Update the ptextBox1 with the new value
                    ptextBox1.Text = currentValue.ToString();
                    ptextBox1.SelectionStart = ptextBox1.Text.Length;

                    // Update the productList based on the new value
                    if (plabel8 != null && int.TryParse(plabel8.Text.Replace("Id: ", ""), out int id))
                    {
                        var existingProduct = productList.FirstOrDefault(p => p.Item1 == id);
                        if (existingProduct != null)
                        {
                            productList.Remove(existingProduct);
                        }

                        if (currentValue > 0)
                        {
                            productList.Add(new Tuple<int, int>(id, currentValue));
                        }
                    }
                }
            }
        }



        private class Product
        {
            public string Id { get; set; }
            public string Nom { get; set; }
            public string Narx { get; set; }
            public byte[] ImageByte { get; set; }
            public string Miqdor { get; set; }
            public string Izoh { get; set; }
            public string Kategoriya { get; set; }
            public double KoordinataX { get; set; }
            public double KoordinataY { get; set; }
            public double Distance { get; set; }
        }


        private double CalculateDistance(double x1, double y1, double x2, double y2)
        {
            double deltaX = x2 * ((double)this.Width) - x1;
            double deltaY = y2 * ((double)this.Height) - y1;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }

        private void addproduct(Panel productpanel)
        {
            try
            {
                productpanel.Controls.Clear();

                List<Product> products = new List<Product>();

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        connection.Open();

                        string query = "SELECT * FROM ehtiyotqismlar";
                        SqlCommand command = new SqlCommand(query, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                Id = reader["Id"].ToString(),
                                Nom = reader["Nom"].ToString(),
                                Narx = reader["Narx"].ToString(),
                                ImageByte = (byte[])reader["Rasm"],
                                Miqdor = reader["Miqdor"].ToString(),
                                Izoh = reader["Izoh"].ToString(),
                                Kategoriya = reader["Kategoriya"].ToString(),
                                KoordinataX = Convert.ToDouble(reader["koordinataX"]),
                                KoordinataY = Convert.ToDouble(reader["koordinataY"])
                            };

                            product.Distance = CalculateDistance(xCoordinate, yCoordinate, product.KoordinataX, product.KoordinataY);

                            products.Add(product);
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xatolik: " + ex.Message);
                        return;
                    }
                }

                // Mahsulotlarni masofa bo'yicha teskari tartibda saralash
                var sortedProducts = products.OrderByDescending(p => p.Distance).ToList();

                foreach (var product in sortedProducts)
                {
                    Panel ppanel1 = new System.Windows.Forms.Panel();
                    Label plabel4 = new System.Windows.Forms.Label();
                    Label plabel5 = new System.Windows.Forms.Label();
                    Panel ppanel2 = new System.Windows.Forms.Panel();
                    System.Windows.Forms.TextBox ptextBox1 = new System.Windows.Forms.TextBox();
                    PictureBox ppictureBox3 = new System.Windows.Forms.PictureBox();
                    PictureBox ppictureBox2 = new System.Windows.Forms.PictureBox();
                    Label plabel3 = new System.Windows.Forms.Label();
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
                    ppanel1.Controls.Add(plabel4);
                    ppanel1.Controls.Add(plabel5);
                    ppanel1.Controls.Add(ppanel2);
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
                    plabel4.Text = "Kategoriya: " + product.Kategoriya;
                    plabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    plabel4.Visible = false;
                    // 
                    // label5
                    // 
                    plabel5.Dock = System.Windows.Forms.DockStyle.Top;
                    plabel5.Font = new System.Drawing.Font("Lucida Sans Typewriter", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    plabel5.Location = new System.Drawing.Point(0, 272);
                    plabel5.Name = "label5";
                    plabel5.Size = new System.Drawing.Size(278, 60);
                    plabel5.TabIndex = 6;
                    plabel5.Text = "Izoh: " + product.Izoh;
                    plabel5.Visible = false;
                    // 
                    // panel2
                    // 
                    ppanel2.Controls.Add(ptextBox1);
                    ppanel2.Controls.Add(ppictureBox3);
                    ppanel2.Controls.Add(ppictureBox2);
                    ppanel2.Dock = System.Windows.Forms.DockStyle.Top;
                    ppanel2.Location = new System.Drawing.Point(0, 232);
                    ppanel2.Name = "panel2";
                    ppanel2.Size = new System.Drawing.Size(278, 40);
                    ppanel2.TabIndex = 4;
                    // 
                    // textBox1
                    // 
                    ptextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
                    ptextBox1.Font = new System.Drawing.Font("Lucida Sans Unicode", 16.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    ptextBox1.Location = new System.Drawing.Point(40, 0);
                    ptextBox1.Name = "textBox1";
                    ptextBox1.Size = new System.Drawing.Size(198, 51);
                    ptextBox1.TabIndex = 2;
                    ptextBox1.Text = "0";
                    ptextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                    ptextBox1.MaxLength = 10;
                    ptextBox1.KeyPress += new KeyPressEventHandler(ptextBox1_KeyPress);
                    ptextBox1.TextChanged += new EventHandler(ptextBox1_TextChanged);

                    var existingProduct = productList.FirstOrDefault(p => p.Item1.ToString() == product.Id);
                    if (existingProduct != null)
                    {
                        ptextBox1.Text = existingProduct.Item2.ToString();
                    }
                    // 
                    // pictureBox3
                    // 
                    ppictureBox3.Dock = System.Windows.Forms.DockStyle.Right;
                    ppictureBox3.Image = global::AvtoSavdo.Properties.Resources.plus;
                    ppictureBox3.Location = new System.Drawing.Point(238, 0);
                    ppictureBox3.Name = "pictureBox3";
                    ppictureBox3.Size = new System.Drawing.Size(40, 40);
                    ppictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    ppictureBox3.TabIndex = 1;
                    ppictureBox3.TabStop = false;
                    ppictureBox3.Click += new EventHandler(ppictureBox3_Click);
                    // 
                    // pictureBox2
                    // 
                    ppictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
                    ppictureBox2.Image = global::AvtoSavdo.Properties.Resources.minus;
                    ppictureBox2.Location = new System.Drawing.Point(0, 0);
                    ppictureBox2.Name = "pictureBox2";
                    ppictureBox2.Size = new System.Drawing.Size(40, 40);
                    ppictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    ppictureBox2.TabIndex = 0;
                    ppictureBox2.TabStop = false;
                    ppictureBox2.Click += new EventHandler(ppictureBox2_Click);
                    // 
                    // label3
                    // 
                    plabel3.Dock = System.Windows.Forms.DockStyle.Top;
                    plabel3.Font = new System.Drawing.Font("Lucida Sans Typewriter", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    plabel3.Location = new System.Drawing.Point(0, 214);
                    plabel3.Name = "label3";
                    plabel3.Size = new System.Drawing.Size(278, 18);
                    plabel3.TabIndex = 3;
                    plabel3.Text = "Mavjud: " + product.Miqdor;
                    plabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    // 
                    // label8
                    // 
                    plabel8.Dock = System.Windows.Forms.DockStyle.Top;
                    plabel8.Font = new System.Drawing.Font("Lucida Sans Typewriter", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    plabel8.Location = new System.Drawing.Point(0, 214);
                    plabel8.Name = "label8";
                    plabel8.Size = new System.Drawing.Size(278, 15);
                    plabel8.TabIndex = 0;
                    plabel8.Text = "Id: " + product.Id;
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
                    ppictureBox1.Image = ByteArrayToImage(product.ImageByte);
                    ppictureBox1.TabIndex = 2;
                    ppictureBox1.TabStop = false;
                    ppictureBox1.Click += (sender, e) =>
                    {
                        PictureBox clickedPictureBox = sender as PictureBox;
                        Panel parentPanel = clickedPictureBox.Parent as Panel;

                        plabel4.Visible = !plabel4.Visible;
                        plabel5.Visible = !plabel5.Visible;
                    };
                    // 
                    // label2
                    // 
                    plabel2.Dock = System.Windows.Forms.DockStyle.Top;
                    plabel2.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    plabel2.Location = new System.Drawing.Point(0, 51);
                    plabel2.Name = "label2";
                    plabel2.Size = new System.Drawing.Size(278, 23);
                    plabel2.TabIndex = 1;
                    plabel2.Text = "Narxi: " + product.Narx + " so'm";
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
                    plabel1.Text = product.Nom;
                    plabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    /*
                    foreach (Control control in ppanel1.Controls)
                    {
                        // Har bir kontrol uchun click hodisasini qo'shish
                        control.Click += (sender, e) =>
                        {
                            productClick(id);
                        };
                    }
                    */
                    productpanel.Controls.Add(ppanel1);

                    Panel panelprobel = new Panel();
                    panelprobel.BackColor = Color.Transparent;
                    panelprobel.Size = new Size(200, 10);
                    panelprobel.Margin = new Padding(10);
                    panelprobel.Dock = DockStyle.Top;
                    productpanel.Controls.Add(panelprobel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message);
            }
        }

        bool formclick = false;
        private void asosiy_Activated(object sender, EventArgs e)
        {
            formclick = !pnlehtiyotqismlar.Visible;
        }

        double xCoordinate = 0;
        double yCoordinate = 0;
        private void asosiy_MouseClick(object sender, MouseEventArgs e)
        {
            if (formclick)
            {
                Point point = this.PointToClient(Control.MousePosition);

                xCoordinate = (double)point.X;
                yCoordinate = (double)point.Y;

                tovarlarToolStripMenuItem_Click(sender, e);
            }
        }

        private void savatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlsavat.Visible = !pnlsavat.Visible;

            imagerotateflip(picsavat);

            if (pnlsavat.Visible)
            {
                addproductsavat(pnlsavat);
            }
        }

        private void addproductsavat(Panel productpanel)
        {
            try
            {
                productpanel.Controls.Clear();

                List<Product> products = new List<Product>();

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        connection.Open();

                        string query = "SELECT * FROM ehtiyotqismlar";
                        SqlCommand command = new SqlCommand(query, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                Id = reader["Id"].ToString(),
                                Nom = reader["Nom"].ToString(),
                                Narx = reader["Narx"].ToString(),
                                ImageByte = (byte[])reader["Rasm"],
                                Miqdor = reader["Miqdor"].ToString(),
                                Izoh = reader["Izoh"].ToString(),
                                Kategoriya = reader["Kategoriya"].ToString(),
                                KoordinataX = Convert.ToDouble(reader["koordinataX"]),
                                KoordinataY = Convert.ToDouble(reader["koordinataY"])
                            };

                            int productId = Convert.ToInt32(product.Id);
                            if (productList.All(p => p.Item1 != productId))
                            {
                                // Mahsulot ro'yxatida mavjud emas, shuning uchun qo'shamiz
                                productList.Add(new Tuple<int, int>(productId, 0));
                            }

                            product.Distance = CalculateDistance(xCoordinate, yCoordinate, product.KoordinataX, product.KoordinataY);

                            products.Add(product);
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xatolik: " + ex.Message);
                        return;
                    }
                }

                // Mahsulotlarni masofa bo'yicha teskari tartibda saralash
                var sortedProducts = products.OrderByDescending(p => p.Distance).ToList();

                foreach (var product in sortedProducts)
                {
                    int productId = Convert.ToInt32(product.Id); 

                    // Mahsulot ro'yxatida mavjud bo'lmagan, yoki miqdori 0 ga teng bo'lgan mahsulotlarni qo'shib yubormaslik uchun tekshiramiz
                    if (productList.All(p => p.Item1 != productId) || productList.Any(p => p.Item1 == productId && p.Item2 != 0))
                    {
                        Panel ppanel1 = new System.Windows.Forms.Panel();
                        Label plabel4 = new System.Windows.Forms.Label();
                        Label plabel5 = new System.Windows.Forms.Label();
                        Panel ppanel2 = new System.Windows.Forms.Panel();
                        System.Windows.Forms.TextBox ptextBox1 = new System.Windows.Forms.TextBox();
                        PictureBox ppictureBox3 = new System.Windows.Forms.PictureBox();
                        PictureBox ppictureBox2 = new System.Windows.Forms.PictureBox();
                        Label plabel3 = new System.Windows.Forms.Label();
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
                        ppanel1.Controls.Add(plabel4);
                        ppanel1.Controls.Add(plabel5);
                        ppanel1.Controls.Add(ppanel2);
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
                        plabel4.Text = "Kategoriya: " + product.Kategoriya;
                        plabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        plabel4.Visible = false;
                        // 
                        // label5
                        // 
                        plabel5.Dock = System.Windows.Forms.DockStyle.Top;
                        plabel5.Font = new System.Drawing.Font("Lucida Sans Typewriter", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        plabel5.Location = new System.Drawing.Point(0, 272);
                        plabel5.Name = "label5";
                        plabel5.Size = new System.Drawing.Size(278, 60);
                        plabel5.TabIndex = 6;
                        plabel5.Text = "Izoh: " + product.Izoh;
                        plabel5.Visible = false;
                        // 
                        // panel2
                        // 
                        ppanel2.Controls.Add(ptextBox1);
                        ppanel2.Controls.Add(ppictureBox3);
                        ppanel2.Controls.Add(ppictureBox2);
                        ppanel2.Dock = System.Windows.Forms.DockStyle.Top;
                        ppanel2.Location = new System.Drawing.Point(0, 232);
                        ppanel2.Name = "panel2";
                        ppanel2.Size = new System.Drawing.Size(278, 40);
                        ppanel2.TabIndex = 4;
                        // 
                        // textBox1
                        // 
                        ptextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
                        ptextBox1.Font = new System.Drawing.Font("Lucida Sans Unicode", 16.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        ptextBox1.Location = new System.Drawing.Point(40, 0);
                        ptextBox1.Name = "textBox1";
                        ptextBox1.Size = new System.Drawing.Size(198, 51);
                        ptextBox1.TabIndex = 2;
                        ptextBox1.Text = "0";
                        ptextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                        ptextBox1.MaxLength = 10;
                        ptextBox1.KeyPress += new KeyPressEventHandler(ptextBox1_KeyPress);
                        ptextBox1.TextChanged += new EventHandler(ptextBox1_TextChanged);

                        var existingProduct = productList.FirstOrDefault(p => p.Item1.ToString() == product.Id);
                        if (existingProduct != null)
                        {
                            ptextBox1.Text = existingProduct.Item2.ToString();
                        }
                        // 
                        // pictureBox3
                        // 
                        ppictureBox3.Dock = System.Windows.Forms.DockStyle.Right;
                        ppictureBox3.Image = global::AvtoSavdo.Properties.Resources.plus;
                        ppictureBox3.Location = new System.Drawing.Point(238, 0);
                        ppictureBox3.Name = "pictureBox3";
                        ppictureBox3.Size = new System.Drawing.Size(40, 40);
                        ppictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                        ppictureBox3.TabIndex = 1;
                        ppictureBox3.TabStop = false;
                        ppictureBox3.Click += new EventHandler(ppictureBox3_Click);
                        // 
                        // pictureBox2
                        // 
                        ppictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
                        ppictureBox2.Image = global::AvtoSavdo.Properties.Resources.minus;
                        ppictureBox2.Location = new System.Drawing.Point(0, 0);
                        ppictureBox2.Name = "pictureBox2";
                        ppictureBox2.Size = new System.Drawing.Size(40, 40);
                        ppictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                        ppictureBox2.TabIndex = 0;
                        ppictureBox2.TabStop = false;
                        ppictureBox2.Click += new EventHandler(ppictureBox2_Click);
                        // 
                        // label3
                        // 
                        plabel3.Dock = System.Windows.Forms.DockStyle.Top;
                        plabel3.Font = new System.Drawing.Font("Lucida Sans Typewriter", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        plabel3.Location = new System.Drawing.Point(0, 214);
                        plabel3.Name = "label3";
                        plabel3.Size = new System.Drawing.Size(278, 18);
                        plabel3.TabIndex = 3;
                        plabel3.Text = "Mavjud: " + product.Miqdor;
                        plabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        // 
                        // label8
                        // 
                        plabel8.Dock = System.Windows.Forms.DockStyle.Top;
                        plabel8.Font = new System.Drawing.Font("Lucida Sans Typewriter", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        plabel8.Location = new System.Drawing.Point(0, 214);
                        plabel8.Name = "label8";
                        plabel8.Size = new System.Drawing.Size(278, 15);
                        plabel8.TabIndex = 0;
                        plabel8.Text = "Id: " + product.Id;
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
                        ppictureBox1.Image = ByteArrayToImage(product.ImageByte);
                        ppictureBox1.TabIndex = 2;
                        ppictureBox1.TabStop = false;
                        ppictureBox1.Click += (sender, e) =>
                        {
                            PictureBox clickedPictureBox = sender as PictureBox;
                            Panel parentPanel = clickedPictureBox.Parent as Panel;

                            plabel4.Visible = !plabel4.Visible;
                            plabel5.Visible = !plabel5.Visible;
                        };
                        // 
                        // label2
                        // 
                        plabel2.Dock = System.Windows.Forms.DockStyle.Top;
                        plabel2.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        plabel2.Location = new System.Drawing.Point(0, 51);
                        plabel2.Name = "label2";
                        plabel2.Size = new System.Drawing.Size(278, 23);
                        plabel2.TabIndex = 1;
                        plabel2.Text = "Narxi: " + product.Narx + " so'm";
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
                        plabel1.Text = product.Nom;
                        plabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                        /*
                        foreach (Control control in ppanel1.Controls)
                        {
                            // Har bir kontrol uchun click hodisasini qo'shish
                            control.Click += (sender, e) =>
                            {
                                productClick(id);
                            };
                        }
                        */
                        productpanel.Controls.Add(ppanel1);

                        Panel panelprobel = new Panel();
                        panelprobel.BackColor = Color.Transparent;
                        panelprobel.Size = new Size(200, 10);
                        panelprobel.Margin = new Padding(10);
                        panelprobel.Dock = DockStyle.Top;
                        productpanel.Controls.Add(panelprobel);
                    }
                }
                System.Windows.Forms.Button btnSotish = new System.Windows.Forms.Button();
                btnSotish.AutoSize = true;
                btnSotish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                btnSotish.Dock = System.Windows.Forms.DockStyle.Top;
                btnSotish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btnSotish.Font = new System.Drawing.Font("Lucida Sans Typewriter", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btnSotish.Location = new System.Drawing.Point(10, 100);
                btnSotish.Name = "btnSotish";
                btnSotish.Size = new System.Drawing.Size(280, 39);
                btnSotish.TabIndex = 10;
                btnSotish.Text = "Sotish";
                btnSotish.UseVisualStyleBackColor = false;
                btnSotish.Click += new EventHandler(btnSotish_Click);
                productpanel.Controls.Add(btnSotish);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message);
            }
        }

        private void btnSotish_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Ma'lumotlar bazasidagi miqdorlar productList dan ajratiladi
                    string ids = string.Join(",", productList.Select(p => p.Item1.ToString()));
                    string query = $"SELECT Id, Miqdor FROM ehtiyotqismlar WHERE Id IN ({ids})";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    Dictionary<int, int> databaseQuantities = new Dictionary<int, int>(); // Dictionary<int, int> o'zgaruvchisi long qiymatlarni int ga o'tqazadi
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        int quantity;
                        if (!reader.IsDBNull(reader.GetOrdinal("Miqdor")))
                        {
                            quantity = Convert.ToInt32(reader["Miqdor"]); // long qiymatlarni int ga o'tqazish
                        }
                        else
                        {
                            quantity = 0; // Miqdor mavjud emas, 0 bo'lishi kerak
                        }
                        databaseQuantities[id] = quantity;
                    }
                    reader.Close();

                    // Mahsulotlar miqdoridan productList dagi mahsulot soni 0 dan farqli mahsulotlar ajratiladi
                    var soldProducts = productList.Where(p => p.Item2 != 0).ToList();

                    foreach (var product in soldProducts)
                    {
                        int id = product.Item1;
                        int soldQuantity = product.Item2;

                        // Ma'lumotlar bazasidagi mahsulot miqdori yangilanadi
                        if (databaseQuantities.ContainsKey(id))
                        {
                            int remainingQuantity = databaseQuantities[id] - soldQuantity;
                            if (remainingQuantity < 0)
                            {
                                MessageBox.Show($"ID raqamiga ega mahsulot uchun yetarli miqdor mavjud emas: {id}");
                                continue;
                            }

                            SqlCommand updateCommand = new SqlCommand("UPDATE ehtiyotqismlar SET Miqdor = @quantity WHERE Id = @id", connection);
                            updateCommand.Parameters.AddWithValue("@quantity", remainingQuantity);
                            updateCommand.Parameters.AddWithValue("@id", id);
                            updateCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            MessageBox.Show($"ID bilan mahsulot: {id} maʼlumotlar bazasida topilmadi.");
                        }
                    }

                    // productList dagi barcha ma'lumotlar o'chiriladi
                    productList.Clear();
                }

                // Mahsulotlar miqdoridan productList dagi mahsulot soni 0 dan farqli mahsulotlar oyatladi
                // Va qo'shilgan mahsulotlar miqdorlari productList dagi mahsulot sonlariga qo'shiladi
                MessageBox.Show("Mahsulotlar muvaffaqiyatli sotildi.");
                addproductsavat(pnlsavat);
                addproduct(pnlehtiyotqismlar);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik: " + ex.Message);
            }
        }
    }
}
