using System;
using System.Collections.Generic;

namespace NidecUniform.Models;

public partial class MProduct
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? Code { get; set; }

    public double? Price { get; set; }

    public string? Unit { get; set; }

    public virtual ICollection<DeliveryDetail> DeliveryDetails { get; set; } = new List<DeliveryDetail>();

    public virtual ICollection<RequestDetail> RequestDetails { get; set; } = new List<RequestDetail>();
}
