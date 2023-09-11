using System;
namespace RazorStore.Services
{
	public interface ISearchAlgorithm<T>
	{
		public IEnumerable<T> Search(string s);
		public int Compute(string s1, string s2);

	}
}

