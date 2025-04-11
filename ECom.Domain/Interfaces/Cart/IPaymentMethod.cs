using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Domain.Interfaces.Cart
{
    public interface IPaymentMethod
    {
        Task<IEnumerable<IPaymentMethod>> GetPaymentMethods();
    }
}
