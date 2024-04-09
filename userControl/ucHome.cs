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

namespace QuanLyDiemSV.userControl
{
    public partial class ucHome : UserControl
    {
        SqlConnection con;
        SqlCommand cmd;
        dbConnect db = new dbConnect();
        public ucHome()
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = db.GetConnection();
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        public void countSV()
        {
            con.Open();

            string query = "SELECT COUNT(*) FROM SinhVien";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                int svCount = (int)cmd.ExecuteScalar();
                label19.Text = "0" + svCount.ToString();
            }

            con.Close();
        }

        public void countHP()
        {
            con.Open();
            cmd = new SqlCommand("Select Count(*) from HocPhan", con);
            var countHP = cmd.ExecuteScalar();
            label22.Text = "0" + countHP.ToString();
            con.Close();
        }

        public void countLop()
        {
            con.Open();
            cmd = new SqlCommand("Select Count(*) from Lop", con);
            var countHP = cmd.ExecuteScalar();
            label24.Text = "0" + countHP.ToString();
            con.Close();
        }
    }
}
