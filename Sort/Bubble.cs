using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sort
{
    public class BubbleSort : SortBase
    {
        public BubbleSort(List<int> SortList) : base(SortList) { }

        public override void Run()
        {
            for (int i = 0; i < TotalCount; i++)
            {
                for (int j = 0; j < TotalCount - 1 - i; j++)
                {
                    int v1 = SortList[j];
                    int v2 = SortList[j + 1];

                    if (v2 < v1)
                    {
                        Swap(j, j + 1, v1, v2);
                    }
                }
            }
        }
    }
}
