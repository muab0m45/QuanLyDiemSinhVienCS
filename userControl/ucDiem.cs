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
using static System.ComponentModel.Design.ObjectSelectorEditor;
using Guna.UI2.WinForms;
using System.Net.NetworkInformation;

namespace QuanLyDiemSV.userControl
{
    public partial class ucDiem : UserControl
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        dbConnect db = new dbConnect();
        string _title = "Quản Lý Điểm Sinh Viên";
        public ucDiem()
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = db.GetConnection();
            txtQT.TextChanged += Textbox1_TextChanged;
            txtDT.TextChanged += Textbox1_TextChanged;
            txtQT2.TextChanged += Textbox1_TextChanged;
            txtDT2.TextChanged += Textbox1_TextChanged;
        }

        private void Textbox1_TextChanged(object sender, EventArgs e)
        {

            txtTK.Text = "";
            txtTK2.Text = "";
        }


        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

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
                cboKhoaHP.Items.Add(dr["TenKhoa"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void getKhoa2()
        {
            con.Open();
            cmd = new SqlCommand("Select * From Khoa", con);
            dr = cmd.ExecuteReader();
            cboKhoaHP.Items.Clear();
            while (dr.Read())
            {
                cboKhoaHP.Items.Add(dr["TenKhoa"].ToString());
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
            cboCNHP.Items.Clear();
            while (dr.Read())
            {
                cboFilCN.Items.Add(dr["TenCN"].ToString());
                cboCNHP.Items.Add(dr["TenCN"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void getCN2()
        {
            con.Open();
            cmd = new SqlCommand("SELECT ChuyenNganh.TenCN FROM Khoa JOIN ChuyenNganh ON Khoa.MaKhoa = ChuyenNganh.MaKhoa WHERE Khoa.TenKhoa = @TenKhoa", con);
            cmd.Parameters.AddWithValue("@TenKhoa", cboKhoaHP.Text);
            dr = cmd.ExecuteReader();
            cboCNHP.Items.Clear();
            while (dr.Read())
            {
                cboCNHP.Items.Add(dr["TenCN"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void getHP()
        {
            con.Open();
            cmd = new SqlCommand("SELECT hp.MaHP FROM HocPhan hp JOIN ChuyenNganh cn ON hp.MaCN = cn.MaCN WHERE cn.TenCN=@TenCN", con);
            cmd.Parameters.AddWithValue("@TenCN", cboFilCN.Text);
            dr = cmd.ExecuteReader();
            cboFilHP.Items.Clear();
            while (dr.Read())
            {
                cboFilHP.Items.Add(dr["MaHP"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void getKhoaHoc()
        {
            con.Open();
            cmd = new SqlCommand("SELECT DISTINCT l.KhoaHoc FROM Lop l JOIN Khoa k ON l.MaKhoa = k.MaKhoa JOIN ChuyenNganh cn ON l.MaCN = cn.MaCN WHERE k.TenKhoa=@TenKhoa AND cn.TenCN=@TenCN", con);
            cmd.Parameters.AddWithValue("@TenKhoa", cboFilKhoa.Text);
            cmd.Parameters.AddWithValue("@TenCN", cboFilCN.Text);
            dr = cmd.ExecuteReader();
            cboFilKhoas.Items.Clear();
            while (dr.Read())
            {
                cboFilKhoas.Items.Add(dr["KhoaHoc"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void getKhoaHoc2()
        {
            con.Open();
            cmd = new SqlCommand("SELECT DISTINCT l.KhoaHoc FROM Lop l JOIN Khoa k ON l.MaKhoa = k.MaKhoa JOIN ChuyenNganh cn ON l.MaCN = cn.MaCN WHERE k.TenKhoa=@TenKhoa AND cn.TenCN=@TenCN", con);
            cmd.Parameters.AddWithValue("@TenKhoa", cboKhoaHP.Text);
            cmd.Parameters.AddWithValue("@TenCN", cboCNHP.Text);
            dr = cmd.ExecuteReader();
            cboKHP.Items.Clear();
            while (dr.Read())
            {
                cboKHP.Items.Add(dr["KhoaHoc"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void getLopQuanLy()
        {
            con.Open();
            cmd = new SqlCommand("SELECT l.MaLop FROM Lop l JOIN Khoa k ON l.MaKhoa = k.MaKhoa JOIN ChuyenNganh cn ON l.MaCN = cn.MaCN WHERE k.TenKhoa=@TenKhoa AND cn.TenCN=@TenCN AND l.KhoaHoc=@KhoaHoc", con);
            cmd.Parameters.AddWithValue("@TenKhoa", cboFilKhoa.Text);
            cmd.Parameters.AddWithValue("@TenCN", cboFilCN.Text);
            cmd.Parameters.AddWithValue("@KhoaHoc", cboFilKhoas.Text);
            dr = cmd.ExecuteReader();
            cboFilLopQL.Items.Clear();
            while (dr.Read())
            {
                cboFilLopQL.Items.Add(dr["MaLop"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void getLopQuanLy2()
        {
            con.Open();
            cmd = new SqlCommand("SELECT l.MaLop FROM Lop l JOIN Khoa k ON l.MaKhoa = k.MaKhoa JOIN ChuyenNganh cn ON l.MaCN = cn.MaCN WHERE k.TenKhoa=@TenKhoa AND cn.TenCN=@TenCN AND l.KhoaHoc=@KhoaHoc", con);
            cmd.Parameters.AddWithValue("@TenKhoa", cboKhoaHP.Text);
            cmd.Parameters.AddWithValue("@TenCN", cboCNHP.Text);
            cmd.Parameters.AddWithValue("@KhoaHoc", cboKHP.Text);
            dr = cmd.ExecuteReader();
            cboLopQLHP.Items.Clear();
            while (dr.Read())
            {
                cboLopQLHP.Items.Add(dr["MaLop"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void getLopQuanLy3()
        {
            con.Open();
            cmd = new SqlCommand("SELECT * FROM Lop WHERE MaGV=@MaGV", con);
            cmd.Parameters.AddWithValue("@MaGV", checkGVLopQL.Text);
            dr = cmd.ExecuteReader();
            cboFilLopQL.Items.Clear();
            while (dr.Read())
            {
                cboFilLopQL.Items.Add(dr["MaLop"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void LoadRecordHP2()
        {
            int i = 0;
            tbListHP.Rows.Clear();
            con.Open();
            cmd = new SqlCommand("SELECT * FROM HocPhan WHERE MaGV=@MaGV", con);
            cmd.Parameters.AddWithValue("@MaGV", checkGVHP.Text);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                tbListHP.Rows.Add(i, dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void getGV()
        {
            con.Open();
            cmd = new SqlCommand("Select * From GiangVien", con);
            dr = cmd.ExecuteReader();
            cboGVHP.Items.Clear();
            while (dr.Read())
            {
                cboGVHP.Items.Add(dr["MaGV"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void LoadRecord()
        {
            int i = 0;
            tbListSV.Rows.Clear();
            con.Open();
            cmd = new SqlCommand("Select * from SinhVien Where MaLop=@MaLop", con);
            cmd.Parameters.AddWithValue("@MaLop", cboFilLopQL.Text);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                tbListSV.Rows.Add(i, dr["MaSV"].ToString(), dr["TenSV"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void LoadRecordSearch()
        {
            int i = 0;
            tbListSV.Rows.Clear();
            con.Open();
            cmd = new SqlCommand("Select * from SinhVien Where MaLop=@MaLop", con);
            cmd.Parameters.AddWithValue("@MaLop", txtTimLop.Text);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                tbListSV.Rows.Add(i, dr["MaSV"].ToString(), dr["TenSV"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void LoadRecordHP()
        {
            int i = 0;
            tbListHP.Rows.Clear();
            con.Open();
            cmd = new SqlCommand("SELECT HP.MaHP, HP.TenHP, HP.SoTC FROM HocPhan HP JOIN ChuyenNganh CN ON HP.MaCN = CN.MaCN JOIN Khoa K ON CN.MaKhoa = K.MaKhoa WHERE K.TenKhoa=@TenKhoa AND CN.TenCN=@TenCN", con);
            cmd.Parameters.AddWithValue("@TenKhoa", cboKhoaHP.Text);
            cmd.Parameters.AddWithValue("@TenCN", cboCNHP.Text);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                tbListHP.Rows.Add(i, dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString());
            }
            dr.Close();
            con.Close();
        }




        private void cboFilKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCN();
            getKhoaHoc();
        }

        private void cboFilCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            getHP();
            getKhoaHoc();
        }

        private void cboFilKhoas_SelectedIndexChanged(object sender, EventArgs e)
        {
           getLopQuanLy();
        }

        private void cboFilLopQL_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void getDataSV_Click(object sender, EventArgs e)
        {
          /*  con.Open();
            cmd = new SqlCommand("Select * from SinhVien Where MaLop=@MaLop", con);
            cmd.Parameters.AddWithValue("@MaLop", cboFilLopQL.Text);
            treeView1.Nodes.Clear();
            try
            {
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TreeNode node = new TreeNode(dr["TenSV"].ToString());
                    node.Nodes.Add(dr["MaSV"].ToString());

                    treeView1.Nodes.Add(node);

                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!");
            }
            con.Close(); */

            LoadRecord();
            if (checkBoxLopQL.Checked == true)
            {
               LoadRecordSearch();
            }

        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            
        }

        private void tbListSV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        public void LoadRecordKQ()
        {
            int i = 0;
            tbListKQ.Rows.Clear();
            con.Open();
            cmd = new SqlCommand("SELECT KQID,KetQua.MaSV,KetQua.MaHP,DiemQT,DiemThi,HocKy,NamHoc,DiemTong,TenHP,SoTC,PhanTramDQT,PhanTramDT FROM KetQua JOIN HocPhan ON KetQua.MaHP = HocPhan.MaHP WHERE KetQua.MaSV=@MaSV", con);
            cmd.Parameters.AddWithValue("@MaSV", txtMSV.Text);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                double tbHP = 0;
              //  int soTC = Convert.ToInt32(dr["SoTC"]);
                double phanTramDQT = Convert.ToDouble(dr["PhanTramDQT"]);
                double phanTramDT = Convert.ToDouble(dr["PhanTramDT"]);


                
                object diemQTValue = dr["DiemQT"];
                object diemThiValue = dr["DiemThi"];
                if (diemQTValue != DBNull.Value && diemThiValue != DBNull.Value)
                {
                    double diemQT = Convert.ToDouble(diemQTValue);
                    double diemThi = Convert.ToDouble(diemThiValue);
                    tbHP = ((diemQT * phanTramDQT) / 100) + ((diemThi * phanTramDT) / 100);
                } else
                {
                    string tbHPString = tbHP.ToString();
                    if (tbHP == 0)
                    {
                        tbHPString = string.Empty;
                        string diemso = string.Empty;
                        string diemchu = string.Empty;
                        i++;
                        tbListKQ.Rows.Add(i, dr["KQID"].ToString(), dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                        tbHPString.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                    }
                }

            //    double diemQT = Convert.ToDouble(dr["DiemQT"]);

          //      double diemThi = Convert.ToDouble(dr["DiemThi"]);
                
                if (tbHP >= 8.5)
                {
                    int diemso = 4;
                    string diemchu = "A";
                    i++;
                    tbListKQ.Rows.Add(i, dr["KQID"].ToString(), dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                        tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                } else if (tbHP >= 7 && tbHP <= 8.4)
                {
                    int diemso = 3;
                    string diemchu = "B";
                    i++;
                    tbListKQ.Rows.Add(i, dr["KQID"].ToString(), dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                        tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                } else if (tbHP >= 5.5 && tbHP <= 6.9)
                {
                    int diemso = 2;
                    string diemchu = "C";
                    i++;
                    tbListKQ.Rows.Add(i, dr["KQID"].ToString(), dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                        tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                } else if (tbHP >= 4 && tbHP <= 5.4)
                {
                    int diemso = 1;
                    string diemchu = "D";
                    i++;
                    tbListKQ.Rows.Add(i, dr["KQID"].ToString(), dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                        tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                } else if (tbHP < 4 && tbHP > 0)
                {
                    int diemso = 0;
                    string diemchu = "F";
                    i++;
                    tbListKQ.Rows.Add(i, dr["KQID"].ToString(), dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                        tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                } 
                
            }

            dr.Close();
            con.Close();
        }

        public void LoadRecordKQCN()
        {
            int i = 0;
            tbListKQCN.Rows.Clear();
            con.Open();
            cmd = new SqlCommand("SELECT KQID,KetQua.MaSV,KetQua.MaHP,DiemQT,DiemThi,HocKy,NamHoc,DiemTong,TenHP,SoTC,PhanTramDQT,PhanTramDT FROM KetQua JOIN HocPhan ON KetQua.MaHP = HocPhan.MaHP WHERE KetQua.MaSV=@MaSV", con);
            cmd.Parameters.AddWithValue("@MaSV", txtMSVCN.Text);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                double tbHP = 0;
                //  int soTC = Convert.ToInt32(dr["SoTC"]);
                double phanTramDQT = Convert.ToDouble(dr["PhanTramDQT"]);
                double phanTramDT = Convert.ToDouble(dr["PhanTramDT"]);
                object diemQTValue = dr["DiemQT"];
                object diemThiValue = dr["DiemThi"];

                //  double diemQT = Convert.ToDouble(dr["DiemQT"]);
                //  double diemThi = Convert.ToDouble(dr["DiemThi"]);

                // tbHP = ((diemQT * phanTramDQT) / 100) + ((diemThi * phanTramDT) / 100);

                if (diemQTValue != DBNull.Value && diemThiValue != DBNull.Value)
                {
                    double diemQT = Convert.ToDouble(diemQTValue);
                    double diemThi = Convert.ToDouble(diemThiValue);
                    tbHP = ((diemQT * phanTramDQT) / 100) + ((diemThi * phanTramDT) / 100);
                }
                else
                {
                    string tbHPString = tbHP.ToString();
                    if (tbHP == 0)
                    {
                        tbHPString = string.Empty;
                        string diemso = string.Empty;
                        string diemchu = string.Empty;
                        i++;
                        tbListKQCN.Rows.Add(i, dr["KQID"].ToString(), dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                        tbHPString.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                    }
                }
                if (tbHP >= 8.5)
                {
                    int diemso = 4;
                    string diemchu = "A";
                    i++;
                    tbListKQCN.Rows.Add(i, dr["KQID"].ToString(), dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                        tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                }
                else if (tbHP >= 7 && tbHP <= 8.4)
                {
                    int diemso = 3;
                    string diemchu = "B";
                    i++;
                    tbListKQCN.Rows.Add(i, dr["KQID"].ToString(), dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                        tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                }
                else if (tbHP >= 5.5 && tbHP <= 6.9)
                {
                    int diemso = 2;
                    string diemchu = "C";
                    i++;
                    tbListKQCN.Rows.Add(i, dr["KQID"].ToString(), dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                        tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                }
                else if (tbHP >= 4 && tbHP <= 5.4)
                {
                    int diemso = 1;
                    string diemchu = "D";
                    i++;
                    tbListKQCN.Rows.Add(i, dr["KQID"].ToString(), dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                        tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                }
                else if (tbHP < 4 && tbHP > 0)
                {
                    int diemso = 0;
                    string diemchu = "F";
                    i++;
                    tbListKQCN.Rows.Add(i, dr["KQID"].ToString(), dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                        tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                }

            }

            dr.Close();
            con.Close();
        }

        public void LoadRecordKQCNFil()
        {
            int i = 0;
            tbListKQCN.Rows.Clear();
            con.Open();
            cmd = new SqlCommand("SELECT KQID,KetQua.MaSV,KetQua.MaHP,DiemQT,DiemThi,HocKy,NamHoc,DiemTong,TenHP,SoTC,PhanTramDQT,PhanTramDT FROM KetQua JOIN HocPhan ON KetQua.MaHP = HocPhan.MaHP WHERE KetQua.HocKy=@HocKy AND KetQua.NamHoc=@NamHoc AND KetQua.MaSV=@MaSV", con);
            cmd.Parameters.AddWithValue("@HocKy", cboHKCN.Text);
            cmd.Parameters.AddWithValue("@NamHoc", cboNHCN.Text);
            cmd.Parameters.AddWithValue("@MaSV", txtMSVCN.Text);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                double tbHP = 0;
                //  int soTC = Convert.ToInt32(dr["SoTC"]);
                double phanTramDQT = Convert.ToDouble(dr["PhanTramDQT"]);
                double phanTramDT = Convert.ToDouble(dr["PhanTramDT"]);
                object diemQTValue = dr["DiemQT"];
                object diemThiValue = dr["DiemThi"];

                //  double diemQT = Convert.ToDouble(dr["DiemQT"]);
                //  double diemThi = Convert.ToDouble(dr["DiemThi"]);

                // tbHP = ((diemQT * phanTramDQT) / 100) + ((diemThi * phanTramDT) / 100);

                if (diemQTValue != DBNull.Value && diemThiValue != DBNull.Value)
                {
                    double diemQT = Convert.ToDouble(diemQTValue);
                    double diemThi = Convert.ToDouble(diemThiValue);
                    tbHP = ((diemQT * phanTramDQT) / 100) + ((diemThi * phanTramDT) / 100);
                }
                else
                {
                    string tbHPString = tbHP.ToString();
                    if (tbHP == 0)
                    {
                        tbHPString = string.Empty;
                        string diemso = string.Empty;
                        string diemchu = string.Empty;
                        i++;
                        tbListKQCN.Rows.Add(i, dr["KQID"].ToString(), dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                        tbHPString.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                    }
                }

               // tbHP = ((diemQT * phanTramDQT) / 100) + ((diemThi * phanTramDT) / 100);
                if (tbHP >= 8.5)
                {
                    int diemso = 4;
                    string diemchu = "A";
                    i++;
                    tbListKQCN.Rows.Add(i, dr["KQID"].ToString(), dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                        tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                }
                else if (tbHP >= 7 && tbHP <= 8.4)
                {
                    int diemso = 3;
                    string diemchu = "B";
                    i++;
                    tbListKQCN.Rows.Add(i, dr["KQID"].ToString(), dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                        tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                }
                else if (tbHP >= 5.5 && tbHP <= 6.9)
                {
                    int diemso = 2;
                    string diemchu = "C";
                    i++;
                    tbListKQCN.Rows.Add(i, dr["KQID"].ToString(), dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                        tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                }
                else if (tbHP >= 4 && tbHP <= 5.4)
                {
                    int diemso = 1;
                    string diemchu = "D";
                    i++;
                    tbListKQCN.Rows.Add(i, dr["KQID"].ToString(), dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                        tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                }
                else if (tbHP < 4 && tbHP > 0)
                {
                    int diemso = 0;
                    string diemchu = "F";
                    i++;
                    tbListKQCN.Rows.Add(i, dr["KQID"].ToString(), dr["MaHP"].ToString(), dr["TenHP"].ToString(), dr["SoTC"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                        tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                }

            }

            dr.Close();
            con.Close();
        }
        public void LoadRecordKQHP()
        {
            int i = 0;
            tbListKQHP.Rows.Clear();
            con.Open();
            SqlCommand cmd = null;
            if (!string.IsNullOrEmpty(txtMaHP2.Text))
            {
                cmd = new SqlCommand("SELECT * FROM SinhVien " +
                                     "JOIN KetQua ON SinhVien.MaSV = KetQua.MaSV " +
                                     "JOIN HocPhan ON KetQua.MaHP = HocPhan.MaHP " +
                                     "WHERE KetQua.MaHP=@MaHP", con);
                cmd.Parameters.AddWithValue("@MaHP", txtMaHP2.Text);
                dr = cmd.ExecuteReader();
            }
     /*       else if (!string.IsNullOrEmpty(cboLopQLHP.Text) && string.IsNullOrEmpty(txtMaHP2.Text))
            {
                cmd = new SqlCommand("SELECT * FROM SinhVien " +
                                     "JOIN KetQua ON SinhVien.MaSV = KetQua.MaSV " +
                                     "JOIN HocPhan ON KetQua.MaHP = HocPhan.MaHP " +
                                     "WHERE KetQua.MaHP=@MaHP AND SinhVien.MaLop=@MaLop", con);
                cmd.Parameters.AddWithValue("@MaHP", txtMaHP2.Text);
                cmd.Parameters.AddWithValue("@MaLop", cboLopQLHP.Text);
                dr = cmd.ExecuteReader();
            }
            else if (guna2CustomCheckBox1.Checked && !string.IsNullOrEmpty(txtMaHP2.Text) && !string.IsNullOrEmpty(txtLopQLHP.Text))
            {
                cmd = new SqlCommand("SELECT * FROM SinhVien " +
                                     "JOIN KetQua ON SinhVien.MaSV = KetQua.MaSV " +
                                     "JOIN HocPhan ON KetQua.MaHP = HocPhan.MaHP " +
                                     "WHERE KetQua.MaHP=@MaHP AND SinhVien.MaLop=@MaLop", con);
                cmd.Parameters.AddWithValue("@MaHP", txtMaHP2.Text);
                cmd.Parameters.AddWithValue("@MaLop", txtLopQLHP.Text);
                dr = cmd.ExecuteReader();
            } */

            //  dr = cmd.ExecuteReader();
            if (cmd != null)
            {
                while (dr.Read())
                {
                    //  int soTC = Convert.ToInt32(dr["SoTC"]);

                    double tbHP = 0;
                    double phanTramDQT = Convert.ToDouble(dr["PhanTramDQT"]);
                    double phanTramDT = Convert.ToDouble(dr["PhanTramDT"]);

                    //      double phanTramDQT = Convert.ToDouble(dr["PhanTramDQT"]);
                    //     double phanTramDT = Convert.ToDouble(dr["PhanTramDT"]);

                    object diemQTValue = dr["DiemQT"];
                    object diemThiValue = dr["DiemThi"];

                    //  double diemQT = Convert.ToDouble(dr["DiemQT"]);
                    //  double diemThi = Convert.ToDouble(dr["DiemThi"]);

                    if (diemQTValue != DBNull.Value && diemThiValue != DBNull.Value)
                    {
                        double diemQT = Convert.ToDouble(diemQTValue);
                        double diemThi = Convert.ToDouble(diemThiValue);
                        tbHP = ((diemQT * phanTramDQT) / 100) + ((diemThi * phanTramDT) / 100);
                    } else
                    {
                        string tbHPString = tbHP.ToString();
                        if (tbHP == 0)
                        {
                            tbHPString = string.Empty;
                            string diemso = string.Empty;
                            string diemchu = string.Empty;
                            i++;
                            tbListKQHP.Rows.Add(i, dr["KQID"].ToString(), dr["MaSV"].ToString(), dr["TenSV"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                            tbHPString.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                        }
                    }

                 //   double tbHP = ((diemQT * phanTramDQT) / 100) + ((diemThi * phanTramDT) / 100);
                    if (tbHP >= 8.5)
                    {
                        int diemso = 4;
                        string diemchu = "A";
                        i++;
                        tbListKQHP.Rows.Add(i, dr["KQID"].ToString(), dr["MaSV"].ToString(), dr["TenSV"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                            tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                    }
                    else if (tbHP >= 7 && tbHP <= 8.4)
                    {
                        int diemso = 3;
                        string diemchu = "B";
                        i++;
                        tbListKQHP.Rows.Add(i, dr["KQID"].ToString(), dr["MaSV"].ToString(), dr["TenSV"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                            tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                    }
                    else if (tbHP >= 5.5 && tbHP <= 6.9)
                    {
                        int diemso = 2;
                        string diemchu = "C";
                        i++;
                        tbListKQHP.Rows.Add(i, dr["KQID"].ToString(), dr["MaSV"].ToString(), dr["TenSV"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                            tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                    }
                    else if (tbHP >= 4 && tbHP <= 5.4)
                    {
                        int diemso = 1;
                        string diemchu = "D";
                        i++;
                        tbListKQHP.Rows.Add(i, dr["KQID"].ToString(), dr["MaSV"].ToString(), dr["TenSV"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                            tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                    }
                    else if (tbHP < 4 && tbHP > 0)
                    {
                        int diemso = 0;
                        string diemchu = "F";
                        i++;
                        tbListKQHP.Rows.Add(i, dr["KQID"].ToString(), dr["MaSV"].ToString(), dr["TenSV"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                            tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                    }

                }
            }
            dr.Close();
            con.Close();
        }

        public void LoadRecordKQHPinclMaLop()
        {
            int i = 0;
            tbListKQHP.Rows.Clear();
            con.Open();
            SqlCommand cmd = null;
                if (cboLopQLHP.SelectedItem != null && !string.IsNullOrEmpty(txtMaHP2.Text))
                   {
                       cmd = new SqlCommand("SELECT * FROM SinhVien " +
                                            "JOIN KetQua ON SinhVien.MaSV = KetQua.MaSV " +
                                            "JOIN HocPhan ON KetQua.MaHP = HocPhan.MaHP " +
                                            "WHERE KetQua.MaHP=@MaHP AND SinhVien.MaLop=@MaLop", con);
                       cmd.Parameters.AddWithValue("@MaHP", txtMaHP2.Text);
                       cmd.Parameters.AddWithValue("@MaLop", cboLopQLHP.SelectedItem);
                       dr = cmd.ExecuteReader();
                   }

            //  dr = cmd.ExecuteReader();
            if (cmd != null)
            {
                while (dr.Read())
                {
                    //  int soTC = Convert.ToInt32(dr["SoTC"]);
                    double phanTramDQT = Convert.ToDouble(dr["PhanTramDQT"]);
                    double phanTramDT = Convert.ToDouble(dr["PhanTramDT"]);

                    double diemQT = Convert.ToDouble(dr["DiemQT"]);

                    double diemThi = Convert.ToDouble(dr["DiemThi"]);

                    double tbHP = ((diemQT * phanTramDQT) / 100) + ((diemThi * phanTramDT) / 100);
                    if (tbHP >= 8.5)
                    {
                        int diemso = 4;
                        string diemchu = "A";
                        i++;
                        tbListKQHP.Rows.Add(i, dr["KQID"].ToString(), dr["MaSV"].ToString(), dr["TenSV"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                            tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                    }
                    else if (tbHP >= 7 && tbHP <= 8.4)
                    {
                        int diemso = 3;
                        string diemchu = "B";
                        i++;
                        tbListKQHP.Rows.Add(i, dr["KQID"].ToString(), dr["MaSV"].ToString(), dr["TenSV"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                            tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                    }
                    else if (tbHP >= 5.5 && tbHP <= 6.9)
                    {
                        int diemso = 2;
                        string diemchu = "C";
                        i++;
                        tbListKQHP.Rows.Add(i, dr["KQID"].ToString(), dr["MaSV"].ToString(), dr["TenSV"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                            tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                    }
                    else if (tbHP >= 4 && tbHP <= 5.4)
                    {
                        int diemso = 1;
                        string diemchu = "D";
                        i++;
                        tbListKQHP.Rows.Add(i, dr["KQID"].ToString(), dr["MaSV"].ToString(), dr["TenSV"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                            tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                    }
                    else if (tbHP < 4)
                    {
                        int diemso = 0;
                        string diemchu = "F";
                        i++;
                        tbListKQHP.Rows.Add(i, dr["KQID"].ToString(), dr["MaSV"].ToString(), dr["TenSV"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                            tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                    }

                }
            }
            dr.Close();
            con.Close();
        }

        public void LoadRecordKQHPinclMaLopSearch()
        {
            int i = 0;
            tbListKQHP.Rows.Clear();
            con.Open();
            SqlCommand cmd = null;
            if (!string.IsNullOrEmpty(txtLopQLHP.Text) && !string.IsNullOrEmpty(txtMaHP2.Text))
            {
                cmd = new SqlCommand("SELECT * FROM SinhVien " +
                                     "JOIN KetQua ON SinhVien.MaSV = KetQua.MaSV " +
                                     "JOIN HocPhan ON KetQua.MaHP = HocPhan.MaHP " +
                                     "WHERE KetQua.MaHP=@MaHP AND SinhVien.MaLop=@MaLop", con);
                cmd.Parameters.AddWithValue("@MaHP", txtMaHP2.Text);
                cmd.Parameters.AddWithValue("@MaLop", txtLopQLHP.Text);
                dr = cmd.ExecuteReader();
            }

            //  dr = cmd.ExecuteReader();
            if (cmd != null)
            {
                while (dr.Read())
                {
                    //  int soTC = Convert.ToInt32(dr["SoTC"]);
                    double phanTramDQT = Convert.ToDouble(dr["PhanTramDQT"]);
                    double phanTramDT = Convert.ToDouble(dr["PhanTramDT"]);

                    double diemQT = Convert.ToDouble(dr["DiemQT"]);

                    double diemThi = Convert.ToDouble(dr["DiemThi"]);

                    double tbHP = ((diemQT * phanTramDQT) / 100) + ((diemThi * phanTramDT) / 100);
                    if (tbHP >= 8.5)
                    {
                        int diemso = 4;
                        string diemchu = "A";
                        i++;
                        tbListKQHP.Rows.Add(i, dr["KQID"].ToString(), dr["MaSV"].ToString(), dr["TenSV"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                            tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                    }
                    else if (tbHP >= 7 && tbHP <= 8.4)
                    {
                        int diemso = 3;
                        string diemchu = "B";
                        i++;
                        tbListKQHP.Rows.Add(i, dr["KQID"].ToString(), dr["MaSV"].ToString(), dr["TenSV"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                            tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                    }
                    else if (tbHP >= 5.5 && tbHP <= 6.9)
                    {
                        int diemso = 2;
                        string diemchu = "C";
                        i++;
                        tbListKQHP.Rows.Add(i, dr["KQID"].ToString(), dr["MaSV"].ToString(), dr["TenSV"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                            tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                    }
                    else if (tbHP >= 4 && tbHP <= 5.4)
                    {
                        int diemso = 1;
                        string diemchu = "D";
                        i++;
                        tbListKQHP.Rows.Add(i, dr["KQID"].ToString(), dr["MaSV"].ToString(), dr["TenSV"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                            tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                    }
                    else if (tbHP < 4)
                    {
                        int diemso = 0;
                        string diemchu = "F";
                        i++;
                        tbListKQHP.Rows.Add(i, dr["KQID"].ToString(), dr["MaSV"].ToString(), dr["TenSV"].ToString(), dr["DiemQT"].ToString(), dr["DiemThi"].ToString(),
                            tbHP.ToString(), diemso.ToString(), diemchu.ToString(), dr["HocKy"].ToString(), dr["NamHoc"].ToString());
                    }

                }
            }
            dr.Close();
            con.Close();
        }

        private void tbListKQ_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        public void Clear()
        {
            txtMaHP.Text = String.Empty;
            txtTenHP.Text = String.Empty;
            txtSoTC.Text = String.Empty;
            txtQT.Text = String.Empty;
            txtHK.Text = String.Empty;
            txtNH.Text = String.Empty;
            txtDT.Text = String.Empty;
            lbKQID.Text = String.Empty;
            txtTK.Text = String.Empty;
            txtMSV2.Text = String.Empty;
            txtHK2.Text = String.Empty;
            txtNH2.Text = String.Empty;
            txtQT2.Text = String.Empty;
            txtDT2.Text = String.Empty;
            txtTK2.Text = String.Empty;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("UPDATE KetQua set DiemQT=@DiemQT,DiemThi=@DiemThi Where KQID=@KQID", con);
                cmd.Parameters.AddWithValue("@DiemQT", txtQT.Text);
                cmd.Parameters.AddWithValue("@DiemThi", txtDT.Text);
                cmd.Parameters.AddWithValue("@KQID", lbKQID.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Sửa Điểm Học Phần Thành Công!", _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                LoadRecordKQ();
                tinhDiemTong();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, _title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void tinhDiemTong()
        {
            double diemTong10 = 0;
            double tbcTichLuy10 = 0;
            double diemTong4 = 0;
            double tbcTichLuy4 = 0;
            int tongSoTC = 0;
            foreach (DataGridViewRow row in tbListKQ.Rows)
            {
                if (row.Cells[4].Value != null && !string.IsNullOrEmpty(row.Cells[4].Value.ToString()))
                {
                    int soTC = Convert.ToInt32(row.Cells[4].Value);
                    tongSoTC += soTC;

                    if (row.Cells[7].Value != null && !string.IsNullOrEmpty(row.Cells[7].Value.ToString()))
                    {
                      double diemTBCHP = Convert.ToDouble(row.Cells[7].Value);

                        diemTong10 += (diemTBCHP * soTC);
                        tbcTichLuy10 = Math.Round(diemTong10 / tongSoTC, 2);
                    }
                    if (row.Cells[8].Value != null && !string.IsNullOrEmpty(row.Cells[8].Value.ToString()))
                    {
                        double diemSo = Convert.ToDouble(row.Cells[8].Value);
                        diemTong4 += (diemSo * soTC);
                        tbcTichLuy4 = Math.Round(diemTong4 / tongSoTC, 2);
                    }
                }
                label16.Text = tongSoTC.ToString();
                label17.Text = tbcTichLuy10.ToString();
                label18.Text = tbcTichLuy4.ToString();

                if (label25.Text != null)
                {
                    if (tbcTichLuy10 >= 9)
                    {
                        label25.Text = "Xuất Sắc";
                    }
                    else if (tbcTichLuy10 >= 8 && tbcTichLuy10 <= 9)
                    {
                        label25.Text = "Giỏi";
                    }
                    else if (tbcTichLuy10 >= 7 && tbcTichLuy10 <= 8)
                    {
                        label25.Text = "Khá";
                    }
                    else if (tbcTichLuy10 >= 5 && tbcTichLuy10 <= 7)
                    {
                        label25.Text = "Trung Bình";
                    }
                    else if (tbcTichLuy10 == 0 && tbcTichLuy10 == 0)
                    {
                        label25.Text = "N/A";
                    }
                }
                if (label24.Text != null)
                {
                    if (tbcTichLuy4 >= 3.6)
                    {
                        label24.Text = "Xuất Sắc";
                    }
                    else if (tbcTichLuy4 >= 3.20 && tbcTichLuy4 <= 3.59)
                    {
                        label24.Text = "Giỏi";
                    }
                    else if (tbcTichLuy4 >= 2.5 && tbcTichLuy4 <= 3.19)
                    {
                        label24.Text = "Khá";
                    }
                    else if (tbcTichLuy4 >= 2 && tbcTichLuy4 <= 2.49)
                    {
                        label24.Text = "Trung Bình";
                    }
                    else if (tbcTichLuy4 == 0 && tbcTichLuy4 == 0)
                    {
                        label24.Text = "N/A";
                    }
                }

            }
        }

        public void tinhDiemTongCN()
        {
            double diemTong10 = 0;
            double tbcTichLuy10 = 0;
            double diemTong4 = 0;
            double tbcTichLuy4 = 0;
            int tongSoTC = 0;
            foreach (DataGridViewRow row in tbListKQCN.Rows)
            {
                if (row.Cells[4].Value != null && !string.IsNullOrEmpty(row.Cells[4].Value.ToString()))
                {
                    int soTC = Convert.ToInt32(row.Cells[4].Value);
                    tongSoTC += soTC;

                    if (row.Cells[7].Value != null && !string.IsNullOrEmpty(row.Cells[7].Value.ToString()))
                    {
                        double diemTBCHP = Convert.ToDouble(row.Cells[7].Value);

                        diemTong10 += (diemTBCHP * soTC);
                        tbcTichLuy10 = Math.Round(diemTong10 / tongSoTC, 2);
                    }
                    if (row.Cells[8].Value != null && !string.IsNullOrEmpty(row.Cells[8].Value.ToString()))
                    {
                        double diemSo = Convert.ToDouble(row.Cells[8].Value);
                        diemTong4 += (diemSo * soTC);
                        tbcTichLuy4 = Math.Round(diemTong4 / tongSoTC, 2);
                    }
                }
                label47.Text = tongSoTC.ToString();
                label46.Text = tbcTichLuy10.ToString();
                label44.Text = tbcTichLuy4.ToString();

                if (label41.Text != null)
                {
                    if (tbcTichLuy10 >= 9)
                    {
                        label41.Text = "Xuất Sắc";
                    }
                    else if (tbcTichLuy10 >= 8 && tbcTichLuy10 <= 9)
                    {
                        label41.Text = "Giỏi";
                    }
                    else if (tbcTichLuy10 >= 7 && tbcTichLuy10 <= 8)
                    {
                        label41.Text = "Khá";
                    }
                    else if (tbcTichLuy10 >= 5 && tbcTichLuy10 <= 7)
                    {
                        label41.Text = "Trung Bình";
                    }
                    else if (tbcTichLuy10 == 0)
                    {
                        label41.Text = "N/A";
                    }
                }
                if (label42.Text != null)
                {
                    if (tbcTichLuy4 >= 3.6)
                    {
                        label42.Text = "Xuất Sắc";
                    }
                    else if (tbcTichLuy4 >= 3.20 && tbcTichLuy4 <= 3.59)
                    {
                        label42.Text = "Giỏi";
                    }
                    else if (tbcTichLuy4 >= 2.5 && tbcTichLuy4 <= 3.19)
                    {
                        label42.Text = "Khá";
                    }
                    else if (tbcTichLuy4 >= 2 && tbcTichLuy4 <= 2.49)
                    {
                        label42.Text = "Trung Bình";
                    }
                    else if (tbcTichLuy4 == 0)
                    {
                        label42.Text = "N/A";
                    }
                }

            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Clear();
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
                e.Handled = value < 0 || value > 10; 
            }
        }

        private void txtDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }


            if (char.IsDigit(e.KeyChar))
            {
                int value = int.Parse(txtDT.Text + e.KeyChar);
                e.Handled = value < 0 || value > 10;
            }
        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox1_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxLopQL_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLopQL.Checked == true)
            {
                cboFilLopQL.Visible = false;
                label5.Text = "Tìm Kiếm Lớp";
                txtTimLop.Visible = true;
            } else
            {
                cboFilLopQL.Visible = true;
                label5.Text = "Lớp Quản Lý";
                txtTimLop.Visible = false;
            }
        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void tbListHP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            LoadRecordHP();
        }

        private void tbListKQHP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void txtDT2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }


            if (char.IsDigit(e.KeyChar))
            {
                int value = int.Parse(txtDT.Text + e.KeyChar);
                e.Handled = value < 0 || value > 10;
            }
        }

        private void txtQT2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }


            if (char.IsDigit(e.KeyChar))
            {
                int value = int.Parse(txtDT.Text + e.KeyChar);
                e.Handled = value < 0 || value > 10;
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("UPDATE KetQua set DiemQT=@DiemQT,DiemThi=@DiemThi Where KQID=@KQID", con);
                cmd.Parameters.AddWithValue("@DiemQT", txtQT2.Text);
                cmd.Parameters.AddWithValue("@DiemThi", txtDT2.Text);
                cmd.Parameters.AddWithValue("@KQID", lbKQID2.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Sửa Điểm Học Phần Thành Công!", _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                LoadRecordKQHP();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, _title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void cboKhoaHP_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCN2();
            getKhoaHoc2();
        }

        private void cboCNHP_SelectedIndexChanged(object sender, EventArgs e)
        {
            getKhoaHoc2();
        }

        private void guna2CustomCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox1.Checked == true)
            {
                cboLopQLHP.Visible = false;
                label28.Text = "Tìm Kiếm Lớp";
                txtLopQLHP.Visible = true;
            }
            else
            {
                cboLopQLHP.Visible = true;
                label28.Text = "Lớp Quản Lý";
                txtLopQLHP.Visible = false;
            }
        }

        private void cboKHP_SelectedIndexChanged(object sender, EventArgs e)
        {
            getLopQuanLy2();
        }

        private void txtLopQLHP_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cboLopQLHP_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2GroupBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (txtMSVCN.Text != null)
            {
                LoadRecordKQCN();
                tinhDiemTongCN();
            }
        }

        private void cboNHCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNHCN.SelectedItem != null && cboHKCN.SelectedItem != null)
            {
                LoadRecordKQCNFil();
                tinhDiemTongCN();
            }
        }

        private void btnF5CN_Click(object sender, EventArgs e)
        {
            cboHKCN.SelectedIndex = -1;
            cboNHCN.SelectedIndex = -1;
            LoadRecordKQCN();
            tinhDiemTongCN();
        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void tbListSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMSV.Text = tbListSV.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            LoadRecordKQ();
            tinhDiemTong();
            Clear();
        }

        private void tbListKQ_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaHP.Text = tbListKQ.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtTenHP.Text = tbListKQ.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtSoTC.Text = tbListKQ.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtQT.Text = tbListKQ.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtDT.Text = tbListKQ.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtTK.Text = tbListKQ.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtHK.Text = tbListKQ.Rows[e.RowIndex].Cells[10].Value.ToString();
            txtNH.Text = tbListKQ.Rows[e.RowIndex].Cells[11].Value.ToString();
            lbKQID.Text = tbListKQ.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void tbListHP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaHP2.Text = tbListHP.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtTenHP2.Text = tbListHP.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtSoTC2.Text = tbListHP.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
            if (cboLopQLHP.SelectedItem != null && !string.IsNullOrEmpty(txtMaHP2.Text))
            {
                LoadRecordKQHPinclMaLop();
            }
            else if (!string.IsNullOrEmpty(txtMaHP2.Text))
            {
                LoadRecordKQHP();
            }
            else if (!string.IsNullOrEmpty(txtLopQLHP.Text) && !string.IsNullOrEmpty(txtMaHP2.Text))
            {

                LoadRecordKQHPinclMaLopSearch();
            }
        }

        private void tbListKQHP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMSV2.Text = tbListKQHP.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtHK2.Text = tbListKQHP.Rows[e.RowIndex].Cells[9].Value.ToString();
            txtNH2.Text = tbListKQHP.Rows[e.RowIndex].Cells[10].Value.ToString();
            txtQT2.Text = tbListKQHP.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtDT2.Text = tbListKQHP.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtTK2.Text = tbListKQHP.Rows[e.RowIndex].Cells[6].Value.ToString();
            lbKQID2.Text = tbListKQHP.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void checkGVHP_TextChanged(object sender, EventArgs e)
        {
            if (checkGVHP.Text.Length > 0)
            {
                LoadRecordHP2();
                groupBox3.Enabled = false;
            }
        }

        private void checkGVLopQL_TextChanged(object sender, EventArgs e)
        {
            if (checkGVLopQL.Text.Length > 0)
            {
                getLopQuanLy3();
                cboFilKhoa.Enabled = false;
                cboFilCN.Enabled = false;
                cboFilHP.Enabled = false;
                cboFilKhoas.Enabled = false;
                checkBoxLopQL.Visible = false;

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

            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "G1");
            head.MergeCells = true;
            if (cboLopQLHP.SelectedIndex != -1)
            {
                head.Value2 = title + " (Học Phần: " + txtMaHP2.Text + "   Lớp Quản Lý: " + cboLopQLHP.SelectedItem + ")";
            } else
            {
                head.Value2 = title + " (Học Phần: " + txtMaHP2.Text + ")";
            }
            
            head.Font.Bold = true;
            head.Font.Name = "Times New Roman";
            head.Font.Size = "16";
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo tiêu đề cột 

            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");

            cl1.Value2 = "Mã Sinh Viên";

            cl1.ColumnWidth = 20;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");

            cl2.Value2 = "Họ Tên";

            cl2.ColumnWidth = 30;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");

            cl3.Value2 = "Điểm QT";
            cl3.ColumnWidth = 16;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");

            cl4.Value2 = "Điểm Thi";

            cl4.ColumnWidth = 16;

            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");

            cl5.Value2 = "TBCHP";

            cl5.ColumnWidth = 16;

            Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");

            cl6.Value2 = "Điểm Số";

            cl6.ColumnWidth = 16;

            Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");

            cl7.Value2 = "Điểm Chữ";

            cl7.ColumnWidth = 16;


            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "G3");

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

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();

            DataColumn col1 = new DataColumn("Mã Sinh Viên");
            DataColumn col2 = new DataColumn("Họ Tên");
            DataColumn col3 = new DataColumn("Điểm QT");
            DataColumn col4 = new DataColumn("Điểm Thi");
            DataColumn col5 = new DataColumn("TBCHP");
            DataColumn col6 = new DataColumn("Điểm Số");
            DataColumn col7 = new DataColumn("Điểm Chữ");


            dataTable.Columns.Add(col1);
            dataTable.Columns.Add(col2);
            dataTable.Columns.Add(col3);
            dataTable.Columns.Add(col4);
            dataTable.Columns.Add(col5);
            dataTable.Columns.Add(col6);
            dataTable.Columns.Add(col7);

            foreach (DataGridViewRow dtgRow in tbListKQHP.Rows)
            {
                DataRow dtrow = dataTable.NewRow();

                dtrow[0] = dtgRow.Cells[2].Value;
                dtrow[1] = dtgRow.Cells[3].Value;
                dtrow[2] = dtgRow.Cells[4].Value;
                dtrow[3] = dtgRow.Cells[5].Value;
                dtrow[4] = dtgRow.Cells[6].Value;
                dtrow[5] = dtgRow.Cells[7].Value;
                dtrow[6] = dtgRow.Cells[8].Value;

                dataTable.Rows.Add(dtrow);
            }


            xuatExcel(dataTable, "Danh Sach Hoc Phan", "Bảng Điểm");
        }
    }
}
