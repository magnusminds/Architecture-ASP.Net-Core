using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace Architecture.Entities.Model
{
    public class ApplicationUser : IdentityUser
    {


        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public int UserId { get; set; }



        [IgnoreDataMember]
        [DisplayName("User Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }


        public string MobileDeviceId { get; set; }

        public string RegisteredFCMToken { get; set; }

    }
}
