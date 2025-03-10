using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NidecUniform.Models;

[Table("M_Employee")]
public partial class M_Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)] // Không tự động tăng
    public string EmployeeID { get; set; } = null!;


    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = null!;

    public string? Department { get; set; }

    public string? Position { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public virtual ICollection<M_Request> Requests { get; set; } = new List<M_Request>();
    
    public virtual ICollection<M_Delivery> DeliveryDetails { get; set; } = new List<M_Delivery>();

}
