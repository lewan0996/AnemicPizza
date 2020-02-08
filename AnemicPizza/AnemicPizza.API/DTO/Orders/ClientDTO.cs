using AnemicPizza.Core.Models.Ordering;
using AutoMapper;

namespace AnemicPizza.API.DTO.Orders
{
    [AutoMap(typeof(Client))]
    public class ClientDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
