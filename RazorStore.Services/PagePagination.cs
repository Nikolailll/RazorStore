using System;
using RazorStore.Model;

namespace RazorStore.Services
{
	public class PagePagination<T>
	{       
        public PagePagination(IEnumerable<T> goods, int pageSize, int pageNumber)
		{
            if(goods == null)
            {
                throw new ArgumentException("Goods cannot be null", nameof(goods));
            }
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalPage = (int)Math.Ceiling(goods.Count() / (double)pageSize);
			Goods = goods.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        }

		public IEnumerable<T> Goods { get; private set; }
        public int PageSize { get; }
        public int PageNumber { get; set; }
        public int TotalPage { get; private set; }

        public bool NextPage
        {
            get
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

