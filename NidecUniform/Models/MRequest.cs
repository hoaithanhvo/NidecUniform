using System;
using System.Collections.Generic;

namespace NidecUniform.Models;

public partial class MRequest
{
    public int RequestId { get; set; }

    public int EmpolyeeId { get; set; }

    public DateTime? RequestDate { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual MEmployee Empolyee { get; set; } = null!;

    public virtual ICollection<MDelivery> MDeliveries { get; set; } = new List<MDelivery>();

    public virtual ICollection<RequestDetail> RequestDetails { get; set; } = new List<RequestDetail>();
}
