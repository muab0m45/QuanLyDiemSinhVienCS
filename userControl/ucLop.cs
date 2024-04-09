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
    public partial class ucLop : UserControl
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        dbConnect db = new dbConnect();
        string _title = "Quản Lý Điểm Sinh Viên";
        public ucLop()
        {
            
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = db.GetConnection();
            guna2DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
        }

        public void LoadRecord()
        {
            int i = 0;
            guna2DataGridView1.Rows.Clear();
            con.Open();
            cmd = new SqlCommand("SELECT * FROM ChuongTrinhDaoTao JOIN Khoa ON ChuongTrinhDaoTao.MaCTDT = Khoa.MaCTDT JOIN ChuyenNganh ON Khoa.MaKhoa = ChuyenNganh.MaKhoa JOIN Lop ON ChuyenNganh.MaKhoa = Lop.MaKhoa AND ChuyenNganh.MaCN = Lop.MaCN JOIN GiangVien ON Lop.MaGV = GiangVien.MaGV", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                guna2DataGridView1.Rows.Add(i, dr["MaCTDT"].ToString(), dr["TenCTDT"].ToString(), dr["MaKhoa"].ToString(), dr["TenKhoa"].ToString(), dr["MaCN"].ToString(), dr["TenCN"].ToString(), dr["MaLop"].ToString(), dr["KhoaHoc"].ToString(), dr["MaGV"].ToString(), dr["TenGV"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void getKhoa()
        {
            con.Open();
            cmd = new SqlCommand("Select * From Khoa", con);
            dr = cmd.ExecuteReader();
            cboFilKhoa.Items.Clear();
            while (dr.Read())
            {
                cboFilKhoa.Items.Add(dr["TenKhoa"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void getCN()
        {
            con.Open();
            cmd = new SqlCommand("SELECT ChuyenNganh.TenCN FROM Khoa JOIN ChuyenNganh ON Khoa.MaKhoa = ChuyenNganh.MaKhoa WHERE Khoa.TenKhoa = @TenKhoa", con);
            cmd.Parameters.AddWithValue("@TenKhoa", cboFilKhoa.Text);
            dr = cmd.ExecuteReader();
            cboFilCN.Items.Clear();
            while (dr.Read())
            {
                cboFilCN.Items.Add(dr["TenCN"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void LoadRecordFil()
        {
            int i = 0;
            guna2DataGridView1.Rows.Clear();
            con.Open();
            cmd = new SqlCommand("SELECT * FROM ChuongTrinhDaoTao CTDT JOIN Khoa K ON CTDT.MaCTDT = K.MaCTDT JOIN ChuyenNganh CN ON K.MaKhoa = CN.MaKhoa JOIN Lop L ON CN.MaKhoa = L.MaKhoa AND CN.MaCN = L.MaCN JOIN GiangVien GV ON L.MaGV = GV.MaGV WHERE CN.TenCN = @TenCN", con);
            cmd.Parameters.AddWithValue("@TenCN", cboFilCN.Text);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                guna2DataGridView1.Rows.Add(i, dr["MaCTDT"].ToString(), dr["TenCTDT"].ToString(), dr["MaKhoa"].ToString(), dr["TenKhoa"].ToString(), dr["MaCN"].ToString(), dr["TenCN"].ToString(), dr["MaLop"].ToString(), dr["KhoaHoc"].ToString(), dr["MaGV"].ToString(), dr["TenGV"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void btnThemLop_Click(object sender, EventArgs e)
        {
            frmThemLop f = new frmThemLop(this);
            f.getCTDT();
            f.getTextCTDT();
            f.getKhoa();
            f.getTextKhoa();
            f.getChuyenNganh();
            f.getTextChuyenNganh();
            f.getGV();
            f.getTextGV();
            f.btnSuaLopFrm.Enabled = false;
            f.ShowDialog();
        }


        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = guna2DataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "ColSua")
            {
                frmThemLop f = new frmThemLop(this);
                f.getCTDT();
                f.getTextCTDT();
                f.getKhoa();
                f.getTextKhoa();
                f.getChuyenNganh();
                f.getTextChuyenNganh();
                f.getGV();
                f.getTextGV();
                f.cboCTDT.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                f.cboKhoa.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                f.cboCN.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                f.txtMaLop.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                f.cboCVHT.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                f.txtKhoaHoc.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                f.txtMaLop.Enabled = false;
                f.btnThemLopFrm.Enabled = false;
                f.ShowDialog();
                
            } else if (colName == "ColXoa")
            {
                if (MessageBox.Show("Xác Nhận Xóa?", _title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        con.Open();
                        cmd = new SqlCommand("delete from Lop where MaLop = '" + guna2DataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Xóa Lớp Học Thành Công!", _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRecord();
                    } catch
                    {
                        MessageBox.Show("Dữ Liệu Sinh Viên Của Lớp Đang Tồn Tại, Không Thể Xóa!", _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void guna2DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            guna2DataGridView1.ClearSelection();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            LoadRecordFil();
        }

        private void cboFilCN_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboFilKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCN();
        }
    }
}
