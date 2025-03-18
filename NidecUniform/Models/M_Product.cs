using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Models
{
    public class M_Product
    {
        [Key]
        [StringLength(50)]
        public string? ProductID { get; set; }

        [StringLength(50)]
        public string? ProductEnglishName { get; set; }

        [StringLength(50)]
        public string? ProductVietnameseName { get; set; }

        [StringLength(10)]
        public string? Currency { get; set; }

        public double? Price { get; set; }

        public string? Unit { get; set; }

        public virtual ICollection<RequestDetail> RequestDetails { get; set; } = new List<RequestDetail>();

        public virtual ICollection<DeliveryDetail> DeliveryDetails { get; set; } = new List<DeliveryDetail>();

    }
}
