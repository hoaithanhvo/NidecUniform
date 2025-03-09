using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NidecUniform.Models;

[Table("RequestDetail")]
public partial class RequestDetail
{
    [Key]
    public int ID { get; set; }

    public int RequestID { get; set; }

    public string? EmployeeID { get; set; }

    public string? ProductID { get; set; }
    public string? ProductName { get; set; }
    public int QuantityRequested { get; set; }
    public int? QuantityDelivered { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }
    public string? Unit { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    [ForeignKey("RequestID")]
    public virtual M_Request? Request { get; set; } = null!;
    [ForeignKey("EmployeeID")]
    public virtual M_Employee? Employee { get; set; } = null!;

    [ForeignKey("ProductID")]
    public virtual M_Product? Product { get; set; } = null!;

    public virtual ICollection<DeliveryDetail> DeliveryDetails { get; set; } = new List<DeliveryDetail>();

}
