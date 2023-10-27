using System;
using System.Diagnostics.Eventing.Reader;
using System.DirectoryServices;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace hw2
{
    public partial class Variable {

        public readonly string label;
        public readonly bool isQuantitative;
        public Variable(string label, bool isQuantitative)
        {
            this.label = label;
            this.isQuantitative = isQuantitative;
        }
    }

    public partial class Form1
    {
        private string[][] matrix;
        private int numCols;
        private int numRows;

        //private Variable[] variables;
        private string[] variables;

        Dictionary<string, int> jointDistribution;
        public Form1()
        {
            InitializeComponent();
        }

        private string[][] ReadTsvFileIntoMatrix(string path)
        {
            // read the file
            string[] lines = File.ReadAllLines(path);

            // save the variables
            variables = lines[0].Split('\t');

            // TODO: recognize which variables are quantitative and set them as such

            // memorize the parameters
            numRows = lines.Length;
            numCols = variables.Length;

            // save every row inside the matrix
            matrix = new string[numRows][];
            for (int i = 1; i < numRows; i++)
            {
                string[] splits = lines[i].Split('\t');
                matrix[i - 1] = splits;
            }
            matrix[numRows-1] = lines[numRows-1].Split('\t');

            return matrix;
        }

        private void buttonGetTsvFile_Click(object sender, EventArgs e)
        {
            matrix = ReadTsvFileIntoMatrix("Professional Life - Sheet1.tsv");

            foreach (var variable in variables)
                comboBoxUnivaried.Items.Add(variable);

            buttonuUnivaried.Enabled = true;
            buttonMultivaried.Enabled = true;
            comboBoxUnivaried.Enabled = true;
            comboBoxUnivaried.SelectedIndex = 0;
        }

        private void buttonUnivaried_Click(object sender, EventArgs e)
        {
            // TODO: make it so you can select these
            string[] selectedVariable = { comboBoxUnivaried.SelectedItem.ToString() };

            var distribution = evalDistribution(selectedVariable);

            printDistribution(distribution);
        }

        private void printDistribution(Dictionary<string, int> distribution)
        {
            int entries = distribution.Values.Sum();

            textBox1.Text = "";
            decimal relativeFreq;
            decimal percentage;
            foreach (KeyValuePair<string, int> kvp in distribution)
            {
                relativeFreq = Math.Round((decimal)kvp.Value / (decimal)entries, 4);
                percentage = Math.Round(relativeFreq * 100, 2);

                textBox1.Text += $"{kvp.Key}: \t {kvp.Value} ({relativeFreq} {percentage}%)\r\n";
            }

        }

        private void buttonMultivaried_Click(object sender, EventArgs e)
        {
            string[] selectedVariables = textBoxMultivaried.Text.Split(',').Select(variable => variable.Trim()).ToArray();

            if (selectedVariables[0] == "")
                return;

            Dictionary<string, int> jointDistribution = evalDistribution(selectedVariables);

            printDistribution(jointDistribution);
        }

        // works for multivaried and univaried
        private Dictionary<string, int> evalDistribution(string[] selectedVariables)
        {
            jointDistribution = new Dictionary<string, int>();

            int[] variableIndexes = new int[selectedVariables.Length];
            for (int i = 0; i < selectedVariables.Length; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    if (variables[j] == selectedVariables[i])
                    {
                        variableIndexes[i] = j;
                        break;
                    }
                }
            }

            string[][] valuesMatrix = new string[variableIndexes.Length][];
            string jointValue;

            for (int currentRow = 0; currentRow < numRows; currentRow++)
            {
                for (int i = 0; i < variableIndexes.Length; i++)
                    valuesMatrix[i] = matrix[currentRow][variableIndexes[i]].ToLower().Trim('"').Trim(' ').Trim(',').Split(',')

                var combinations = CartesianProduct(valuesMatrix);
                foreach (var combination in combinations)
                {
                    jointValue = String.Join(", ", combination);

                    if (!jointDistribution.ContainsKey(jointValue)) jointDistribution[jointValue] = 1;
                    else jointDistribution[jointValue]++;
                }

            }

            return jointDistribution.OrderByDescending(f => f.Value).ToDictionary(f => f.Key, f => f.Value);
        }
        private static IEnumerable<string[]> CartesianProduct(string[][] items)
        {
            string[] currentItem = new string[items.Length];
            static IEnumerable<string[]> Go(string[][] items, string[] currentItem, int index)
            {
                if (index == items.Length)
                {
                    yield return (string[])currentItem.Clone();
                }
                else
                {
                    foreach (string item in items[index])
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