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
        public int ID { get; set; }
        public int customerID { get; set; }
        public decimal Shares { get; set; }
        public decimal Fonds { get; set; }
        public decimal Pensions { get; set; }

        public override string ToString()
        {
            return "\nAktien: " + Shares + "\nFonds: " + Fonds + "\nRenten: " + Pensions;
        }
    }
}
