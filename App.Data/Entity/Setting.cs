using System.ComponentModel.DataAnnotations;

namespace App.Data.Entity
{
	public class Setting : IEntity
	{
		public int Id { get; set; }
		//public int UserId { get; set; }
		//public User? User { get; set; }// CodeFirst kullanarak Setting class ı ile User class ı arasında 1 e 1 ilişki kurduk
		[Required(ErrorMessage = "{0} alanı boş geçilemez!"), StringLength(200), Display(Name = "İsim")]
		public string Name { get; set; }
		[Required(ErrorMessage = "{0} alanı boş geçilemez!"), StringLength(400), Display(Name = "Kurulum")]
		public string Value { get; set; }
	}
}
