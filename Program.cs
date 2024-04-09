using QuanLyDiemSV.userControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDiemSV
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            frmMain mainForm = new frmMain();

            frmDangNhap dangNhapForm = new frmDangNhap(mainForm);

            Application.EnableVisualStyles();
          //  Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(dangNhapForm);
        }
    }
}
