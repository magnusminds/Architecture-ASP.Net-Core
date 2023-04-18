using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Architecture.Entities
{
    public class ApplicationUser : IdentityUser
    {
       

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public long Id { get; set; }

        [IgnoreDataMember]
        [DisplayName("User Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }


        public string? MobileDeviceId { get; set; }

    }
}
