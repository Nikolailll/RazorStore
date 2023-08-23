using System;
using RazorStore.Model;

namespace RazorStore.Services
{
	public class PagePagination<T>
	{
		public PagePagination(IEnumerable<T> goods, Pagination pagination)
		{
            Goods = goods;
            Pagination = pagination;
        }
		public IEnumerable<T> Goods { get; set; }
		public Pagination Pagination { get; set; }
	}

	public class Pagination
	{
		public int PageNumber { get; private set; }
		public int TotalPage { get; private set; }

		public Pagination(int pageSize, int pageNumber, int count)
		{
			this.PageNumber = pageNumber;
			TotalPage = (int)Math.Ceiling(count / (double)pageSize);
		}

		public bool NextPage
		{ get
			{
				return PageNumber < TotalPage;
			}
		}
		public bool PreviosPage
		{
			get
			{
				return PageNumber > 1;
			}
		}
	}
}

