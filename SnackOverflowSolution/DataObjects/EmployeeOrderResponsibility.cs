using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace DataObjects
{
    public class EmployeeOrderResponsibility
    {
        [Key]
        public int? OrderId { get; set; }
        public int? EmployeeId { get; set; }
        public String Description { get; set; }
    }
}