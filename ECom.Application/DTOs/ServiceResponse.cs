using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Application.DTOs
{
    public record ServiceResponse(bool Succes= false,string Message = null!);
}
