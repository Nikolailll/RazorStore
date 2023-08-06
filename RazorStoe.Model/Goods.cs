using System;
using RazorStore.Model.Enum;

namespace RazorStore.Model
{
	public class Goods
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string PicturePath { get; set; }
		public int Price { get; set; }
		public string Subscribe { get; set; }
		public TypeOfGoods Type { get; set; }
		public string Location { get; set; }
		public bool Delete { get; set; } = false;


	}
}

