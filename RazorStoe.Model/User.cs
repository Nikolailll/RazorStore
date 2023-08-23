using System;
using Microsoft.AspNetCore.Identity;

namespace RazorStore.Model
{
	public class User : IdentityUser
	{
		public ICollection<Goods> Goods { get; set; } = new List<Goods>();
	}
}

