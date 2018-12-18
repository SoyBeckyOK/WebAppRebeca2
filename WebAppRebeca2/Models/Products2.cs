using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppRebeca2.Models
{
    public class Products2
    {
        public int ProductID { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public short Existencias { get; set; }
        public int CategoryID { get; set; }
    }
}