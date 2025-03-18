using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Models.Model
{
    public class ExportModel
    {
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string RequestType { get; set; }
        public string ProductID { get; set; }

        public string ProductVieNameseName {  get; set; }    
        public string ProductEnglishName { get; set; }

        public int QuantityDelivered { get; set; }  
        public DateTime CreateDate {  get; set; }   
    }
}
