using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NidecUniform.Models;
[Table("M_Delivery")]
public partial class M_Delivery
{
    [Key]
    public int ID { get; set; }
   
    public int RequestID { get; set; }

    [MaxLength(20)]
    public string? EmployeeID { get; set; }

    public DateTime DeliveryDate { get; set; }

    public string DeliveredBy { get; set; } = null!;

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }


    [ForeignKey("RequestID")]
    public virtual M_Request? Request { get; set; } = null!;

    [ForeignKey("EmployeeID")]
    public virtual M_Employee Employee { get; set; } = null!;

    public virtual ICollection<RequestDetail> RequestDetail { get; set; } = new List<RequestDetail>();

    public virtual ICollection<DeliveryDetail> DeliveryDetail { get; set; } = new List<DeliveryDetail>();
}
