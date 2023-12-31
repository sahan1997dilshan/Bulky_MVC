﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
	public class Category
	{

		[Key]
		public int Id { get; set; }
		[Required]

		[MaxLength(10)]   //validation 
		[DisplayName("Name")]
		public string Name { get; set; }

		
		[DisplayName("Display Order")]
		public string DisplayOrder { get; set; }
	}
}
