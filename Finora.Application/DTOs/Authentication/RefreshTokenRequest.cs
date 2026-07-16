using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.DTOs.Authentication
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
