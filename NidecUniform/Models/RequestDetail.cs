using System;
using System.Collections.Generic;

namespace NidecUniform.Models;

public partial class RequestDetail
{
    public int DetailId { get; set; }

    public int RequestId { get; set; }

    public int ProductId { get; set; }

    public int QuantityRequested { get; set; }

    public int? QuantityApproved { get; set; }

    public int? QuantityDelivered { get; set; }

    public string? Size { get; set; }

    public string? Unit { get; set; }

    public virtual MProduct Product { get; set; } = null!;

    public virtual MRequest Request { get; set; } = null!;
}
