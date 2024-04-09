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
using System.Drawing.Imaging;
using System.IO;

namespace QuanLyDiemSV.userControl
{
    public partial class ucSinhVien : UserControl
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        dbConnect db = new dbConnect();
        string _title = "Quản Lý Điểm Sinh Viên";
        public ucSinhVien()
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
            cmd = new SqlCommand("Select * from SinhVien", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                guna2DataGridView1.Rows.Add(i, dr["MaSV"].ToString(), dr["TenSV"].ToString(), DateTime.Parse(dr["NgaySinh"].ToString()).ToShortDateString(), dr["QueQuan"].ToString(), dr["SoDienThoai"].ToString(), dr["GioiTinh"].ToString(), dr["MaLop"].ToString(), dr["NienKhoa"].ToString(), dr["Anh"]);
            }
            dr.Close();
            con.Close();
        }

        public void getCN()
        {
            con.Open();
            cmd = new SqlCommand("Select * From ChuyenNganh", con);
            dr = cmd.ExecuteReader();
            cboFilCN.Items.Clear();
            while (dr.Read())
            {
                cboFilCN.Items.Add(dr["TenCN"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void getLop()
        {
            con.Open();
            cmd = new SqlCommand("SELECT Lop.MaLop FROM Lop INNER JOIN ChuyenNganh ON Lop.MaCN = ChuyenNganh.MaCN WHERE ChuyenNganh.TenCN = @TenCN", con);
            cmd.Parameters.AddWithValue("@TenCN", cboFilCN.Text);
            dr = cmd.ExecuteReader();
            cboFilLop.Items.Clear();
            while (dr.Read())
            {
                cboFilLop.Items.Add(dr["MaLop"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void LoadRecordFil()
        {
            int i = 0;
            guna2DataGridView1.Rows.Clear();
            con.Open();
            cmd = new SqlCommand("SELECT * FROM SinhVien Where MaLop=@MaLop", con);
            cmd.Parameters.AddWithValue("@MaLop", cboFilLop.Text);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                guna2DataGridView1.Rows.Add(i, dr["MaSV"].ToString(), dr["TenSV"].ToString(), DateTime.Parse(dr["NgaySinh"].ToString()).ToShortDateString(), dr["QueQuan"].ToString(), dr["SoDienThoai"].ToString(), dr["GioiTinh"].ToString(), dr["MaLop"].ToString(), dr["NienKhoa"].ToString(), dr["Anh"]);
            }
            dr.Close();
            con.Close();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            guna2DataGridView1.ClearSelection();
        }

        private void cboFilCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            getLop();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            LoadRecordFil();
        }

        private void btnThemSV_Click(object sender, EventArgs e)
        {
            frmThemSV f = new frmThemSV(this);
            f.btnEdit.Enabled = false;
            f.getLop();
            f.ShowDialog();
        }

        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

            int r = guna2DataGridView1.CurrentCell.RowIndex;

            string colName = guna2DataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "ColSua")
            {
                frmThemSV f = new frmThemSV(this);
                con.Open();
                cmd = new SqlCommand("Select * from SinhVien where MaSV like '" + guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    f.getLop();
                    f.txtMSV.Text = dr["MaSV"].ToString();
                    f.txtHoTen.Text = dr["TenSV"].ToString();
                    f.dtNgaySinh.Text = dr["NgaySinh"].ToString();
                    f.txtQueQuan.Text = dr["QueQuan"].ToString();
                    f.txtSdt.Text = dr["SoDienThoai"].ToString();
                    f.cboLop.Text = dr["MaLop"].ToString();
                    f.cboGioiTinh.Text = dr["GioiTinh"].ToString();
                    f.txtNienKhoa.Text = dr["NienKhoa"].ToString();
                    f.txtMSV.Enabled = false;

                    byte[] arr = (byte[])guna2DataGridView1.Rows[r].Cells[9].Value;
                    f.imgAnh.Image = ByteArrayToImage(arr);

                    f.btnAdd.Enabled = false;
                    
                }
                con.Close();
                dr.Close();
                f.ShowDialog();

            }

            else if (colName == "ColXoa")
            {
                if (MessageBox.Show("Xác Nhận Xóa?", _title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        con.Open();
                        cmd = new SqlCommand("delete from SinhVien where MaSV = '" + guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Xóa Sinh Viên Thành Công!", _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRecord();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Dữ Liệu Điểm Của Sinh Viên Đang Tồn Tại, Không Thể Xóa!", _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    

                }
            }
        }


        Image ByteArrayToImage(byte[] arr)
        {
            MemoryStream ms = new MemoryStream(arr);
            return Image.FromStream(ms);
        }

        private void checkBoxTaoTK_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTaoTK.Checked == true)
            {
                btnSaveTaoTK.Visible = true;
                DataGridViewCheckBoxColumn checkBoxCol = (DataGridViewCheckBoxColumn)guna2DataGridView1.Columns["checkBoxCol"];
                if (checkBoxCol != null)
                {
                    // Đặt thuộc tính Visible của cột checkBoxCol
                    checkBoxCol.Visible = true;
                }
            } else
            {
                btnSaveTaoTK.Visible = false;
                DataGridViewCheckBoxColumn checkBoxCol = (DataGridViewCheckBoxColumn)guna2DataGridView1.Columns["checkBoxCol"];
                if (checkBoxCol != null)
                {
                    // Đặt thuộc tính Visible của cột checkBoxCol
                    checkBoxCol.Visible = false;
                }
            }
        }

        private void btnSaveTaoTK_Click(object sender, EventArgs e)
        {
            int isInserted = 0;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells["checkBoxCol"].Value);
                if (isSelected)
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT COUNT(*) FROM TaiKhoan WHERE TaiKhoan=@TaiKhoan", con);
                    cmd.Parameters.AddWithValue("@TaiKhoan", row.Cells["Column2"].Value);
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Tài Khoản " + row.Cells["Column2"].Value + " Đã Tồn Tại!", "Cảnh Báo", MessageBoxButtons.OK);
                    } else
                    {
                        cmd = new SqlCommand("INSERT INTO TaiKhoan(TaiKhoan,MatKhau,LoaiTaiKhoan) values(@TaiKhoan,@MatKhau,@LoaiTaiKhoan) ", con);
                        cmd.Parameters.AddWithValue("TaiKhoan", row.Cells["Column2"].Value);
                        cmd.Parameters.AddWithValue("MatKhau", row.Cells["Column2"].Value);
                        cmd.Parameters.AddWithValue("LoaiTaiKhoan", label4.Text);
                        cmd.ExecuteNonQuery();
                        isInserted++;
                    }
                    checkBoxTaoTK.Checked = false;
                    con.Close();

                }
                /*    if (isInserted > 0)
                    {
                       MessageBox.Show(string.Format("{0} Bản Ghi Được Lưu", isInserted), _title);
                    } */
            }
            if (isInserted > 0)
            {
                MessageBox.Show(string.Format("{0} Tài Khoản Được Tạo!", isInserted), _title);
            }
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
            head.Value2 = title + " LỚP: " + cboFilLop.Text;
            head.Font.Bold = true;
            head.Font.Name = "Times New Roman";
            head.Font.Size = "20";
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo tiêu đề cột 

            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");

            cl1.Value2 = "Mã Sinh Viên";

            cl1.ColumnWidth = 30;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");

            cl2.Value2 = "Họ Tên";

            cl2.ColumnWidth = 36;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");

            cl3.Value2 = "Ngày Sinh";
            cl3.ColumnWidth = 12;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");

            cl4.Value2 = "Quê Quán";

            cl4.ColumnWidth = 12;

            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");

            cl5.Value2 = "Điện Thoại";

            cl5.ColumnWidth = 12;

            Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");

            cl6.Value2 = "Giới Tính";

            cl6.ColumnWidth = 20;

            Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");

            cl7.Value2 = "Mã Lớp";

            cl7.ColumnWidth = 24;

            Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("H3", "H3");

            cl8.Value2 = "Niên Khóa";

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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();

            DataColumn col1 = new DataColumn("Mã Sinh Viên");
            DataColumn col2 = new DataColumn("Họ Tên");
            DataColumn col3 = new DataColumn("Ngày Sinh");
            DataColumn col4 = new DataColumn("Quê Quán");
            DataColumn col5 = new DataColumn("Điện Thoại");
            DataColumn col6 = new DataColumn("Giới Tính");
            DataColumn col7 = new DataColumn("Mã Lớp");
            DataColumn col8 = new DataColumn("Niên Khóa");


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


            xuatExcel(dataTable, "Danh Sach Sinh Vien", "DANH SÁCH SINH VIÊN");
        }
    }
}
