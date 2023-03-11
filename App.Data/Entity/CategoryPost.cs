namespace App.Data.Entity
{
	public class CategoryPost : IEntity
	{
		public int Id { get; set; }
		public int CategoryId { get; set; }
		public Category? Category { get; set; }// CodeFirst kullanarak CategoryPost class ı ile Category class ı arasında 1 e 1 ilişki kurduk
		public int PostId { get; set; }
		public Post? Post { get; set; }// CodeFirst kullanarak CategoryPost class ı ile Post class ı arasında 1 e 1 ilişki kurduk

	}
}
