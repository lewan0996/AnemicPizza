using AnemicPizza.Core.Models.Ordering;
using AutoMapper;
#pragma warning disable 1591

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
