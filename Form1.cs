using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sort
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const int BarWidth = 2;
        private const int OffsetY = 39;
        private const int OffsetX = 16;
        private Dictionary<int, Panel> Bars;
        private List<int> SortList;
        private int TotalCount;

        private Thread th;

        private void Form1_Load(object sender, EventArgs e)
        {
            int TotalWidth = this.Width;
            int TotalHeight = this.Height - OffsetY;

            TotalCount = TotalWidth / BarWidth - OffsetX / BarWidth;
            int Ratio = TotalHeight / TotalCount;
            List<int> Nums = new List<int>();

            for (int i = 0; i < TotalCount; i++)
            {
                Nums.Add(i * Ratio);
            }

            SortList = new List<int>();

            Bars = new Dictionary<int, Panel>();
            Random ran = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < TotalCount; i++)
            {
                int Index = ran.Next(Nums.Count);
                int pHeight = Nums[Index];

                SortList.Add(pHeight);
                Nums.Remove(pHeight);

                Panel p = new Panel();
                p.Location = new Point(i * BarWidth, TotalHeight - pHeight);
                p.Size = new Size(BarWidth, pHeight);
                p.BackColor = Color.Green;

                this.Controls.Add(p);
                Bars.Add(i, p);
            }

            th = new Thread(Sort);
            th.Start();
        }

        private void Sort()
        {
            ShellsSort CS = new ShellsSort(SortList);
            CS.OnSwap += CS_OnSwap;
            CS.Run();
        }

        private void CS_OnSwap(object sender, OnSwapEventArgs e)
        {
            Panel p1 = Bars[e.ID1];
            Panel p2 = Bars[e.ID2];

            Point p1Loc = new Point(e.ID2 * BarWidth, p1.Location.Y);
            Point p2Loc = new Point(e.ID1 * BarWidth, p2.Location.Y);

            ChangeLocation(p1, p1Loc);
            ChangeLocation(p2, p2Loc);

            Bars[e.ID1] = p2;
            Bars[e.ID2] = p1;
        }

        private void ChangeLocation(Panel p, Point Loc)
        {
            if (p.InvokeRequired)
            {
                Action<Point> action = (x) => { p.Location = Loc; Application.DoEvents(); };
                p.Invoke(action, Loc);
            }
            else
            {
                p.Location = Loc;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            th.Abort();
        }
    }
}
