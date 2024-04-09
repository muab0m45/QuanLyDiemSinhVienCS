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

namespace QuanLyDiemSV
{
    public partial class frmThemLop : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        dbConnect db = new dbConnect();
        ucLop ucLop = new ucLop();
        string _title = "Quản Lý Điểm Sinh Viên";
        public frmThemLop(ucLop ucLop)
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = db.GetConnection();
            this.ucLop = ucLop;
        }

        public frmThemLop()
        {
        }

        public void getCTDT()
        {
            con.Open();
            cmd = new SqlCommand("Select * from ChuongTrinhDaoTao", con);
            dr = cmd.ExecuteReader();
            cboCTDT.Items.Clear();
            while (dr.Read())
            {
                cboCTDT.Items.Add(dr["MaCTDT"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void getTextCTDT()
        {
            con.Open();
            cmd = new SqlCommand("Select * from ChuongTrinhDaotao where MaCTDT = @MaCTDT", con);
            cmd.Parameters.AddWithValue("@MaCTDT", cboCTDT.Text);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtCTDT.Text = dr["TenCTDT"].ToString();
            }
            dr.Close();
            con.Close();
        }


        public void getKhoa()
        {
            con.Open();
            cmd = new SqlCommand("Select * from Khoa where MaCTDT = @MaCTDT", con);
            cmd.Parameters.AddWithValue("@MaCTDT", cboCTDT.Text);
            dr = cmd.ExecuteReader();
            cboKhoa.Items.Clear();
            while (dr.Read())
            {
                cboKhoa.Items.Add(dr["MaKhoa"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void getTextKhoa()
        {
            con.Open();
            cmd = new SqlCommand("Select * from Khoa where MaKhoa = @MaKhoa", con);
            cmd.Parameters.AddWithValue("@MaKhoa", cboKhoa.Text);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtKhoa.Text = dr["TenKhoa"].ToString();
            }
            dr.Close();
            con.Close();
        }

        public void getChuyenNganh()
        {
            con.Open();
            cmd = new SqlCommand("Select * from ChuyenNganh where MaKhoa = @MaKhoa", con);
            cmd.Parameters.AddWithValue("@MaKhoa", cboKhoa.Text);
            dr = cmd.ExecuteReader();
            cboCN.Items.Clear();
            while (dr.Read())
            {
                cboCN.Items.Add(dr["MaCN"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void getTextChuyenNganh()
        {
            con.Open();
            cmd = new SqlCommand("Select * from ChuyenNganh where MaCN = @MaCN", con);
            cmd.Parameters.AddWithValue("@MaCN", cboCN.Text);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtCN.Text = dr["TenCN"].ToString();
            }
            dr.Close();
            con.Close();
        }

        public void getGV()
        {
            con.Open();
            cmd = new SqlCommand("Select * from GiangVien", con);
            dr = cmd.ExecuteReader();
            cboCVHT.Items.Clear();
            while (dr.Read())
            {
                cboCVHT.Items.Add(dr["MaGV"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void getTextGV()
        {
            con.Open();
            cmd = new SqlCommand("Select * from GiangVien where MaGV = @MaGV", con);
            cmd.Parameters.AddWithValue("@MaGv", cboCVHT.Text);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtCVHT.Text = dr["TenGV"].ToString();
            }
            dr.Close();
            con.Close();
        }


        private void cboKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            getChuyenNganh();
            getTextKhoa();
        }

        private void cboCTDT_SelectedIndexChanged(object sender, EventArgs e)
        {
            getKhoa();
            getTextCTDT();
        }

        private void txtCTDT_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCN_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            getTextChuyenNganh();
        }

        private void cboCVHT_SelectedIndexChanged(object sender, EventArgs e)
        {
            getTextGV();
        }

        public void Clear()
        {
            cboCN.SelectedIndex = -1;
            cboCTDT.SelectedIndex = -1;
            cboCVHT.SelectedIndex = -1;
            cboKhoa.SelectedIndex = -1;
            txtCN.Text = string.Empty;
            txtCTDT.Text = string.Empty;
            txtKhoa.Text = string.Empty;
            txtCVHT.Text = string.Empty;
            txtKhoaHoc.Text = string.Empty;
            txtMaLop.Text = string.Empty;
        }

        private void btnThemLopFrm_Click(object sender, EventArgs e)
        {

            con.Open();
            cmd = new SqlCommand("SELECT COUNT(*) FROM Lop WHERE MaLop=@MaLop and MaCN=@MaCN and KhoaHoc=@KhoaHoc", con);
            cmd.Parameters.AddWithValue("@MaLop", txtMaLop.Text);
            cmd.Parameters.AddWithValue("@MaCN", cboCN.Text);
            cmd.Parameters.AddWithValue("@KhoaHoc", txtKhoaHoc.Text);
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
            {
                MessageBox.Show("Lớp Học Đã Tồn Tại!", "Cảnh Báo", MessageBoxButtons.OK);
            } else
            {
                if (cboCTDT.Text == "" || cboCVHT.Text == "" || cboCN.Text == "" || cboKhoa.Text == "" || txtMaLop.Text == "" || txtKhoaHoc.Text == "")
                {
                    MessageBox.Show("Vui Lòng Nhập Thông Đủ Tin", "Cảnh Báo", MessageBoxButtons.OK);
                }
                else
                {
                    try
                    {
                        //con.Open();
                        cmd = new SqlCommand("INSERT INTO Lop(MaLop,MaKhoa,MaCN,MaGV,KhoaHoc) values(@MaLop,@MaKhoa,@MaCN,@MaGV,@KhoaHoc)", con);
                        cmd.Parameters.AddWithValue("@MaLop", txtMaLop.Text);
                        cmd.Parameters.AddWithValue("@MaKhoa", cboKhoa.Text);
                        cmd.Parameters.AddWithValue("@MaCN", cboCN.Text);
                        cmd.Parameters.AddWithValue("@MaGV", cboCVHT.Text);
                        cmd.Parameters.AddWithValue("@KhoaHoc", txtKhoaHoc.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Thêm Thông Tin Lớp Thành Công!", _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                        ucLop.LoadRecord();
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

        private void btnSuaLopFrm_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("UPDATE Lop set MaLop=@MaLop,MaKhoa=@MaKhoa,MaCN=@MaCN,MaGV=@MaGV,KhoaHoc=@KhoaHoc Where MaLop=@MaLop", con);
                cmd.Parameters.AddWithValue("@MaLop", txtMaLop.Text);
                cmd.Parameters.AddWithValue("@MaKhoa", cboKhoa.Text);
                cmd.Parameters.AddWithValue("@MaCN", cboCN.Text);
                cmd.Parameters.AddWithValue("@MaGV", cboCVHT.Text);
                cmd.Parameters.AddWithValue("@KhoaHoc", txtKhoaHoc.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Sửa Thông Tin Lớp Thành Công!", _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                ucLop.LoadRecord();
                this.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, _title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnThemLopf5_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
