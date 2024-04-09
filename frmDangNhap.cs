using QuanLyDiemSV.userControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDiemSV
{
    public partial class frmDangNhap : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        frmMain f = new frmMain();
        dbConnect db = new dbConnect();
        string _title = "Phần Mềm Quản Lý Điểm Sinh Viên";
        public frmDangNhap(frmMain f)
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = db.GetConnection();
            this.f = f;
            txtPassword.PasswordChar = '*';

        }

        public static string CurrentUserName { get; set; }
        public static string CurrentUserType { get; set; }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            con.Open();
            string userName = txtUser.Text;
            string passWord = txtPassword.Text;

            if (userName == null || userName.Equals(""))
            {
                MessageBox.Show("Chưa Nhập Tài Khoản");
                return;
            }
            if (passWord == null || passWord.Equals(""))
            {
                MessageBox.Show("Chưa Nhập Mật Khẩu");
                return;
            }
            cmd = new SqlCommand("Select * from TaiKhoan where Taikhoan = @Taikhoan and Matkhau=@Matkhau and LoaiTaiKhoan=@LoaiTaiKhoan", con);
            cmd.Parameters.AddWithValue("@Taikhoan", txtUser.Text);
            cmd.Parameters.AddWithValue("@Matkhau", txtPassword.Text);
            cmd.Parameters.AddWithValue("LoaiTaiKhoan", cboLoaiTK.Text);
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                CurrentUserName = txtUser.Text;
                CurrentUserType = cboLoaiTK.Text;
                string loaiTaiKhoan = dr["LoaiTaiKhoan"].ToString();
                if (loaiTaiKhoan == "Quản Trị")
                {
                    f.Show();
                } else if (loaiTaiKhoan == "Giảng Viên")
                {
                    f.btnHomeSV.Visible = false;
                    f.guna2Button3.Visible = false;
                    f.guna2Button4.Visible = false;
                    f.guna2Button5.Visible = false;
                    f.dropDownDiem.Location = new Point(0, 217);
                    f.Show();
                } else if (loaiTaiKhoan == "Sinh Viên")
                {
                    f.btnHomeSV.Visible = false;
                    f.guna2Button3.Visible = false;
                    f.guna2Button4.Visible = false;
                    f.guna2Button2.Enabled = false;
                    f.guna2Button5.Visible = false;
                    f.dropDownDiem.Location = new Point(0, 217);
                    f.Show();
                }
                MessageBox.Show("Đăng Nhập Thành Công");
                this.Hide();
            }
            else
            {
                MessageBox.Show("Đăng Nhập Thất Bại");
            }

            con.Close();
        }


        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
