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
            TotalPage = CalculationTotalPage(goods, pageSize);
            PageNumber = pageNumber > TotalPage ? TotalPage  : pageNumber;
            Goods = CalculationTotalGoods(goods, PageNumber, pageSize);
            MaxVisiblePages = 4;
            SetStartPage(pageNumber, MaxVisiblePages, TotalPage);

        }
        private static int CalculationTotalPage(IEnumerable<T> goods, int pageSize) =>
            (int)Math.Ceiling(goods.Count() / (double)pageSize);


        private static IEnumerable<T> CalculationTotalGoods(IEnumerable<T> goods, int pageNumber, int pageSize) =>
            goods.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        private void SetStartPage(int pageNumber, int maxVisiblePage, int totalPage)
        {
            StartPage = pageNumber - maxVisiblePage / 2;
            EndPage = pageNumber + maxVisiblePage / 2;
            if(StartPage < 1)
            {
                StartPage = 1;
                EndPage = Math.Min(totalPage, maxVisiblePage);
            }
            if(EndPage > totalPage)
            {
                EndPage = totalPage;
                StartPage = Math.Max(1, totalPage - maxVisiblePage + 1);
            }
        }

        public IEnumerable<T> Goods { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPage { get; private set; }
        public int PageNumber { get; private set; }
        public int MaxVisiblePages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

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

