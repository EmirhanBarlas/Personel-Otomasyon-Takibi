using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Personel
{
    public partial class Form1 : Form
    {
        sqlbaglanti bgl = new sqlbaglanti();
        public Form1()
        {
            InitializeComponent();
        }

        //metods start
        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter($"Select * From {SqlTables.Personel}", bgl.Connection());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void IdList()
        {
            string sorgu = $"Select * from {SqlTables.Personel}";

            SqlCommand cmd = new SqlCommand(sorgu, bgl.Connection());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                id.Properties.Items.Add(dr[0]);
                id3.Properties.Items.Add(dr[0]);
            }
            bgl.Connection().Close();
        }
        private void TotalPersonel()
        {
            SqlCommand cek = new SqlCommand($"SELECT COUNT (*) FROM {SqlTables.Personel}", bgl.Connection());
            int TotalPersonel = (int)cek.ExecuteScalar();
            total.Text = "TOPLAM PERSONEL SAYISI: " + TotalPersonel.ToString();
        }
        private void AvgPersonelAge()
        {
            SqlCommand cek = new SqlCommand($"SELECT AVG(Yas) FROM {SqlTables.Personel}", bgl.Connection());
            int AvgPersonelAge = (int)cek.ExecuteScalar();
            avg.Text = "ORTALAMA PERSONEL YASI: " + AvgPersonelAge.ToString();
        }
        //metods end

        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
            TotalPersonel();
            AvgPersonelAge();
            IdList();
        }
        void temizle()
        {
            AD.Text = string.Empty;
            soyad.Text = string.Empty;
            cinsiyet.Text = string.Empty;
            yas.Text = string.Empty;
            tc.Text = string.Empty;
        }
        private void temizle2()
        {
            ad2.Text = string.Empty;
            soyad2.Text = string.Empty;
            cinsiyet2.Text = string.Empty;
            yas2.Text = string.Empty;
            tc2.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand($"insert into {SqlTables.Personel} (AD,Soyad,Cinsiyet,Yas,Tc,EklenmeTarihi) values(@p1,@p2,@p3,@p4,@p5,@p6)", bgl.Connection());
            cmd.Parameters.AddWithValue("@p1", AD.Text.Trim());
            cmd.Parameters.AddWithValue("@p2", soyad.Text.Trim());
            cmd.Parameters.AddWithValue("@p3", cinsiyet.Text.Trim());
            cmd.Parameters.AddWithValue("@p4", yas.Text.Trim());
            cmd.Parameters.AddWithValue("@p5", tc.Text.Trim());
            cmd.Parameters.AddWithValue("@p6", DateTime.Now.ToString("dd.MM.yyyy - HH:mm"));
            cmd.ExecuteNonQuery();
            bgl.Connection().Close();
            MessageBox.Show("Personel Kaydý Baþarýyla Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Thread.Sleep(100);
            temizle();
            bgl.Connection().Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string IdText = id.Text;
                string Name = ad2.Text;
                string SurName = soyad2.Text;
                string Person = cinsiyet2.Text;
                string Age = yas2.Text;
                string TC = tc2.Text;

                SqlCommand cmd = new SqlCommand($"UPDATE {SqlTables.Personel} SET AD = @p2, soyad = @p3, Cinsiyet = @p4, Yas = @p5, Tc = @p6 WHERE ID = @p1", bgl.Connection());
                cmd.Parameters.AddWithValue("@p1", IdText);
                cmd.Parameters.AddWithValue("@p2", Name);
                cmd.Parameters.AddWithValue("@p3", SurName);
                cmd.Parameters.AddWithValue("@p4", Person);
                cmd.Parameters.AddWithValue("@p5", Age);
                cmd.Parameters.AddWithValue("@p6", TC);
                cmd.ExecuteNonQuery();

                bgl.Connection().Close();
                MessageBox.Show("Personel Bilgileri Güncellendi: " + Name, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Hatalý Giriþ: Lütfen Kontrol Saðlayýnýz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Hatalý giriþ: Lütfen yaþ alanýna geçerli bir sayý deðeri giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            temizle2();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand($"DELETE FROM {SqlTables.Personel} WHERE ID = @p1", bgl.Connection());
            cmd.Parameters.AddWithValue("@p1", id3.Text);
            cmd.ExecuteNonQuery();
            bgl.Connection().Close();
            Thread.Sleep(1);
            temizle3();
            Listele();
        }
        private void temizle3()
        {
            id3.Text = string.Empty;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Listele();
            TotalPersonel();
            AvgPersonelAge();
        }
    }
}