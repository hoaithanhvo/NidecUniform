using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Models
{
    [Table("RawData")]
    public class RawData
    {
        [NotMapped]
        public int No { get;set; }
        public int ID { get; set; }
        public string? EmployeeID { get; set; }
        public string? FullName { get; set; }
        public string? Dept { get; set; }
        public string? Gender { get; set; }
        public string? UniformType { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public int NumberOfPaint {  get; set; }
        public string? PaintType { get; set; }

        public int NumberOfshirts { get; set; }
        public string? ShirtsType { get; set; }

        public int NumberOfCones { get; set; }
        public string? ConesType { get; set; }

        public int NumberOfShoes { get; set; }
        public string? ShoesType { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

    }
}
