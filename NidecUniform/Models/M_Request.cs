using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NidecUniform.Models;
[Table("M_Request")]

public partial class M_Request
{
    [Key]
    public int ID { get; set; }

    [MaxLength(20)]
    public string? EmployeeID { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    [StringLength(50)]
    public string? Status { get; set; } = "Pending";

    public string RequestType { get; set; } = null!;

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("EmployeeID")]
    public virtual M_Employee? Employee { get; set; }

    public virtual ICollection<RequestDetail> RequestDetails { get; set; } = new List<RequestDetail>();

    public virtual M_Delivery Delivery { get; set; } = null!;

    public virtual ICollection<DeliveryDetail> DeliveryDetails { get; set; } = new List<DeliveryDetail>();
}
