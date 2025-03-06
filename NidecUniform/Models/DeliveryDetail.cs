using System;
using System.Collections.Generic;

namespace NidecUniform.Models;

public partial class DeliveryDetail
{
    public int DeliveryDetailId { get; set; }

    public int DeliveryId { get; set; }

    public int ProductId { get; set; }

    public int QuantityDelivered { get; set; }

    public string? Size { get; set; }

    public string? Unit { get; set; }

    public virtual MDelivery Delivery { get; set; } = null!;

    public virtual MProduct Product { get; set; } = null!;
}
