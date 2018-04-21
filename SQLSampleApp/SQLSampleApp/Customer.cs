using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSampleApp
{
    public class Customer : BindableBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return "\nName: " + Name + "\nEMail: " + Mail + "\nTelefonnummer: " + PhoneNumber;
        }
    }

}
