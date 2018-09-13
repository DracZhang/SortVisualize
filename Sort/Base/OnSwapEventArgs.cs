using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort
{
    public class OnSwapEventArgs : EventArgs
    {
        public int ID1;
        public int ID2;
        public int Value1;
        public int Value2;

        public OnSwapEventArgs(int ID1, int ID2, int Value1, int Value2)
        {
            this.ID1 = ID1;
            this.ID2 = ID2;
            this.Value1 = Value1;
            this.Value2 = Value2;
        }
    }
}
