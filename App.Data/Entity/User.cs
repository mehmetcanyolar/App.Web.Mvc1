using System.ComponentModel.DataAnnotations;

namespace App.Data.Entity
{
	public class User : IEntity
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "{0} alanı boş geçilemez!"), StringLength(200), EmailAddress, Display(Name = "Email")]
		public string Email { get; set; }
		[Required(ErrorMessage = "{0} alanı boş geçilemez!"), StringLength(100), Display(Name = "Şifre"), DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "{0} alanı boş geçilemez!"), StringLength(100), Display(Name = "Ad")]
		public string Name { get; set; }
		[Display(Name = "Şehir"), StringLength(100)]
		public string? City { get; set; }
		[Display(Name = "Telefon"), StringLength(20)]
		public string? Phone { get; set; }
		public virtual ICollection<Post>? Posts { get; set; } // User ile Post arasında 1 e çok ilişki kurduk
		public virtual ICollection<Setting>? Settings { get; set; } // User ile Setting arasında 1 e çok ilişki kurduk

	}
}
