using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sort
{
    public class RadixSort : SortBase
    {
        public RadixSort(List<int> SortList) : base(SortList) { }

        public override void Run()
        {
            int Digits = 0;
            foreach (int Num in SortList)
            {
                int Dig = (int)Math.Log10(Num) + 1;
                if (Dig > Digits)
                {
                    Digits = Dig;
                }
            }

            for (int Dig = 0; Dig < Digits; Dig++)
            {
                int DivideCoe = (int)Math.Pow(10, Dig);
                int ModCoe = DivideCoe * 10;

                for (int i = 0; i < TotalCount; i++)
                {
                    for (int j = 0; j < TotalCount - 1; j++)
                    {
                        int CurrentDig = (SortList[j] % ModCoe) / DivideCoe;
                        int NextDig = (SortList[j + 1] % ModCoe) / DivideCoe;

                        if (CurrentDig > NextDig)
                        {
                            Swap(j, j + 1, SortList[j], SortList[j + 1]);
                        }
                    }
                }
            }

            base.Run();
        }
    }
}
