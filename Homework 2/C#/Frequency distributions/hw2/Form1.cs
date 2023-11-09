using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Runtime.CompilerServices;
using System.Data;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;

namespace hw2
{
    public partial class Form1
    {
        private Dictionary<string, bool> variables;
        private Dictionary<string, string[]> entries;

        private int numCols;
        private int numRows;

        private string[] selectedVariables;
        private int[] classIntervals;

        private string selectIntervalsDefaultText = "Enter the amount of desired intervals for each quantitative variable, in order, separated by commas";

        Dictionary<string, int> jointDistribution;
        public Form1()
        {
            InitializeComponent();
        }

        private void ReadTsvFileIntoDict(string path)
        {
            // read the file
            string[] lines = File.ReadAllLines(path);

            string[] variableLabels = lines.First().Split('\t').ToArray();
            string[] isQuantitativeValues = lines[3].Split('\t').ToArray();

            numRows = lines.Length;
            numCols = variableLabels.Length;

            entries = new Dictionary<string, string[]>();
            variables = new Dictionary<string, bool>();

            for(int i = 0; i < numCols; i++)
            {
                entries[variableLabels[i]] = new string[numRows];
                variables.Add(variableLabels[i], int.TryParse(isQuantitativeValues[i], out int dummy));
            }

            string[] values;
            for (int i = 1; i < lines.Length; i++)
            {
                values = lines[i].Split("\t");
                for (int j = 0; j < values.Length; j++)
                {
                    entries[entries.ElementAt(j).Key][i-1] = values[j];
                }
            }

            values = lines[lines.Length - 1].Split('\t');
            for (int j = 0; j < values.Length; j++)
            {
                entries[entries.ElementAt(j).Key][lines.Length - 1] = values[j];
            }
        }

        private void buttonGetTsvFile_Click(object sender, EventArgs e)
        {
            //ReadTsvFileIntoMatrix("Professional Life - Sheet1.tsv");

            ReadTsvFileIntoDict("Professional Life - Sheet1.tsv");

            foreach (string variable in entries.Keys)
                comboBoxVariable.Items.Add(variable);

            comboBoxVariable.Enabled = true;
            comboBoxVariable.SelectedIndex = 0;
            buttonGetTsvFile.Enabled = false;

            textBoxIntervals.Text = "Enter the amount of desired intervals for each quantitative variable, in order, separated by commas";
        }

        private void NewPrintDistribution(Dictionary<string, int> distribution)
        {
            dataGridViewResult.Rows.Clear();
            dataGridViewResult.Columns.Clear();

            int entries = distribution.Values.Sum();
            foreach (string variable in selectedVariables)
            {
                dataGridViewResult.Columns.Add(variable, variable);
            }
            dataGridViewResult.Columns.Add("Absolute Frequency", "Absolute Frequency");
            dataGridViewResult.Columns.Add("Relative Frequency", "Relative Frequency");
            dataGridViewResult.Columns.Add("Percentage Frequency", "Percentage Frequency");  

            foreach (KeyValuePair<string, int> kvp in distribution)
            {
                decimal relativeFreq = Math.Round((decimal)kvp.Value / (decimal)entries, 4);
                decimal percentageFreq = Math.Round(relativeFreq * 100, 2);

                string[] keys = kvp.Key.Split(", ");
                string[] values = new string[keys.Length + 3];
                for(int i = 0; i < keys.Length; i++)
                {
                    values[i] = keys[i];
                }
                values[keys.Length] = $"{kvp.Value}";
                values[keys.Length + 1] = $"{relativeFreq}";
                values[keys.Length + 2] = $"{percentageFreq}%";

                dataGridViewResult.Rows.Add(values);
            }
        }

        private void buttonEval_Click(object sender, EventArgs e)
        {
            selectedVariables = textBoxSelected.Text.Split(',').Select(variable => variable.Trim()).ToArray();

            if (selectedVariables.Length == 0)
                return;

            if (textBoxIntervals.Text.Trim() != selectIntervalsDefaultText)
                classIntervals = textBoxIntervals.Text.Split(", ").Select(interval => int.Parse(interval)).ToArray();

            Dictionary<string, int> jointDistribution = NewEvalDistribution(selectedVariables, classIntervals);

            NewPrintDistribution(jointDistribution);
        }
        
        private string[][] NewInsertClassIntervals(string[][] inMatrix, int indexInValuesMatrix, string variableLabel, int k)
        {
            // exclude not parsable variables (for simplicity)
            int[] variableValues = new int[entries[variableLabel].Length];
            for (int i = 0; i < entries[variableLabel].Length; i++)
            {
                int.TryParse(entries[variableLabel][i], out variableValues[i]);
            }

            // find the bounds of the intervals
            int min = variableValues.Min();
            int max = variableValues.Max();

            // calculate the step for the intervals
            int intervalStep = (max - min) / k;

            // if the step is too small, just use the unit
            if (intervalStep == 0) intervalStep = 1;

            // this bool is to make sure that every variable gets an interval
            bool foundInterval;
            for (int i = 0; i < inMatrix[indexInValuesMatrix].Length; i++)
            {
                if (!int.TryParse(inMatrix[indexInValuesMatrix][i], out int value)) continue;

                // find the interval of the value
                foundInterval = false;
                for (int j = 1; j < k; j++)
                {
                    if (value <= min + intervalStep * j)
                    {
                        // insert the class interval
                        inMatrix[indexInValuesMatrix][i] = $"{variableLabel}[{min + intervalStep * (j - 1)} - {min + intervalStep * j}]";
                        foundInterval = true;
                        break;
                    }
                }
                // if the variable is too far ahead, place it on the last interval
                if (!foundInterval) inMatrix[indexInValuesMatrix][i] = $"{variableLabel}[{min + intervalStep * k} - {min + intervalStep * (k + 1)}]";
            }
            return inMatrix;
        }

        private Dictionary<string, int> NewEvalDistribution(string[] selectedVariables, int[] classIntervals = null)
        {
            // prepare the new variable
            jointDistribution = new Dictionary<string, int>();

            string jointValue;
            string[][] valuesMatrix;

            for(int i = 0; i < numRows; i++)
            {
                valuesMatrix = new string[selectedVariables.Length][];
                for(int j = 0; j < selectedVariables.Length; j++)
                {
                    valuesMatrix[j] = entries[selectedVariables[j]][i].Split(',').Select(s => string.IsNullOrEmpty(s) ? "(empty)" : s.Trim().ToLower()).ToArray();
                }

                // if none are specified, skip
                if (classIntervals != null)
                {
                    // check if there are quantitative variables, if there are and there is a specified class interval, operate accordingly
                    int intervalIndex = 0;
                    for (int j = 0; j < selectedVariables.Length; j++)
                    {
                        // if a variable is quantitative and the class interval is senseful, insert the class interval
                        if (variables[selectedVariables[j]] && classIntervals[intervalIndex] != 0 && classIntervals[intervalIndex] != 1)
                        {
                            valuesMatrix = NewInsertClassIntervals(valuesMatrix, j, selectedVariables[j], classIntervals[intervalIndex]);
                            intervalIndex++; // this is because intervals are not the same index as i
                        }
                        else continue;
                    }
                }

                var combinations = CartesianProduct(valuesMatrix);
                foreach (var combination in combinations)
                {
                    jointValue = String.Join(", ", combination);

                    if (!jointDistribution.ContainsKey(jointValue)) jointDistribution[jointValue] = 1;
                    else jointDistribution[jointValue]++;
                }
            }

            // order the distibution by frequency
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

        private void buttonAddVariable_Click(object sender, EventArgs e)
        {
            // make it so quantitative variables are in red unless u specify a class interval
            if (textBoxSelected.Text == "")
            {
                textBoxSelected.Text += $"{comboBoxVariable.Text}";

                buttonRemove.Enabled = true;
                buttonEval.Enabled = true;
            }
            else
            {
                string[] currentVariables = textBoxSelected.Text.Split(", ");

                if (currentVariables.Contains(comboBoxVariable.Text)) return;

                textBoxSelected.Text += $", {comboBoxVariable.Text}";
            }
        }

        private void textBoxIntervals_GotFocus(object sender, EventArgs e)
        {
            textBoxIntervals.Text = "";
            textBoxIntervals.ForeColor = Color.Black;
        }

        private void textBoxIntervals_LostFocus(object sender, EventArgs e)
        {
            if (textBoxIntervals.Text == "")
            {
                textBoxIntervals.ForeColor = Color.Gray;
                textBoxIntervals.Text = selectIntervalsDefaultText;
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (textBoxSelected.Text == "") return;

            int commaIndex = textBoxSelected.Text.LastIndexOf(", ");

            textBoxSelected.Text = textBoxSelected.Text.Substring(0, commaIndex == -1 ? 0 : commaIndex);

            if (textBoxSelected.Text == "")
            {
                buttonRemove.Enabled = false;
                buttonEval.Enabled = false;
            }
        }
    }
}