using System;
using System.Collections.Generic;

namespace NidecUniform.Models;

public partial class MEmployee
{
    public int EmpolyeeId { get; set; }

    public string? Department { get; set; }

    public string? Section { get; set; }

    public string? FullName { get; set; }

    public virtual ICollection<MDelivery> MDeliveries { get; set; } = new List<MDelivery>();

    public virtual ICollection<MRequest> MRequests { get; set; } = new List<MRequest>();
}
