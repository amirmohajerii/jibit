
namespace Jibit.Domain.Aggregates
{


    public class CardMatchingRequest
    {
        public string CardNumber { get; set; }

        public string NationalCode { get; set; }

        public DateTime BirthDate { get; set; }
    }

}