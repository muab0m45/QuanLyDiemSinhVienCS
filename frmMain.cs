using QuanLyDiemSV.userControl;
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

namespace QuanLyDiemSV
{
    public partial class frmMain : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        dbConnect db = new dbConnect();
        public frmMain()
        {
            InitializeComponent();
            ucHome ucHome = new ucHome();
            addUserControl(ucHome);
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }



        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            ucLop ucLop = new ucLop();
            ucLop.getKhoa();
            ucLop.getCN();
            ucLop.LoadRecord();
            addUserControl(ucLop);
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            ucQuanLy ucQuanLy = new ucQuanLy();
            ucQuanLy.LoadRecord();
            addUserControl(ucQuanLy);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            ucHocPhan ucHocPhan = new ucHocPhan();
            ucHocPhan.LoadRecord();
            ucHocPhan.getCN();
            ucHocPhan.getKhoa();
            ucHocPhan.LoadRecordGVXHP();
            addUserControl(ucHocPhan);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            ucSinhVien ucSinhVien = new ucSinhVien();
            ucSinhVien.LoadRecord();
            ucSinhVien.getCN();
            addUserControl(ucSinhVien); 
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ucHome ucHome = new ucHome();
            addUserControl(ucHome);
            ucHome.countSV();
            ucHome.countLop();
            ucHome.countHP();
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button15_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button25_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label20_Click_1(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox13_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox15_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox16_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox17_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox18_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox19_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox20_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox21_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox22_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button16_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox23_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button17_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button18_Click(object sender, EventArgs e)
        {
            ucDiem ucDiem = new ucDiem();
            string getMaGV = frmDangNhap.CurrentUserName;
            string loaiTK = frmDangNhap.CurrentUserType;
            if (loaiTK == "Sinh Viên")
            {
                ucDiem.txtMSVCN.Text = getMaGV.ToString();
                ucDiem.txtMSVCN.ReadOnly = true;
            }
            ucDiem.getKhoa();
            ucDiem.getCN();
            ucDiem.getHP();
            ucDiem.getKhoaHoc();
            ucDiem.getLopQuanLy();
            ucDiem.getGV();
            ucDiem.getKhoaHoc2();
            ucDiem.getKhoa2();
            ucDiem.getCN2();
            ucDiem.getLopQuanLy2();
            addUserControl(ucDiem);
            ucDiem.guna2TabControl1.TabPages.Remove(ucDiem.tabDiemHP);
            ucDiem.guna2TabControl1.TabPages.Remove(ucDiem.tabDiemLopQL);
        }



        private bool expanding = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (expanding)
            {
                if (dropDownDiem.Height < dropDownDiem.MaximumSize.Height)
                {
                    dropDownDiem.Height += 10;
                    guna2Button1.Checked = false;
                    btnHomeSV.Checked = false;
                    guna2Button3.Checked = false;
                    guna2Button4.Checked = false;
                }
                else
                {
                    timer1.Stop();
                    expanding = false;
                }
            }
            else
            {
                if (dropDownDiem.Height > dropDownDiem.MinimumSize.Height)
                {
                    dropDownDiem.Height -= 10;
                }
                else
                {
                    timer1.Stop();
                    expanding = true;
                    guna2Button7.Checked = false;
                }
            }
        }


        private void guna2Button7_Click_1(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            
            ucDiem ucDiem = new ucDiem();
            string getMaGV = frmDangNhap.CurrentUserName;
            string loaiTK = frmDangNhap.CurrentUserType;
            if (loaiTK == "Giảng Viên")
            {
                ucDiem.checkGVLopQL.Text = getMaGV.ToString();
                ucDiem.checkGVHP.Text = getMaGV.ToString();
            } else if (loaiTK == "Quản Trị")
            {
                ucDiem.checkGVLopQL.Text = string.Empty;
                ucDiem.checkGVHP.Text = string.Empty;
                ucDiem.label54.Text = "Quản Trị Viên";
                ucDiem.label55.Text = "Quản Trị Viên";
                ucDiem.label54.Location = new Point(1500, 25);
                ucDiem.label55.Location = new Point(1500, 25);
            }
            
            ucDiem.getKhoa();
            ucDiem.getCN();
            ucDiem.getHP();
            ucDiem.getKhoaHoc();
            ucDiem.getLopQuanLy();
            ucDiem.getGV();
            ucDiem.getKhoaHoc2();
            ucDiem.getKhoa2();
            ucDiem.getCN2();
            ucDiem.getLopQuanLy2();
            ucDiem.getLopQuanLy3();
            addUserControl(ucDiem);
            
        }
    }
}
