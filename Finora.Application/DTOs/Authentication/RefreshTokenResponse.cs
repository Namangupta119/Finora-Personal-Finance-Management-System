using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.DTOs.Authentication
{
    public class RefreshTokenResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTimeOffset ExpiresOn { get; set;  }
    }
}
