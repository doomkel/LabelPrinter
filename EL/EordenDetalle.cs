using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL
{
    public class EordenDetalle
    {
        public int pzas { get; set; }
        public int oc { get; set; }
        public string tienda1 { get; set; }
        public string tienda2 { get; set; }
        public int sku { get; set; }
        public long ean { get; set; }
        public string estilo { get; set; }
        public string descrip { get; set; }
        public string cod_color { get; set; }
        public string desc_corta { get; set; }
        public string talla_corta { get; set; }
        public double costo { get; set; }
        public double precio { get; set; }
        public int division { get; set; }
        public long no_proveedor { get; set; }
    }
}
