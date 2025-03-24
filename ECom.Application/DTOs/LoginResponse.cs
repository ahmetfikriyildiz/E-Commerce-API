using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Application.DTOs
{
     public record LoginResponse(bool Success = false, string Token = null!, string RefreshToken = null!, string Message = null!);
}
