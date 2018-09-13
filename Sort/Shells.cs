using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sort
{
    public class ShellsSort : SortBase
    {
        public ShellsSort(List<int> SortList) : base(SortList) { }

        public override void Run()
        {
            int Step = TotalCount;
            while (Step > 1)
            {
                Step = Step / 3 + 1;
                for (int j = Step; j < TotalCount; j++)
                {
                    for (int k = j; k >= Step; k -= Step)
                    {
                        if (SortList[k] < SortList[k - Step])
                        {
                            Swap(k - Step, k, SortList[k - Step], SortList[k]);
                        }
                    }
                }
            }

            base.Run();
        }
    }
}
