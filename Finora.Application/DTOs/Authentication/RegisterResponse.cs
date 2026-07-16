using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.DTOs.Authentication
{
    public class RegisterResponse
    {
        public Guid UserId { get; set; }
        public string Email { get; set;  } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
