using System;
using RazorStore.Model;

namespace RazorStore.Services
{
	public class SearchAlgorithm : ISearchAlgorithm<Goods>
	{
        private AppDbContext Context { get; }

        public SearchAlgorithm(AppDbContext context)
		{
            Context = context;
        }
        public IEnumerable<Goods> Search(string searchWords)
        {
            var goods = Context.Goods;
            List<Goods> foundGoods = new();
            foreach (var item in goods)
            {
                var distance = Compute(item.Name, searchWords);

                if (distance < 3)
                {
                    foundGoods.Add(item);
                }
            }
            return foundGoods;
        }

        public int Compute(string s1, string s2)
        {
            int lenS1 = s1.Length;
            int lenS2 = s2.Length;

            int[,] dp = new int[lenS1 + 1, lenS2 + 1];

            // Инициализация базовых случаев
            for (int i = 0; i <= lenS1; i++)
                dp[i, 0] = i;

            for (int j = 0; j <= lenS2; j++)
                dp[0, j] = j;

            // Заполнение массива dp
            for (int i = 1; i <= lenS1; i++)
            {
                for (int j = 1; j <= lenS2; j++)
                {
                    int cost = (s1[i - 1] == s2[j - 1]) ? 0 : 1;
                    dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1), dp[i - 1, j - 1] + cost);
                }
            }

            // Результат - расстояние между строками
            return dp[lenS1, lenS2];
        }
    }
}


