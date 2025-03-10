using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_ReverseEnginering.Views
{
    public class OrderDetails
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int TechnicalId { get; set; }


        override public string ToString()
        {
            return $"Id: {Id}, FullName: {FullName}, PhoneNumber: {PhoneNumber}, TechnicalId: {TechnicalId}";
        }
    }
}
