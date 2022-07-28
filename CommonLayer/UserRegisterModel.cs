using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class UserRegisterModel
    {
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public long MobileNumber { get; set; }
    }
}
