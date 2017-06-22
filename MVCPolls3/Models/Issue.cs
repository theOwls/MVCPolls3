using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPolls3.Models
{
    public enum Postcode
    {
        AB10,AB11,AB12
    }

    public class Issue
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int Votes { get; set; }
        public Postcode? Postcode { get; set; }
    }
}
