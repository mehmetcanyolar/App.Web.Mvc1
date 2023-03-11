using System.ComponentModel.DataAnnotations;

namespace App.Data.Entity
{
	public class Page : IEntity
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "{0} alanı boş geçilemez!"), StringLength(200, MinimumLength = 3, ErrorMessage = "Kategori için 3-200 arasında değer giriniz."), Display(Name = "Sayfa Başlığı")]
		public string Title { get; set; }
		[Required(ErrorMessage = "{0} alanı boş geçilemez!"), DataType(DataType.MultilineText), Display(Name = "Sayfa İçeriği")]
		public string Content { get; set; }
		[Display(Name = "Durum")]
		public bool IsActive { get; set; }
	}
}
