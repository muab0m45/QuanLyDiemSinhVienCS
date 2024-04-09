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
using DGVPrinterHelper;

namespace QuanLyDiemSV.userControl
{
    public partial class ucHocPhan : UserControl
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        dbConnect db = new dbConnect();
        string _title = "Quản Lý Điểm Sinh Viên";
        public ucHocPhan()
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
            cmd = new SqlCommand("Select * from HocPhan", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                guna2DataGridView1.Rows.Add(i, dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString(), dr["PhanTramDQT"].ToString(), dr["PhanTramDT"].ToString(), dr["MaGV"].ToString(), dr["MaKhoa"].ToString(), dr["MaCN"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void LoadRecordFil()
        {
            int i = 0;
            guna2DataGridView1.Rows.Clear();
            con.Open();
            cmd = new SqlCommand("SELECT * FROM HocPhan hp JOIN Khoa k ON hp.MaKhoa = k.MaKhoa JOIN ChuyenNganh cn ON hp.MaCN = cn.MaCN WHERE k.TenKhoa=@TenKhoa AND cn.TenCN=@TenCN", con);
            cmd.Parameters.AddWithValue("@TenKhoa", cboFilKhoa.Text);
            cmd.Parameters.AddWithValue("@TenCN", cboFilCN.Text);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                guna2DataGridView1.Rows.Add(i, dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString(), dr["PhanTramDQT"].ToString(), dr["PhanTramDT"].ToString(), dr["MaGV"].ToString(), dr["MaKhoa"].ToString(), dr["MaCN"].ToString());
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
            cboKhoaXHP.Items.Clear();
            cboKhoaXHP2.Items.Clear();
            while (dr.Read())
            {
                cboFilKhoa.Items.Add(dr["TenKhoa"].ToString());
                cboKhoaXHP.Items.Add(dr["TenKhoa"].ToString());
                cboKhoaXHP2.Items.Add(dr["TenKhoa"].ToString());
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

        public void getCNXHP()
        {
            con.Open();
            cmd = new SqlCommand("SELECT ChuyenNganh.TenCN FROM Khoa JOIN ChuyenNganh ON Khoa.MaKhoa = ChuyenNganh.MaKhoa WHERE Khoa.TenKhoa = @TenKhoa", con);
            cmd.Parameters.AddWithValue("@TenKhoa", cboKhoaXHP.Text);
            dr = cmd.ExecuteReader();
            cboCNXHP.Items.Clear();
            while (dr.Read())
            {
                cboCNXHP.Items.Add(dr["TenCN"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void LoadRecordXHP()
        {
            int i = 0;
            tbXHP.Rows.Clear();
            con.Open();
            cmd = new SqlCommand("SELECT * FROM HocPhan hp JOIN Khoa k ON hp.MaKhoa = k.MaKhoa JOIN ChuyenNganh cn ON hp.MaCN = cn.MaCN WHERE k.TenKhoa=@TenKhoa AND cn.TenCN=@TenCN", con);
            cmd.Parameters.AddWithValue("@TenKhoa", cboKhoaXHP.Text);
            cmd.Parameters.AddWithValue("@TenCN", cboCNXHP.Text);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                tbXHP.Rows.Add(i, dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void LoadRecordGVXHP()
        {
            int i = 0;
            tbGVXHP.Rows.Clear();

            con.Open();
            string query = "SELECT * FROM GiangVien";
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adapter.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                i++;
                tbGVXHP.Rows.Add(i, row["MaGV"].ToString(), row["TenGV"].ToString());
            }

            con.Close();
        }

        /*     public void LoadRecordSVXHP()
                {
                    int i = 0;
                    tbXHP.Rows.Clear();
                    con.Open();
                    cmd = new SqlCommand("SELECT * FROM SinhVien WHERE MaLop=@MaLop", con);
                    cmd.Parameters.AddWithValue("@MaLop", cboLopXHP.Text);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        i++;
                        tbSVXHP.Rows.Add(i, dr["MaSV"].ToString(), dr["TenSV"].ToString(), dr["GioiTinh"].ToString(), dr["NienKhoa"].ToString(), false);
                    }

                    dr.Close();
                    con.Close();
                } */

        public void LoadRecordSVXHP2()
        {
            int i = 0;
            tbSVXHP.Rows.Clear();

            con.Open();
            string query = "SELECT * FROM SinhVien WHERE MaLop=@MaLop";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@MaLop", cboLopXHP.Text);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adapter.Fill(dt);

           foreach (DataRow row in dt.Rows)
            {
                i++;
                tbSVXHP.Rows.Add(i, row["MaSV"].ToString(), row["TenSV"].ToString(), row["GioiTinh"].ToString(), row["NienKhoa"].ToString(), false);
            } 


            con.Close();
        }



        public void getCNXHP2()
        {
            con.Open();
            cmd = new SqlCommand("SELECT ChuyenNganh.TenCN FROM Khoa JOIN ChuyenNganh ON Khoa.MaKhoa = ChuyenNganh.MaKhoa WHERE Khoa.TenKhoa = @TenKhoa", con);
            cmd.Parameters.AddWithValue("@TenKhoa", cboKhoaXHP2.Text);
            dr = cmd.ExecuteReader();
            cboCNXHP2.Items.Clear();
            while (dr.Read())
            {
                cboCNXHP2.Items.Add(dr["TenCN"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void LopXHP()
        {
            con.Open();
            cmd = new SqlCommand("SELECT L.MaLop FROM Lop AS L JOIN Khoa AS K ON L.MaKhoa = K.MaKhoa JOIN ChuyenNganh AS CN ON L.MaCN = CN.MaCN WHERE K.TenKhoa=@TenKhoa AND CN.TenCN=@TenCN AND L.KhoaHoc=@KhoaHoc", con);
            cmd.Parameters.AddWithValue("@KhoaHoc", cboKHXHP.Text);
            cmd.Parameters.AddWithValue("@TenKhoa", cboKhoaXHP2.Text);
            cmd.Parameters.AddWithValue("@TenCN", cboCNXHP2.Text);
            dr = cmd.ExecuteReader();
            cboLopXHP.Items.Clear();
            while (dr.Read())
            {
                cboLopXHP.Items.Add(dr["MaLop"].ToString());
            }
            dr.Close();
            con.Close();
        }


        private void guna2DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            guna2DataGridView1.ClearSelection();
        }

        private void cboFilKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCN();
        }

        private void btnThemLop_Click(object sender, EventArgs e)
        {
            frmThemHP f = new frmThemHP(this);
            f.getGV();
            f.getTextGV();
            f.getKhoa();
            f.getTextKhoa();
            f.getCN();
            f.getTextCN();
            f.btnEdit.Enabled = false;
            f.ShowDialog();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = guna2DataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "ColSua")
            {
                frmThemHP f = new frmThemHP(this);
                f.getGV();
                f.getTextGV();
                f.getKhoa();
                f.getTextKhoa();
                f.getCN();
                f.getTextCN();
                f.txtMaHP.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                f.txtTenHP.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                f.txtSoTC.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                f.txtQT.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                f.txtThi.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                f.cboGV.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                f.cboKhoa.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                f.cboCN.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                f.txtMaHP.Enabled = false;
                f.btnAdd.Enabled = false;
                f.ShowDialog();

            }
            else if (colName == "ColXoa")
            {
                if (MessageBox.Show("Xác Nhận Xóa?", _title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        con.Open();
                        cmd = new SqlCommand("delete from HocPhan where MaHP = '" + guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Xóa Học Phần Thành Công!", _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRecord();
                    } catch
                    {
                        MessageBox.Show("Dữ Liệu Điểm Của Học Phần Đang Tồn Tại, Không Thể Xóa!", _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            LoadRecordFil();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboKhoaXHP_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCNXHP();
        }

        private void cboKhoaXHP2_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCNXHP2();
            LopXHP();
            cboKHXHP.SelectedIndex = -1;
        }

        private void cboCNXHP2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LopXHP();
        }

        private void cboKHXHP_SelectedIndexChanged(object sender, EventArgs e)
        {
            LopXHP();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            LoadRecordXHP();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            LoadRecordSVXHP2();
        }

        private void tbXHP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void tbGVXHP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            int i = 0;
            tbGVXHP.Rows.Clear();

            con.Open();
            string query = "SELECT * FROM GiangVien WHERE MaGV=@MaGV OR TenGV=@TenGV";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@MaGV", txtGVXHP.Text);
            cmd.Parameters.AddWithValue("@TenGV", txtGVXHP.Text);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adapter.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                i++;
                tbGVXHP.Rows.Add(i, row["MaGV"].ToString(), row["TenGV"].ToString());
            }

            con.Close();
        }

        private void btnSaveXHP_Click(object sender, EventArgs e)
        {
            int isInserted = 0;
            foreach (DataGridViewRow row in tbSVXHP.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells["checkBoxCol"].Value);
                if (isSelected)
                {
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO KetQua(MaSV,MaHP,HocKy,NamHoc) values(@MaSV,@MaHP,@HocKy,@NamHoc) ", con);
                    cmd.Parameters.AddWithValue("MaSV", row.Cells["nameCol"].Value);
                    cmd.Parameters.AddWithValue("@MaHP", txtMXHP.Text);
                    cmd.Parameters.AddWithValue("@HocKy", cboHKXHP.Text);
                    cmd.Parameters.AddWithValue("@NamHoc", cboNHXHP.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    isInserted++;
                }
            /*    if (isInserted > 0)
                {
                   MessageBox.Show(string.Format("{0} Bản Ghi Được Lưu", isInserted), _title);
                } */
            }
            if (isInserted > 0)
            {
                MessageBox.Show(string.Format("{0} Sinh Viên Được Xếp Học Phần", isInserted), _title);
            }
        }

        private void tbXHP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMXHP.Text = tbXHP.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtTXHP.Text = tbXHP.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();

            DataColumn col1 = new DataColumn("Mã Học Phần");
            DataColumn col2 = new DataColumn("Tên Học Phần");
            DataColumn col3 = new DataColumn("Số Tín Chỉ");
            DataColumn col4 = new DataColumn("% Quá Trình");
            DataColumn col5 = new DataColumn("% Thi");
            DataColumn col6 = new DataColumn("Mã Giảng Viên");
            DataColumn col7 = new DataColumn("Mã Khoa");
            DataColumn col8 = new DataColumn("Mã Chuyên Ngành");


            dataTable.Columns.Add(col1);
            dataTable.Columns.Add(col2);
            dataTable.Columns.Add(col3);
            dataTable.Columns.Add(col4);
            dataTable.Columns.Add(col5);
            dataTable.Columns.Add(col6);
            dataTable.Columns.Add(col7);
            dataTable.Columns.Add(col8);

            foreach (DataGridViewRow dtgRow in guna2DataGridView1.Rows)
            {
                DataRow dtrow = dataTable.NewRow();

                dtrow[0] = dtgRow.Cells[1].Value;
                dtrow[1] = dtgRow.Cells[2].Value;
                dtrow[2] = dtgRow.Cells[3].Value;
                dtrow[3] = dtgRow.Cells[4].Value;
                dtrow[4] = dtgRow.Cells[5].Value;
                dtrow[5] = dtgRow.Cells[6].Value;
                dtrow[6] = dtgRow.Cells[7].Value;
                dtrow[7] = dtgRow.Cells[8].Value;

                dataTable.Rows.Add(dtrow);
            }


            xuatExcel(dataTable, "Danh Sach Hoc Phan", "DANH SÁCH HỌC PHẦN");
        }

        public void xuatExcel(DataTable dataTable, string sheetName, string title)
        {
            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks oBooks;
            Microsoft.Office.Interop.Excel.Sheets oSheets;
            Microsoft.Office.Interop.Excel.Workbook oBook;
            Microsoft.Office.Interop.Excel.Worksheet oSheet;

            oExcel.Visible = true;
            oExcel.DisplayAlerts = false;
            oExcel.Application.SheetsInNewWorkbook = 1;
            oBooks = oExcel.Workbooks;
            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));
            oSheets = oBook.Worksheets;
            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);
            oSheet.Name = sheetName;

            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "H1");
            head.MergeCells = true;
            head.Value2 = title + " CHUYÊN NGÀNH: " + cboFilCN.Text;
            head.Font.Bold = true;
            head.Font.Name = "Times New Roman";
            head.Font.Size = "16";
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo tiêu đề cột 

            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");

            cl1.Value2 = "Mã Học Phần";

            cl1.ColumnWidth = 20;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");

            cl2.Value2 = "Tên Học Phần";

            cl2.ColumnWidth = 30;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");

            cl3.Value2 = "Số Tín Chỉ";
            cl3.ColumnWidth = 12;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");

            cl4.Value2 = "% Quá Trình";

            cl4.ColumnWidth = 12;

            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");

            cl5.Value2 = "% Thi";

            cl5.ColumnWidth = 12;

            Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");

            cl6.Value2 = "Mã Giảng Viên";

            cl6.ColumnWidth = 20;

            Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");

            cl7.Value2 = "Mã Khoa";

            cl7.ColumnWidth = 24;

            Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("H3", "H3");

            cl8.Value2 = "Mã Chuyên Ngành";

            cl8.ColumnWidth = 24;


            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "H3");

            rowHead.Font.Bold = true;

            // Kẻ viền

            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            // Thiết lập màu nền

            rowHead.Interior.ColorIndex = 4;

            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo mảng theo datatable

            object[,] arr = new object[dataTable.Rows.Count, dataTable.Columns.Count];

            //Chuyển dữ liệu từ DataTable vào mảng đối tượng

            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                DataRow dataRow = dataTable.Rows[row];

                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    arr[row, col] = dataRow[col];
                }
            }

            //Thiết lập vùng điền dữ liệu

            int rowStart = 4;

            int columnStart = 1;

            int rowEnd = rowStart + dataTable.Rows.Count - 1;

            int columnEnd = dataTable.Columns.Count;

            // Ô bắt đầu điền dữ liệu

            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];

            // Ô kết thúc điền dữ liệu

            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];

            // Lấy về vùng điền dữ liệu

            Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);

            //Điền dữ liệu vào vùng đã thiết lập

            range.Value2 = arr;

            // Kẻ viền

            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            //Căn giữa cả bảng 
            oSheet.get_Range(c1, c2).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

        }

        private void tbGVXHP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtGVXHP2.Text = tbGVXHP.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }
    }
}
