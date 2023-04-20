using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Entities.Model
{
    [Table("OTPLogin")]
    public class OTPLogin
    {

        [Key]
        public long Id { get; set; }

        public string PhoneNumber { get; set; }
        public string OTP { get; set; }
        public DateTimeOffset ExpiryTime { get; set; }
    }
}
