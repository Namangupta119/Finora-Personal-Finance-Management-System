using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.DTOs.Authentication
{
    public class RegisterRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set;  } = string.Empty;
        public string Password { get; set;  } = string.Empty;
        public string ConfirmPassword { get; set;  } = string.Empty;
    }
}
