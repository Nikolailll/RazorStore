using System;
namespace RazorStore.Model
{
	public class PathItem
	{
		public int Id { get; set; }
		public string Path { get; set; }
		public Goods? Good { get; set; }	
	}
}

