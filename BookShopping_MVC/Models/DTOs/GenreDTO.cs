namespace BookShopping_MVC.Models.DTOs
{
    public class GenreDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(55)]
        public string GenreName { get; set; }  
        
    }
}
