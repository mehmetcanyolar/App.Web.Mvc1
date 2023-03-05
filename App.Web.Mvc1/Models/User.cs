using System.ComponentModel.DataAnnotations;

namespace App.Web.Mvc1.Models
{
	public class User
	{
		public int Id { get; set; }

		[Display(Name="Name"), StringLength(50)]
		public string Name { get; set; }

		[Display(Name = "Surname"),StringLength(50)]

		public string Surname { get; set; }

		[Display(Name = "Email"),StringLength(100)]

		public string Email { get; set; }

		[Display(Name = "Password"), StringLength(100)]
		public string Password { get; set; }

		[Display(Name= "Create Date")]
		public DateTime CreateDate { get; set; }
		[Display(Name="Status")]
		public bool IsActive { get; set; } //kullanıcıyı aktif pasif yapmak için

		[Display(Name = "Admin")]
		public bool IsAdmin { get; set; } //kullanıcının admin paneline erişimi için
	}
}
