using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDiemSV
{
    internal class dbConnect
    {

        public string GetConnection()
        {
            string con = "Data Source=MANH;Initial Catalog=QLDiemSV;Integrated Security=True";
            return con ;
        }
    }
}
