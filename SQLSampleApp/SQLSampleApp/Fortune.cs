using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSampleApp
{
    public class Fortune : BindableBase
    {
        public int customerID { get; set; }
        public double Shares { get; set; }
        public double Fonds { get; set; }
        public double Pensions { get; set; }

        public override string ToString()
        {
            return "\nAktien: " + Shares + "\nFonds: " + Fonds + "\nRenten: " + Pensions;
        }
    }
}
