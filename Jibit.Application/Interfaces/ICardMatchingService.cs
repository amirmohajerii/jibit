using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jibit.Application.Interfaces
{
    public interface ICardMatchingService
    {
        Task<bool> MatchCardWithNationalCodeAsync(string cardNumber, string nationalCode, DateTime birthDate);
    }
}