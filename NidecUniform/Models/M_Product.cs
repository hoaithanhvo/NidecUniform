using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NidecUniform.Models;
[Table("M_Product")]

public partial class M_Product
{
    [Key]
    [StringLength(50)]
    public string? ProductID { get; set; }

    [StringLength(100)]
    public string? ProductName { get; set; }

    public double? Price { get; set; }

    public string? Unit { get; set; }

    public virtual ICollection<RequestDetail> RequestDetails { get; set; } = new List<RequestDetail>();

    public virtual ICollection<DeliveryDetail> DeliveryDetails { get; set; } = new List<DeliveryDetail>();

}
