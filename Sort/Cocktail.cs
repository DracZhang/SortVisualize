using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sort
{
    public class CocktailSort : SortBase
    {
        public CocktailSort(List<int> SortList) : base(SortList) { }

        public override void Run()
        {
            int Left = 0;
            int Right = TotalCount - 1;

            while (Left < Right)
            {
                for (int i = Left; i < Right; i++)
                {
                    int v1 = SortList[i];
                    int v2 = SortList[i + 1];

                    if (v2 < v1)
                    {
                        Swap(i, i + 1, v1, v2);
                    }
                }

                Right--;

                for (int i = Right; i > Left; i--)
                {
                    int v1 = SortList[i];
                    int v2 = SortList[i - 1];

                    if (v2 > v1)
                    {
                        Swap(i, i - 1, v1, v2);
                    }
                }

                Left++;
            }

            base.Run();
        }
    }
}
