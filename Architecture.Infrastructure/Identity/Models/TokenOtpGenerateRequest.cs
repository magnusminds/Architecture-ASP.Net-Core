using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Infrastructure.Identity.Models
{
    public class TokenOtpGenerateRequest
    {
        /// <summary>
        /// The phone No which is used for login
        /// </summary>
        [Required]
        [JsonProperty("phoneNo")]
        public string PhoneNo { get; set; }

        /// <summary>
        /// MobileDeviceId which is used for AspNetUsers
        /// </summary>
        [Required]
        [JsonProperty("MobileDeviceId")]
        public string MobileDeviceId { get; set; }
    }
}
