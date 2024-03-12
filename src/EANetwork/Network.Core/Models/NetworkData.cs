using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Core.Models
{
    public class NetworkData
    {
        public List<Branch> Branches { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
