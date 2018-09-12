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

        private const int BarWidth = 4;
        private const int OffsetY = 39;
        private const int OffsetX = 16;
        private Dictionary<int, Panel> Bars;
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

            Bars = new Dictionary<int, Panel>();
            Random ran = new Random(1000);

            for (int i = 0; i < TotalCount; i++)
            {
                int Index = ran.Next(Nums.Count);
                int pHeight = Nums[Index];
                Nums.Remove(pHeight);

                Panel p = new Panel();
                p.Location = new Point(i * BarWidth, TotalHeight - pHeight);
                p.Size = new Size(BarWidth, pHeight);
                p.BackColor = Color.Green;

                this.Controls.Add(p);
                Bars.Add(i, p);
            }

            th = new Thread(CocktailSort);
            th.Start();
        }

        #region Cocktail sort
        private void CocktailSort()
        {
            int Left = 0;
            int Right = TotalCount - 1;

            while (Left < Right)
            {
                for (int i = Left; i < Right; i++)
                {
                    Panel p1 = Bars[i];
                    Panel p2 = Bars[i + 1];

                    if (p2.Height < p1.Height)
                    {
                        Point p1Loc = new Point((i + 1) * BarWidth, p1.Location.Y);
                        Point p2Loc = new Point(i * BarWidth, p2.Location.Y);

                        ChangeLocation(p1, p1Loc);
                        ChangeLocation(p2, p2Loc);

                        Bars[i] = p2;
                        Bars[i + 1] = p1;
                    }
                }

                Right--;

                for (int i = Right; i > Left; i--)
                {
                    Panel p1 = Bars[i];
                    Panel p2 = Bars[i - 1];

                    if (p2.Height > p1.Height)
                    {
                        Point p1Loc = new Point((i - 1) * BarWidth, p1.Location.Y);
                        Point p2Loc = new Point(i * BarWidth, p2.Location.Y);

                        ChangeLocation(p1, p1Loc);
                        ChangeLocation(p2, p2Loc);

                        Bars[i] = p2;
                        Bars[i - 1] = p1;
                    }
                }

                Left++;
            }
        }
        #endregion

        #region Bubble Sort
        private void BubbleSort()
        {
            for (int i = 0; i < TotalCount; i++)
            {
                for (int j = 0; j < TotalCount - 1 - i; j++)
                {
                    Panel p1 = Bars[j];
                    Panel p2 = Bars[j + 1];

                    if (p2.Height < p1.Height)
                    {
                        Point p1Loc = new Point((j + 1) * BarWidth, p1.Location.Y);
                        Point p2Loc = new Point(j * BarWidth, p2.Location.Y);

                        ChangeLocation(p1, p1Loc);
                        ChangeLocation(p2, p2Loc);

                        Bars[j] = p2;
                        Bars[j + 1] = p1;
                    }
                }
            }
        }
        #endregion

        #region Selectin Sort
        private void SelectionSort()
        {
            for (int i = 0; i < TotalCount; i++)
            {
                int MinIndex = 0;
                int MinValue = int.MaxValue;

                for (int j = i; j < TotalCount; j++)
                {
                    if (Bars[j].Height < MinValue)
                    {
                        MinValue = Bars[j].Height;
                        MinIndex = j;
                    }
                }

                Panel p1 = Bars[MinIndex];
                Panel p2 = Bars[i];

                Point p1Loc = new Point(i * BarWidth, p1.Location.Y);
                Point p2Loc = new Point(MinIndex * BarWidth, p2.Location.Y);

                ChangeLocation(p1, p1Loc);
                ChangeLocation(p2, p2Loc);

                Bars[i] = p1;
                Bars[MinIndex] = p2;
            }
        }
        #endregion

        #region Insertion Sort
        private void InsertionSort()
        {
            for (int i = 1; i < TotalCount; i++)
            {
                int Current = Bars[i].Height;
                int InsertIndex = i;

                if (Current > Bars[i - 1].Height)
                {
                    continue;
                }

                InsertIndex = 0;
                for (int j = i - 1; j >= 0; j--)
                {
                    if (Bars[j].Height < Current)
                    {
                        InsertIndex = j + 1;
                        break;
                    }
                }

                if (InsertIndex == i)
                {
                    continue;       //No need to insert when it is already the maximum value in sorted list.
                }

                Panel pInsert = Bars[i];
                for (int k = i; k > InsertIndex; k--)
                {
                    Bars[k] = Bars[k - 1];
                    Point p1Loc = new Point(k * BarWidth, Bars[k - 1].Location.Y);

                    ChangeLocation(Bars[k], p1Loc);
                }

                Bars[InsertIndex] = pInsert;
                ChangeLocation(Bars[InsertIndex], new Point(InsertIndex * BarWidth, pInsert.Location.Y));
            }
        }
        #endregion

        private void ChangeLocation(Panel p, Point Loc)
        {
            if (p.InvokeRequired)
            {
                Action<Point> action = (x) => { p.Location = Loc; };
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
