using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jibit.Application.Interfaces
{
    public interface IMobileMatchingService
    {
        Task<bool> MatchNationalCodeWithMobileAsync(string nationalCode, string mobileNumber);
    }
}