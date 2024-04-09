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
    public partial class frmThemHP : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        dbConnect db = new dbConnect();
        ucHocPhan ucHocPhan = new ucHocPhan();
        string _title = "Quản Lý Điểm Sinh Viên";
        public frmThemHP(ucHocPhan ucHocPhan)
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = db.GetConnection();
            this.ucHocPhan = ucHocPhan;
        }

        public void getGV()
        {
            con.Open();
            cmd = new SqlCommand("Select * from GiangVien", con);
            dr = cmd.ExecuteReader();
            cboGV.Items.Clear();
            while (dr.Read())
            {
                cboGV.Items.Add(dr["MaGV"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void getKhoa()
        {
            con.Open();
            cmd = new SqlCommand("Select * from Khoa", con);
            dr = cmd.ExecuteReader();
            cboKhoa.Items.Clear();
            while (dr.Read())
            {
                cboKhoa.Items.Add(dr["MaKhoa"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void getCN()
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

        public void getTextGV()
        {
            con.Open();
            cmd = new SqlCommand("Select * from GiangVien where MaGV = @MaGV", con);
            cmd.Parameters.AddWithValue("@MaGv", cboGV.Text);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtTenGV.Text = dr["TenGV"].ToString();
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
                txtTenKhoa.Text = dr["TenKhoa"].ToString();
            }
            dr.Close();
            con.Close();
        }

        public void getTextCN()
        {
            con.Open();
            cmd = new SqlCommand("Select * from ChuyenNganh where MaCN = @MaCN", con);
            cmd.Parameters.AddWithValue("@MaCN", cboCN.Text);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtTenCN.Text = dr["TenCN"].ToString();
            }
            dr.Close();
            con.Close();
        }

        private void cboGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            getTextGV();
        }

        private void cboKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            getTextCN();
        }

        private void cboKhoa_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            getTextKhoa();
            getCN();
            cboCN.SelectedIndex = -1;
            txtTenCN.Text = string.Empty;
        }

        public void Clear()
        {
            cboCN.SelectedIndex = -1;
            cboGV.SelectedIndex = -1;
            cboKhoa.SelectedIndex = -1;
            txtMaHP.Text = string.Empty;
            txtTenHP.Text = string.Empty;
            txtSoTC.Text = string.Empty;
            txtQT.Text = string.Empty;
            txtThi.Text = string.Empty;
            txtTenGV.Text = string.Empty;
            txtTenKhoa.Text = string.Empty;
            txtTenCN.Text = string.Empty;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("SELECT COUNT(*) FROM HocPhan WHERE MaHP=@MaHP", con);
            cmd.Parameters.AddWithValue("@MaHP", txtMaHP.Text);
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
            {
                MessageBox.Show("Mã Học Phần Đã Tồn Tại!", "Cảnh Báo", MessageBoxButtons.OK);
            }
            else
            {
                if (txtMaHP.Text == "" || txtTenHP.Text == "" || txtSoTC.Text == "" || txtQT.Text == "" || txtThi.Text == "")
                {
                    MessageBox.Show("Vui Lòng Nhập Thông Đủ Tin!", "Cảnh Báo", MessageBoxButtons.OK);
                }
                else
                {
                    try
                    {
                        //con.Open();
                        cmd = new SqlCommand("INSERT INTO HocPhan(MaHP,TenHP,SoTC,PhanTramDQT,PhanTramDT,MaGV,MaKhoa,MaCN) values(@MaHP,@TenHP,@SoTC,@PhanTramDQT,@PhanTramDT,@MaGV,@MaKhoa,@MaCN)", con);
                        cmd.Parameters.AddWithValue("@MaHP", txtMaHP.Text);
                        cmd.Parameters.AddWithValue("@TenHP", txtTenHP.Text);
                        cmd.Parameters.AddWithValue("@SoTC", txtSoTC.Text);
                        cmd.Parameters.AddWithValue("@PhanTramDQT", txtQT.Text);
                        cmd.Parameters.AddWithValue("@PhanTramDT", txtThi.Text);
                        cmd.Parameters.AddWithValue("@MaGV", cboGV.Text);
                        cmd.Parameters.AddWithValue("@MaKhoa", cboKhoa.Text);
                        cmd.Parameters.AddWithValue("@MaCN", cboCN.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Thêm Học Phần Thành Công!", _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                        ucHocPhan.LoadRecord();
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

        private void btnF5_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("UPDATE HocPhan set MaHP=@MaHP,TenHP=@TenHP,SoTC=@SoTC,PhanTramDQT=@PhanTramDQT,PhanTramDT=@PhanTramDT,MaGV=@MaGV,MaKhoa=@MaKhoa,MaCN=@MaCN Where MaHP=@MaHP", con);
                cmd.Parameters.AddWithValue("@MaHP", txtMaHP.Text);
                cmd.Parameters.AddWithValue("@TenHP", txtTenHP.Text);
                cmd.Parameters.AddWithValue("@SoTC", txtSoTC.Text);
                cmd.Parameters.AddWithValue("@PhanTramDQT", txtQT.Text);
                cmd.Parameters.AddWithValue("@PhanTramDT", txtThi.Text);
                cmd.Parameters.AddWithValue("@MaGV", cboGV.Text);
                cmd.Parameters.AddWithValue("@MaKhoa", cboKhoa.Text);
                cmd.Parameters.AddWithValue("@MaCN", cboCN.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Sửa Thông Tin Học Phần Thành Công!", _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                ucHocPhan.LoadRecord();
                this.Dispose();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, _title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtSoTC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtQT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }


            if (char.IsDigit(e.KeyChar))
            {
                int value = int.Parse(txtQT.Text + e.KeyChar);
                e.Handled = value < 0 || value > 100;
            }
        }

        private void txtThi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }


            if (char.IsDigit(e.KeyChar))
            {
                int value = int.Parse(txtThi.Text + e.KeyChar);
                e.Handled = value < 0 || value > 100;
            }
        }
    }
}
