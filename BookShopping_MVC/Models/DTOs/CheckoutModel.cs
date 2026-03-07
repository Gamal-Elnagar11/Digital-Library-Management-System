namespace BookShopping_MVC.Models.DTOs
{
    public class CheckoutModel
    {
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }

        
        [Required]
        [MaxLength(30)]
        public string? Email { get; set; }
        

        [Required]
        [MaxLength(30)]
        public string? MobileNumber { get; set; }

        
        [Required]
        [MaxLength(30)]
        public string? Address { get; set; }


        [Required]
        [MaxLength(30)]
        public string? PaymentMethod { get; set; }



    }
}
