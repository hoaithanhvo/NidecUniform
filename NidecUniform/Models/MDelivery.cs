using System;
using System.Collections.Generic;

namespace NidecUniform.Models;

public partial class MDelivery
{
    public int DeliveryId { get; set; }

    public int RequestId { get; set; }

    public int EmpolyeeId { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public string DeliveredBy { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<DeliveryDetail> DeliveryDetails { get; set; } = new List<DeliveryDetail>();

    public virtual MEmployee Empolyee { get; set; } = null!;

    public virtual MRequest Request { get; set; } = null!;
}
