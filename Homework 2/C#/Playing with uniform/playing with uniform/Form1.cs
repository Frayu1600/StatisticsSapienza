namespace playing_with_uniform
{
    public partial class Form1 : Form
    {
        double[] values;
        int[] intervals;
        int N;
        int k;
        public Form1()
        {
            InitializeComponent();
        }
        private void buttonCalc_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxN.Text, out N) || !int.TryParse(textBoxK.Text, out k)) return;

            values = new double[N];

            Random r = new Random();
            for (int i = 0; i < N; i++)
            {
                values[i] = r.NextDouble();
            }

            intervals = new int[k];
            for (int i = 0; i < k; ++i)
            {
                intervals[i] = 0;
            }

            for (int i = 0; i < N; i++)
            {
                for (int j = 1; j <= k; j++)
                {
                    if (values[i] <= (double)j/(double)k)
                    {
                        intervals[j - 1]++;
                        break;
                    }
                    else continue;
                }
            }

            double perc;
            textBoxResult.Text = "";
            for (int i = 0; i < k; ++i)
            {
                perc = Math.Round(((double)intervals[i]/(double)N) * 100, 4);
                textBoxResult.Text += $"[{i}/{k}, {i + 1}/{k}) \t {intervals[i].ToString()} ({perc}%) \r\n";
            }
        }
    }

}