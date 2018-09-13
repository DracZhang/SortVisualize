using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sort
{
    public class SelectionSort : SortBase
    {
        public SelectionSort(List<int> SortList) : base(SortList) { }

        public override void Run()
        {
            for (int i = 1; i < TotalCount; i++)
            {
                int Current = SortList[i];
                int InsertIndex = i;

                if (Current > SortList[i - 1])
                {
                    continue;
                }

                InsertIndex = 0;
                for (int j = i - 1; j >= 0; j--)
                {
                    if (SortList[j] < Current)
                    {
                        InsertIndex = j + 1;
                        break;
                    }
                }

                if (InsertIndex == i)
                {
                    continue;       //No need to insert when it is already the maximum value in sorted list.
                }

                int iInsert = SortList[i];
                for (int k = i; k > InsertIndex; k--)
                {
                    Swap(k, k - 1, SortList[k], SortList[k - 1]);
                }
            }
        }
    }
}
