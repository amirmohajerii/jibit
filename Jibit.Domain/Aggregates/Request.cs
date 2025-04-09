
namespace Jibit.Domain.Aggregates
{
    public class Request
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string Provider { get; set; }
        public DateTime CalledAt { get; set; }
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; } 
     
    }
}