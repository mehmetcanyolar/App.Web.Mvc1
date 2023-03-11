using App.Data.Entity;

namespace App.Web.Mvc1.Models
{
	public class SearchModel
	{
		public string search { get; set; }
		public List<Category>? Categorys { get; set; }
	}
}
