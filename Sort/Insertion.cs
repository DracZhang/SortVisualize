using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sort
{
    public class InsertionSort : SortBase
    {
        public InsertionSort(List<int> SortList) : base(SortList) { }

        public override void Run()
        {
            for (int i = 0; i < TotalCount; i++)
            {
                int MinIndex = 0;
                int MinValue = int.MaxValue;

                for (int j = i; j < TotalCount; j++)
                {
                    if (SortList[j] < MinValue)
                    {
                        MinValue = SortList[j];
                        MinIndex = j;
                    }
                }

                Swap(i, MinIndex, SortList[i], MinValue);
            }

            base.Run();
        }
    }
}
