using System.ComponentModel.DataAnnotations;

namespace App.Data.Entity
{
	public class Category : IEntity
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "{0} alanı boş geçilemez!"), StringLength(100), Display(Name = "Kategori Adı")]
		public string Name { get; set; }
		[Display(Name = "Kategori Açıklaması"), StringLength(200, MinimumLength = 3, ErrorMessage = "Kategori için 3-200 arasında değer giriniz.")]
		public string? Description { get; set; }
		public virtual ICollection<Post>? Posts { get; set; } // Category ile Post arasında 1 e çok ilişki kurduk
    }
}
