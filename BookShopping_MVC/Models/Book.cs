
namespace BookShopping_MVC.Models
{
    [Table("Book")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string? BookName { get; set; }
        

        [Required]
        [StringLength(40)]
        public string? AuthorName { get; set; }

        [Required]
        public double Price { get; set; }  

        public string? Image { get; set; }

        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public Stock Stock { get; set; }

        public List<OrderDetail> orderDetail { get; set; }
        public List<CartDetail> cartDetail { get; set; }


        [NotMapped]
        public string GenerName { get; set; }


        public int Quantity { get; set; }


    }
}
