using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NidecUniform.Models;
[Table("DeliveryDetail")]

public partial class DeliveryDetail
{
    public int ID { get; set; }

    public string? EmployeeID { get; set; }


    public int DeliveryID { get; set; }

    public string? ProductID { get; set; }

    public string? ProductName { get; set; }

    public int QuantityDelivered { get; set; } = 0;

    public string? Unit { get; set; }

    public DateTime CreateDate { get; set; } = DateTime.Now;


    [ForeignKey("EmployeeID")]
    public virtual M_Employee? Employee { get; set; } = null!;


    [ForeignKey("ProductID")]
    public virtual M_Product? Product { get; set; } = null!;

    [ForeignKey("DeliveryID")]
    public virtual M_Delivery? Delivery { get; set; } = null!;
}
