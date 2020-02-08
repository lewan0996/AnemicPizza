using System.ComponentModel.DataAnnotations;

namespace AnemicPizza.API.DTO.Basket
{
    public class CheckoutDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required]
        public short ZipCode { get; set; }
    }
}
