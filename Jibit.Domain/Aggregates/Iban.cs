
namespace Jibit.Domain.Aggregates
{
    public class Iban
    {
        public string Value { get; set; }
        public string Bank { get; set; }
        public string DepositNumber { get; set; }
        public string Status { get; set; }
    }
}
    public class IbanRequest
    {
        public string Iban { get; set; }
    }
