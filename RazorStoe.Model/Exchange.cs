using System;
namespace RazorStore.Model
{
	public class Exchange
	{
		public string Time { get; set; }
		public string Asset_Id_Base { get; set; }
		public string Asset_Id_Quote { get; set; }
		public string Rate { get; set; }
	}
}

