using System.ComponentModel.DataAnnotations;

namespace App.Web.Mvc1.Models
{
	public class Post
	{
		public int Id { get; set; }


		[StringLength(50), Display(Name = " Post Name")]
		public string Name { get; set; }

		[Display(Name = "Post Description")]

		public string Description { get; set; }

		public DateTime CreateDate { get; set; }


		[StringLength(150), Display(Name = "Post Image")]
		public string? Image { get; set; }

		[Display(Name = "Category")]
		public int CategoryId { get; set; }

		[Display(Name = "Category")]
		public virtual Category? Category { get; set; }

		//polymorphisimden dolayı virtual ifadesi ekleyerek istediği gibi kullanması için ekledik
	}
}
