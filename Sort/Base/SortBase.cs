using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort
{
    public class SortBase
    {
        public List<int> SortList { get; private set; }
        public int TotalCount { get; private set; }

        public event EventHandler<OnSwapEventArgs> OnSwap;

        public SortBase(List<int> SortList)
        {
            this.SortList = SortList;
            TotalCount = SortList.Count;
        }

        public virtual void Run(){ }

        protected void Swap(int ID1, int ID2, int Value1, int Value2)
        {
            SortList[ID1] = Value2;
            SortList[ID2] = Value1;

            OnSwapEventArgs OSEA = new OnSwapEventArgs(ID1, ID2, Value1, Value2);
            OnSwap?.Invoke(this, OSEA);
        }
    }
}
