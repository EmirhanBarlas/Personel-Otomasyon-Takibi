using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Personel
{
    class Sqlbaglanti
    {
        public SqlConnection Connection()
        {
            SqlConnection ccon = new SqlConnection(@"Data Source=DESKTOP-RI6MMQG;Initial Catalog=Personel;Integrated Security=True;Encrypt=False");
            ccon.Open();
            return ccon;
        }

    }
}
