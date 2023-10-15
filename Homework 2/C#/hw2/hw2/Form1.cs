using System;
using System.Diagnostics.Eventing.Reader;
using System.DirectoryServices;
using System.Drawing.Drawing2D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace hw2
{
    public partial class Form1 
    {
        string[,] matrix;
        int numCols;
        int numRows;

        Dictionary<string, int> jointDistribution;
        public Form1()
        {
            InitializeComponent();
        }

        private string[,] ReadTsvFileIntoMatrix(string path)
        {
            // read the file
            string[] lines = File.ReadAllLines(path);

            int numRows = lines.Length;
            int numCols = lines[0].Split('\t').Length;

            string[,] matrix = new string[numRows, numCols];

            for (int i = 0; i < numRows; i++)
            {
                string[] splits = lines[i].Split('\t');
                for (int j = 0; j < numCols; j++)
                {
                    matrix[i, j] = splits[j];
                }
            }

            return matrix;
        }

        private Dictionary<string, int> evalUnivariateDistribution(string var)
        {
            // get the column corresponding to the variable
            int varCol = 0;
            for (int j = 0; j < numCols; j++)
            {
                if (matrix[0, j] == var)
                {
                    varCol = j;
                    break;
                }
            }

            Dictionary<string, int> distribution = new Dictionary<string, int>();

            string[] values;
            string value;
            for (int i = 1; i < numRows; i++)
            {
                values = matrix[i, varCol].ToLower().Trim('"').Trim(' ').Trim(',').Split(',');

                for (int k = 0; k < values.Length; k++)
                {
                    value = values[k].Trim(' ');
                    if (value == "")
                    {
                        if (!distribution.ContainsKey("no answer")) distribution["no answer"] = 0;

                        distribution["no answer"]++;
                        continue;
                    }

                    if (value == "-")
                    {
                        if (!distribution.ContainsKey("none")) distribution["none"] = 0;

                        distribution["none"]++;
                        continue;
                    }

                    if (!distribution.ContainsKey(value)) distribution[value] = 1;
                    else distribution[value]++;
                }
            }

            return distribution;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            matrix = ReadTsvFileIntoMatrix("Professional Life - Sheet1.tsv");

            numCols = matrix.GetLength(1);
            numRows = matrix.GetLength(0);

            for (int j = 1; j < numCols; j++)
            {
                comboBox1.Items.Add(matrix[0, j]);
            }

            button2.Enabled = true;
            button3.Enabled = true;
            comboBox1.Enabled = true;
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // TODO: make it so you can select these
            string var = comboBox1.SelectedItem.ToString();

            Dictionary<string, int> distribution = evalUnivariateDistribution(var);
            int entries = distribution.Values.Sum();

            textBox1.Text = "";
            decimal relativeFreq;
            decimal percentage;
            foreach (KeyValuePair<string, int> kvp in distribution)
            {
                relativeFreq = Math.Round((decimal)kvp.Value / (decimal)entries, 2);
                percentage = Math.Round(relativeFreq * 100, 2);

                textBox1.Text += $"{kvp.Key}: \t {kvp.Value} ({relativeFreq} {percentage}%)\r\n";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string var1 = "Background (degree)";
            string var2 = "Programming Languages";
            string var3 = "Age";

            string[] variables = { var1, var2, var3 };

            Dictionary<string, int> jointDistribution = evalMultivariateDistribution(variables);
            int entries = jointDistribution.Values.Sum();

            textBox1.Text = "";
            decimal relativeFreq;
            decimal percentage;
            foreach (KeyValuePair<string, int> kvp in jointDistribution)
            {
                relativeFreq = Math.Round((decimal)kvp.Value / (decimal)entries, 2);
                percentage = Math.Round(relativeFreq * 100, 2);

                textBox1.Text += $"{kvp.Key}: \t {kvp.Value} ({relativeFreq} {percentage}%)\r\n";
            }
        }

        private Dictionary<string, int> evalMultivariateDistribution(string[] variables)
        {
            jointDistribution = new Dictionary<string, int>();

            int[] varColumns = new int[variables.Length];
            for (int i = 0; i < variables.Length; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    if (matrix[0, j] == variables[i])
                    {
                        varColumns[i] = j;
                        break;
                    }
                }
            }

            string[][] valuesMatrix = new string[varColumns.Length][];
            string jointValue;

            for (int i = 1; i < numRows; i++)
            {
                valuesMatrix[0] = matrix[i, varColumns[0]].ToLower().Trim('"').Trim(' ').Trim(',').Split(',');
                for (int k = 1; k < varColumns.Length; k++)
                {
                    valuesMatrix[k] = matrix[i, varColumns[k]].ToLower().Trim('"').Trim(' ').Trim(',').Split(',');
                }

                var combinations = CartesianProduct(valuesMatrix);
                foreach(var combination in combinations)
                {
                    jointValue = String.Join(",", combination);

                    if (!jointDistribution.ContainsKey(jointValue)) jointDistribution[jointValue] = 1;
                    else jointDistribution[jointValue]++;
                }

            }

            return jointDistribution;
        }
        private static IEnumerable<string[]> CartesianProduct<T>(T[][] items)
        {
            string[] currentItem = new string[items.Length];
            static IEnumerable<string[]> Go(T[][] items, string[] currentItem, int index)
            {
                if (index == items.Length)
                {
                    yield return (string[])currentItem.Clone();
                }
                else
                {
                    foreach (T item in items[index])
                    {
                        currentItem[index] = item.ToString();
                        foreach (string[] j in Go(items, currentItem, index + 1))
                        {
                            yield return (string[])j.Clone();
                        }
                    }
                }
            }
            return Go(items, currentItem, 0);
        }
    }
}