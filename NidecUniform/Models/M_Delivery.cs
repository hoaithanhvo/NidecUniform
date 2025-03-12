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

    public DateTime? DeliveryDate { get; set; } = DateTime.Now;

    public string DeliveredBy { get; set; } = "NCC"!;

    //public DateTime? CreatedAt { get; set; } = DateTime.Now;

    //public DateTime? UpdatedAt { get; set; }

    [StringLength(50)]
    public string Status
    {
        get
        {
            if (DeliveryDetail == null || !DeliveryDetail.Any())
                return "Chưa giao";

            var requestDetails = Request?.RequestDetails ?? new List<RequestDetail>();

            bool allDelivered = true;
            bool someDelivered = false;

            foreach (var req in requestDetails)
            {
                var totalDelivered = DeliveryDetail
                    .Where(d => d.ProductID == req.ProductID)
                    .Sum(d => d.QuantityDelivered);

                if (totalDelivered < req.QuantityRequested)
                {
                    allDelivered = false;
                }
                if (totalDelivered > 0)
                {
                    someDelivered = true;
                }
            }

            if (allDelivered) return "Đã giao đủ";
            if (someDelivered) return "Thiếu";
            return "Chưa giao";
        }
    }


    [ForeignKey("RequestID")]
    public virtual M_Request? Request { get; set; } = null!;

    [ForeignKey("EmployeeID")]
    public virtual M_Employee Employee { get; set; } = null!;

    public virtual ICollection<RequestDetail> RequestDetail { get; set; } = new List<RequestDetail>();

    public virtual ICollection<DeliveryDetail> DeliveryDetail { get; set; } = new List<DeliveryDetail>();
}
