using System.ComponentModel.DataAnnotations;

namespace App.Data.Entity
{
	public class PostComment : IEntity
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public Post? Post { get; set; }// CodeFirst kullanarak PostComment class ı ile Post class ı arasında 1 e 1 ilişki kurduk
		//public virtual ICollection<User>? Users { get; set; } // PostComment ile User arasında 1 e çok ilişki kurduk

		[DataType(DataType.MultilineText), Display(Name = "İçerik Yorumu")]
		public string? Comment { get; set; }
		public bool IsActive { get; set; }
	}
}
