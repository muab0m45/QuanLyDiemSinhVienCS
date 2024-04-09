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
using QuanLyDiemSV.userControl;
using System.Drawing;
using System.IO;

namespace QuanLyDiemSV
{
    public partial class frmThemSV : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        dbConnect db = new dbConnect();
        ucSinhVien ucSinhVien = new ucSinhVien();
        string _title = "Quản Lý Điểm Sinh Viên";
        string imageUrl = null;

        public frmThemSV(ucSinhVien ucSinhVien)
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = db.GetConnection();
            this.ucSinhVien = ucSinhVien;
        }

        public void getLop()
        {
            con.Open();
            cmd = new SqlCommand("Select * from Lop", con);
            dr = cmd.ExecuteReader();
            cboLop.Items.Clear();
            while (dr.Read())
            {
                cboLop.Items.Add(dr["MaLop"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void Clear()
        {
            cboGioiTinh.SelectedIndex = -1;
            cboLop.SelectedIndex = -1;
            txtMSV.Text = string.Empty;
            txtHoTen.Text = string.Empty;
            txtQueQuan.Text = string.Empty;
            txtSdt.Text = string.Empty;
            txtNienKhoa.Text = string.Empty;
            dtNgaySinh.Value = DateTime.Now;
            imgAnh.Image = null;
        }




        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // byte[] arr = ImageToByteArray(imgAnh.Image);
            byte[] arr = null;
            if (imgAnh.Image != null)
            {
                arr = ImageToByteArray(imgAnh.Image);
            }


            con.Open();
            cmd = new SqlCommand("SELECT COUNT(*) FROM SinhVien WHERE MaSV=@MaSV and MaLop=@MaLop", con);
            cmd.Parameters.AddWithValue("@MaSV", txtMSV.Text);
            cmd.Parameters.AddWithValue("@MaLop", cboLop.Text);
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
            {
                MessageBox.Show("Mã Sinh Viên Đã Tồn Tại!", "Cảnh Báo", MessageBoxButtons.OK);
            }
            else
            {
                if (txtMSV.Text == "" || txtHoTen.Text == "" || cboLop.Text == "" || imgAnh.Image == null)
                {
                    MessageBox.Show("Vui Lòng Nhập Thông Đủ Tin", "Cảnh Báo", MessageBoxButtons.OK);
                }
                else
                {
                    try
                    {
                        
                        //con.Open();
                        cmd = new SqlCommand("INSERT INTO SinhVien(MaSV,TenSV,NgaySinh,QueQuan,SoDienThoai,MaLop,GioiTinh,Anh,NienKhoa) values(@MaSV,@TenSV,@NgaySinh,@QueQuan,@SoDienThoai,@MaLop,@GioiTinh,@Anh,@NienKhoa)", con);
                        cmd.Parameters.AddWithValue("@MaSV", txtMSV.Text);
                        cmd.Parameters.AddWithValue("@TenSV", txtHoTen.Text);
                        cmd.Parameters.AddWithValue("@NgaySinh", dtNgaySinh.Text);
                        cmd.Parameters.AddWithValue("@QueQuan", txtQueQuan.Text);
                        cmd.Parameters.AddWithValue("@SoDienThoai", txtSdt.Text);
                        cmd.Parameters.AddWithValue("@MaLop", cboLop.Text);
                        cmd.Parameters.AddWithValue("@GioiTinh", cboGioiTinh.Text);
                        cmd.Parameters.AddWithValue("@NienKhoa", txtNienKhoa.Text);
                        cmd.Parameters.AddWithValue("@Anh", arr);
                        cmd.ExecuteNonQuery();
                       // con.Close();
                        MessageBox.Show("Thêm Thông Tin Lớp Thành Công!", _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                        ucSinhVien.LoadRecord();
                        this.Dispose();
                    }
                    catch (Exception ex)
                    {
                        //con.Close();
                        MessageBox.Show(ex.Message, _title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            con.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            byte[] arr = ImageToByteArray(imgAnh.Image);

            try
            {
                con.Open();
                cmd = new SqlCommand("UPDATE SinhVien set MaSV=@MaSV,TenSV=@TenSV,NgaySinh=@NgaySinh,QueQuan=@QueQuan,SoDienThoai=@SoDienThoai,MaLop=@MaLop,GioiTinh=@GioiTinh,Anh=@Anh,NienKhoa=@NienKhoa Where MaSV=@MaSV", con);
                cmd.Parameters.AddWithValue("@MaSV", txtMSV.Text);
                cmd.Parameters.AddWithValue("@TenSV", txtHoTen.Text);
                cmd.Parameters.AddWithValue("@NgaySinh", dtNgaySinh.Text);
                cmd.Parameters.AddWithValue("@QueQuan", txtQueQuan.Text);
                cmd.Parameters.AddWithValue("@SoDienThoai", txtSdt.Text);
                cmd.Parameters.AddWithValue("@MaLop", cboLop.Text);
                cmd.Parameters.AddWithValue("@GioiTinh", cboGioiTinh.Text);
                cmd.Parameters.AddWithValue("@NienKhoa", txtNienKhoa.Text);
                cmd.Parameters.AddWithValue("@Anh", arr);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Sửa Thông Tin Lớp Thành Công!", _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                ucSinhVien.LoadRecord();
                this.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, _title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

          byte[] ImageToByteArray(Image img)
           {
               MemoryStream ms = new MemoryStream();
               img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
               return ms.ToArray();
           } 

        private void btnF5_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imgAnh.Image = Image.FromFile(ofd.FileName);
                this.Text = ofd.FileName;
            }
        }

        private void txtSdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
