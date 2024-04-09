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
    public partial class ucQuanLy : UserControl
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        dbConnect db = new dbConnect();
        string _title = "Quản Lý Điểm Sinh Viên";
        public ucQuanLy()
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = db.GetConnection();
        }

        public void LoadRecord()
        {
            int i = 0;
            guna2DataGridView1.Rows.Clear();
            con.Open();
            cmd = new SqlCommand("Select * from TaiKhoan", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string loaiTaiKhoan = Convert.ToString(dr["LoaiTaiKhoan"]);

                if (loaiTaiKhoan == "Quản Trị")
                {
                    string quyenHan = "Tất Cả Chức Năng";
                    i++;
                    guna2DataGridView1.Rows.Add(i, dr["TaiKhoan"].ToString(), dr["MatKhau"].ToString(), dr["LoaiTaiKhoan"].ToString(), quyenHan.ToString());
                } else if (loaiTaiKhoan == "Giảng Viên")
                {
                    string quyenHan = "Xem Thông Tin Lớp Học Phần, Nhập Điểm Lớp Học Phần";
                    i++;
                    guna2DataGridView1.Rows.Add(i, dr["TaiKhoan"].ToString(), dr["MatKhau"].ToString(), dr["LoaiTaiKhoan"].ToString(), quyenHan.ToString());
                } else if (loaiTaiKhoan == "Sinh Viên")
                {
                    string quyenHan = "Xem Điểm Cá Nhân";
                    i++;
                    guna2DataGridView1.Rows.Add(i, dr["TaiKhoan"].ToString(), dr["MatKhau"].ToString(), dr["LoaiTaiKhoan"].ToString(), quyenHan.ToString());
                }

            }
            dr.Close();
            con.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            int i = 0;
            guna2DataGridView1.Rows.Clear();
            con.Open();
            cmd = new SqlCommand("Select * from TaiKhoan Where TaiKhoan=@TaiKhoan", con);
            cmd.Parameters.AddWithValue("@TaiKhoan", txtSearchTK.Text);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string loaiTaiKhoan = Convert.ToString(dr["LoaiTaiKhoan"]);

                if (loaiTaiKhoan == "Quản Trị")
                {
                    string quyenHan = "Tất Cả Chức Năng";
                    i++;
                    guna2DataGridView1.Rows.Add(i, dr["TaiKhoan"].ToString(), dr["MatKhau"].ToString(), dr["LoaiTaiKhoan"].ToString(), quyenHan.ToString());
                }
                else if (loaiTaiKhoan == "Giảng Viên")
                {
                    string quyenHan = "Xem Thông Tin Lớp Học Phần, Nhập Điểm Lớp Học Phần";
                    i++;
                    guna2DataGridView1.Rows.Add(i, dr["TaiKhoan"].ToString(), dr["MatKhau"].ToString(), dr["LoaiTaiKhoan"].ToString(), quyenHan.ToString());
                }
                else if (loaiTaiKhoan == "Sinh Viên")
                {
                    string quyenHan = "Xem Điểm Cá Nhân";
                    i++;
                    guna2DataGridView1.Rows.Add(i, dr["TaiKhoan"].ToString(), dr["MatKhau"].ToString(), dr["LoaiTaiKhoan"].ToString(), quyenHan.ToString());
                }

            }
            dr.Close();
            con.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            int i = 0;
            guna2DataGridView1.Rows.Clear();
            con.Open();
            cmd = new SqlCommand("Select * from TaiKhoan Where LoaiTaiKhoan=@LoaiTaiKhoan", con);
            cmd.Parameters.AddWithValue("@LoaiTaiKhoan", cboFilLoaiTK.Text);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string loaiTaiKhoan = Convert.ToString(dr["LoaiTaiKhoan"]);

                if (loaiTaiKhoan == "Quản Trị")
                {
                    string quyenHan = "Tất Cả Chức Năng";
                    i++;
                    guna2DataGridView1.Rows.Add(i, dr["TaiKhoan"].ToString(), dr["MatKhau"].ToString(), dr["LoaiTaiKhoan"].ToString(), quyenHan.ToString());
                }
                else if (loaiTaiKhoan == "Giảng Viên")
                {
                    string quyenHan = "Xem Thông Tin Lớp Học Phần, Nhập Điểm Lớp Học Phần";
                    i++;
                    guna2DataGridView1.Rows.Add(i, dr["TaiKhoan"].ToString(), dr["MatKhau"].ToString(), dr["LoaiTaiKhoan"].ToString(), quyenHan.ToString());
                }
                else if (loaiTaiKhoan == "Sinh Viên")
                {
                    string quyenHan = "Xem Điểm Cá Nhân";
                    i++;
                    guna2DataGridView1.Rows.Add(i, dr["TaiKhoan"].ToString(), dr["MatKhau"].ToString(), dr["LoaiTaiKhoan"].ToString(), quyenHan.ToString());
                }

            }
            dr.Close();
            con.Close();
        }

        private void btnThemSV_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("SELECT COUNT(*) FROM TaiKhoan WHERE TaiKhoan=@TaiKhoan", con);
            cmd.Parameters.AddWithValue("@TaiKhoan", txtAddTK.Text);
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
            {
                MessageBox.Show("Tài Khoản Đã Tồn Tại!", "Cảnh Báo", MessageBoxButtons.OK);
            }
            else
            {
                if (txtAddTK.Text == "" || txtAddMK.Text == "" || cboAddLoai.Text == "")
                {
                    MessageBox.Show("Vui Lòng Nhập Thông Đủ Tin!", "Cảnh Báo", MessageBoxButtons.OK);
                }
                else
                {
                    try
                    {
                        //con.Open();
                        cmd = new SqlCommand("INSERT INTO TaiKhoan(TaiKhoan,MatKhau,LoaiTaiKhoan) values(@TaiKhoan,@MatKhau,@LoaiTaiKhoan)", con);
                        cmd.Parameters.AddWithValue("@TaiKhoan", txtAddTK.Text);
                        cmd.Parameters.AddWithValue("@MatKhau", txtAddMK.Text);
                        cmd.Parameters.AddWithValue("@LoaiTaiKhoan", cboAddLoai.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Thêm Tài Khoản Thành Công!", _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAddMK.Text = String.Empty;
                        txtAddTK.Text = String.Empty;
                        cboAddLoai.SelectedIndex = -1;
                        LoadRecord();
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

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = guna2DataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "ColXoa")
            {
                if (MessageBox.Show("Xác Nhận Xóa?", _title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("delete from TaiKhoan where TaiKhoan = '" + guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Xóa Tài Khoản Thành Công!", _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRecord();
                }
            }
        }
    }
}
