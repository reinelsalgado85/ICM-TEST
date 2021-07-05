using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_icm.ViewModel
{
    public class LineItemVM
    {

        public string id { get; set; }
        public int quantity { get; set; }
        public string product_tax_code { get; set; }
        public decimal? unit_price { get; set; }
        public decimal? discount { get; set; }

    }
}
