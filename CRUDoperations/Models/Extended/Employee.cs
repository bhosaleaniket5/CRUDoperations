using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CRUDoperations.Models.Extended
{
    [MetadataType(typeof(EmployeeMetadata))]
    public class Employee
    {
    }

    public class EmployeeMetadata
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide First Name")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide First Name")]
        public string LastName { get; set; }
    }


}   