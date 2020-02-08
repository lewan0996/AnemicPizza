using AnemicPizza.Core.Models.Ordering;
using AutoMapper;

namespace AnemicPizza.API.DTO.Orders
{
    [AutoMap(typeof(Address))]
    public class AddressDTO
    {
        public string City { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }
    }
}
