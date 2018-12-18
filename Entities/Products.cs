using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class Products
    {
        public int ProductID { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public short Existencias { get; set; }
        public int CategoryID { get; set; }
    }
}