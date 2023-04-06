using System.ComponentModel.DataAnnotations;

namespace App.Data.Entity
{
	public class Post : IEntity
	{
		public int Id { get; set; }
		//public int UserId { get; set; }
		//public User User { get; set; }// CodeFirst kullanarak Post class ı ile User class ı arasında 1 e 1 ilişki kurduk
		[Required(ErrorMessage = "{0} alanı boş geçilemez!"), StringLength(200, MinimumLength = 3, ErrorMessage = "Kategori için 3-200 arasında değer giriniz."), Display(Name = "Paylaşım Başlığı")]
		public string? Title { get; set; }
		[Required(ErrorMessage = "{0} alanı boş geçilemez!"), DataType(DataType.MultilineText), Display(Name = "Paylaşım İçeriği")]
		public string? Content { get; set; }
		public virtual ICollection<PostComment>? PostComments { get; set; } // Post ile PostComment arasında 1 e çok ilişki kurduk
		public int CategoryId { get; set; }
        public Category? Category { get; set; } // CodeFirst kullanarak Post class ı ile Category class ı arasında 1 e 1 ilişki kurduk
    }
}
