﻿using App.Data.Entity;

namespace App.Web.Mvc1.Models
{
	public class CategoryPageModel
	{
		public Category? Category { get; set; }
		public List<Post>? Posts { get; set; }
	}
}
