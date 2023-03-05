using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace App.Web.Mvc1.Models
{
	public class Category
	{
		public int Id { get; set; }

		[StringLength(50), Display(Name = "Category Name")]
		public string Name { get; set; }

		[Display(Name = "Category Description")]
		public string? Description { get; set; }

		[Display(Name = "Creation Date")]
		public DateTime CreateDate { get; set; }


		public virtual List<Post>? Posts { get; set; } //Category.Posts yaparakistediğimiz içeriği gidebilmek için




	}
}
