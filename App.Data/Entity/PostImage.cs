using System.ComponentModel.DataAnnotations;

namespace App.Data.Entity
{
	public class PostImage : IEntity
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public Post? Post { get; set; }// CodeFirst kullanarak PostImage class ı ile Post class ı arasında 1 e 1 ilişki kurduk
		[Required(ErrorMessage = "{0} alanı boş geçilemez!"), StringLength(200), Display(Name = "Resim Adı")]
		public string ImagePath { get; set; }
	}
}
