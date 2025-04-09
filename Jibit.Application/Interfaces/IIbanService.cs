using Jibit.Infra.Services;
using System.Threading.Tasks;

namespace Jibit.Application.Interfaces
{
    public interface IIbanService
    {
        Task<IbanResponse> GetIbanInfoAsync(string iban);
    }
}