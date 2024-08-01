using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Personel
{
    class sqlbaglanti
    {
        public SqlConnection Connection()
        {
            SqlConnection ccon = new SqlConnection(@"Data Source=DESKTOP-RI6MMQG;Initial Catalog=Personel;Integrated Security=True;Encrypt=False");
            try
            {
                ccon.Open();
                return ccon;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Veritabanına bağlantı sağlanamadı: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
                return null;
            }
        }

    }
}
