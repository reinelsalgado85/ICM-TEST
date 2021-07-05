using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace my_icm.ViewModel
{
    public class OrderVM
    {
        public string from_country { get; set; }
        public string from_zip { get; set; }
        public string from_state { get; set; }
        public string from_city { get; set; }
        public string from_street { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string to_country { get; set; }
        public string to_zip { get; set; }
        public string to_state { get; set; }
        public string to_city { get; set; }
        public string to_street { get; set; }
        public decimal? amount { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public decimal shipping { get; set; }
        public string customer_id { get; set; }
        public string exemption_type { get; set; }

        public List<LineItemVM> line_items { get; set; }
        public List<NexusAddressVM> nexus_addresses { get; set; }

        public OrderVM()
        {
            line_items = new List<LineItemVM>();
            nexus_addresses = new List<NexusAddressVM>();
        }

        public IEnumerable<string> ValidationErrors()
        {
            var errors = new List<string>();
            if (!amount.HasValue && line_items.Count == 0)
                errors.Add("Either Amount or LineItems are required to calculate taxes");
            if (to_country == "US" && string.IsNullOrWhiteSpace(to_zip))
                errors.Add("ZipCode required when shipping to US");
            if ((to_country == "US" || to_country == "CA") && string.IsNullOrWhiteSpace(to_state))
                errors.Add("State is required when shipping to US or CA");
            if (nexus_addresses.Count == 0 && (string.IsNullOrWhiteSpace(from_country)))
                errors.Add("Either NexusAddress or From address information is required for tax calculation");
            if (nexus_addresses.Any(na => string.IsNullOrWhiteSpace(na.country) || string.IsNullOrWhiteSpace(na.state)))
                errors.Add("Nexus address requires Country and State code");
            return errors;
        }
    }
}
