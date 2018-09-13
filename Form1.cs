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
        private List<int> SortList;

        private int TotalCount;
        private int TotalWidth;
        private int TotalHeight;
        private int Ratio;

        private string SelectedAlgorithm;

        private Thread th;

        private void Form1_Load(object sender, EventArgs e)
        {
            BuildCheckBox();

            TotalWidth = this.Width;
            TotalHeight = this.Height - OffsetY;

            TotalCount = TotalWidth / BarWidth - OffsetX / BarWidth;
            Ratio = TotalHeight / TotalCount;
        }

        private void BuildCheckBox()
        {
            cbSortSelect.Items.Add("Bubble Sort");
            cbSortSelect.Items.Add("Cocktail Sort");
            cbSortSelect.Items.Add("Insertion Sort");
            cbSortSelect.Items.Add("Selection Sort");
            cbSortSelect.Items.Add("Shell's Sort");
            cbSortSelect.Items.Add("Merge Sort");

            cbSortSelect.SelectedIndex = 5;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (th != null)
            {
                th.Abort();
            }
            StopSorting();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Start")
            {
                List<int> Nums = new List<int>();

                for (int i = 0; i < TotalCount; i++)
                {
                    Nums.Add(i * Ratio);
                }

                SortList = new List<int>();

                if (Bars != null)
                {
                    foreach (Panel p in Bars.Values)
                    {
                        p.Dispose();
                    }

                    Bars.Clear();
                }

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

                SetButtonText("Stop");

                SelectedAlgorithm = cbSortSelect.SelectedItem.ToString();

                th = new Thread(Sort);
                th.Start();
            }
            else if (button1.Text == "Stop")
            {
                if (th != null)
                {
                    th.Abort();
                }

                SetButtonText("Start");
                StopSorting();
            }
        }

        private void SetButtonText(string Text)
        {
            if (button1.InvokeRequired)
            {
                Action<string> action = (x) => { button1.Text = Text; };
                button1.Invoke(action, Text);
            }
            else
            {
                button1.Text = Text;
            }
        }

        private SortBase SB;
        private void Sort()
        {
            switch (SelectedAlgorithm)
            {
                case "Bubble Sort":
                    SB = new BubbleSort(SortList);
                    break;
                case "Cocktail Sort":
                    SB = new CocktailSort(SortList);
                    break;
                case "Insertion Sort":
                    SB = new InsertionSort(SortList);
                    break;
                case "Selection Sort":
                    SB = new SelectionSort(SortList);
                    break;
                case "Shell's Sort":
                    SB = new ShellsSort(SortList);
                    break;
                case "Merge Sort":
                    SB = new MergeSort(SortList);
                    break;
                default:
                    break;
            }

            if (SB != null)
            {
                SB.OnSwap += SB_OnSwap;
                SB.OnFinished += SB_OnFinished;
                SB.Run();
            }
        }

        private void SB_OnFinished(object sender, EventArgs e)
        {
            SetButtonText("Start");
            StopSorting();
        }

        private void StopSorting()
        {
            if (SB != null)
            {
                SB.OnSwap -= SB_OnSwap;
                SB.OnFinished -= SB_OnFinished;
            }

            if (th != null)
            {
                th = null;
            }
        }

        private void SB_OnSwap(object sender, OnSwapEventArgs e)
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
    }
}
