using System;
using RazorStore.Model.Enum;

namespace RazorStore.Services
{
	public interface ISearchAlgorithm<T>
	{
		public IEnumerable<T> Search(string s1, IEnumerable<T> s2);
		public int Compute(string s1, string s2);

	}
}

