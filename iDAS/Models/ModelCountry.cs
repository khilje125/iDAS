using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iDAS.Models
{
    public class ModelCountry
    {
        public int CountryId{ get; set; }
        public string CuontryNameEN { get; set; }
        public string CuontryNameAR { get; set; }
        public int Status { get; set; }
        public List<ModelCity> City{ get; set; }
    }
}