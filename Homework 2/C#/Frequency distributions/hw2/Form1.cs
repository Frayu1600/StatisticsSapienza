using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Runtime.CompilerServices;
using System.Data;

namespace hw2
{
    public partial class Form1
    {
        private string[][] matrix;
        private int numCols;
        private int numRows;

        private Variable[] variables;
        private string[] selectedVariables;

        private string selectIntervalsDefaultText = "Enter the amount of desired intervals for each quantitative variable, in order, separated by commas";

        Dictionary<string, int> jointDistribution;
        public Form1()
        {
            InitializeComponent();
        }

        private string[][] ReadTsvFileIntoMatrix(string path)
        {
            // read the file
            string[] lines = File.ReadAllLines(path);

            // save the variables, except the first one (stat ID)
            string[] variableLabels = lines[0].Split('\t').ToArray();
            string[] isQuantitativeValues = lines[3].Split('\t').ToArray();
            variables = new Variable[variableLabels.Length];

            // gets the variables and if they are quantitative or not
            string label;
            bool isQuantitative;
            for (int i = 0; i < variables.Length; i++)
            {
                label = variableLabels[i];
                isQuantitative = int.TryParse(isQuantitativeValues[i], out int dummy);

                variables[i] = new Variable(label, isQuantitative);
            }

            // memorize the parameters
            numRows = lines.Length;
            numCols = variables.Length;

            // save every row inside the matrix
            matrix = new string[numRows][];
            for (int i = 1; i < numRows; i++)
            {
                matrix[i - 1] = lines[i].Split('\t');
            }
            matrix[numRows - 1] = lines[numRows - 1].Split('\t');

            return matrix;
        }

        private void buttonGetTsvFile_Click(object sender, EventArgs e)
        {
            matrix = ReadTsvFileIntoMatrix("Professional Life - Sheet1.tsv");

            foreach (var variable in variables)
                comboBoxVariable.Items.Add(variable.name);

            comboBoxVariable.Enabled = true;
            comboBoxVariable.SelectedIndex = 0;
            buttonGetTsvFile.Enabled = false;

            textBoxIntervals.Text = "Enter the amount of desired intervals for each quantitative variable, in order, separated by commas";
        }

        private void PrintDistribution(Dictionary<string, int> distribution)
        {
            int entries = distribution.Values.Sum();

            var dataTable = new DataTable();
            foreach(string variable in selectedVariables)
            {
                dataTable.Columns.Add(variable);
            }
            dataTable.Columns.Add("Absolute Frequency");
            dataTable.Columns.Add("Relative Frequency");
            dataTable.Columns.Add("Percentage Frequency");

            decimal relativeFreq;
            decimal percentageFreq;
            DataRow row;
            foreach (KeyValuePair<string, int> kvp in distribution)
            {
                relativeFreq = Math.Round((decimal)kvp.Value / (decimal)entries, 4);
                percentageFreq = Math.Round(relativeFreq * 100, 2);

                row = dataTable.NewRow();

                string[] keys = kvp.Key.Split(", ");
                for(int i = 0; i < keys.Length; i++)
                {
                    row[selectedVariables[i]] = keys[i];
                }
                row["Absolute Frequency"] = kvp.Value;
                row["Relative Frequency"] = relativeFreq;
                row["Percentage Frequency"] = $"{percentageFreq}%";

                dataTable.Rows.Add(row);
            }
            dataGridViewResult.DataSource = dataTable;
        }

        private int FindColumnByVariableName(string varName)
        {
            for (int i = 0; i < variables.Length; i++)
            {
                if (variables[i].name == varName) return i;
            }
            return -1;
        }

        private void buttonEval_Click(object sender, EventArgs e)
        {
            selectedVariables = textBoxSelected.Text.Split(',').Select(variable => variable.Trim()).ToArray();

            if (selectedVariables.Length == 0)
                return;

            Dictionary<string, int> jointDistribution = EvalDistribution(selectedVariables);

            PrintDistribution(jointDistribution);
        }

        private string[][] InsertClassIntervals(string[][] inMatrix, int col, int k)
        {
            // exclude not parsable variables (for simplicity)
            int[] columnValues = new int[inMatrix.GetLength(0)];
            for (int i = 0; i < inMatrix.GetLength(0); i++)
            {
                int.TryParse(inMatrix[i][col], out columnValues[i]);
            }

            // find the bounds of the intervals
            int min = columnValues.Min();
            int max = columnValues.Max();

            // calculate the step for the intervals
            int intervalStep = (max - min) / k;

            // if the step is too small, just use the unit
            if (intervalStep == 0) intervalStep = 1;

            // this bool is to make sure that every variable gets an interval
            bool foundInterval;
            for (int i = 0; i < inMatrix.Length; i++)
            {
                if (!int.TryParse(inMatrix[i][col], out int value)) continue;

                // find the interval of the value
                foundInterval = false;
                for (int j = 1; j < k; j++)
                {
                    if (value <= min + intervalStep * j)
                    {
                        // insert the class interval
                        inMatrix[i][col] = $"{variables[col].name}[{min + intervalStep * (j - 1)} - {min + intervalStep * j}]";
                        foundInterval = true;
                        break;
                    }
                }
                // if the variable is too far ahead, place it on the last interval
                if (!foundInterval) inMatrix[i][col] = $"{variables[col].name}[{min + intervalStep * k} - {min + intervalStep * (k + 1)}]";
            }
            return inMatrix;
        }

        // works for univaried and multivaried of k variables
        private Dictionary<string, int> EvalDistribution(string[] selectedVariables)
        {
            // prepare the new variable
            jointDistribution = new Dictionary<string, int>();

            // find every column index for every variable faster checks later on
            int[] variableColumns = new int[selectedVariables.Length];
            for (int i = 0; i < selectedVariables.Length; i++)
            {
                variableColumns[i] = FindColumnByVariableName(selectedVariables[i]);
            }

            // copy the actual matrix into a dummy one
            string[][] processedMatrix = CloneMatrix(matrix);

            // check textbox to see if the user specified any intervals or not
            int[] classIntervals = null;
            if (textBoxIntervals.Text.Trim() != selectIntervalsDefaultText)
            {
                classIntervals = textBoxIntervals.Text.Split(", ").Select(interval => int.Parse(interval)).ToArray();
            }

            // if none are specified, skip
            if (classIntervals != null)
            {
                // check if there are quantitative variables, if there are and there is a specified class interval, operate accordingly
                int intervalIndex = 0;
                for (int i = 0; i < variableColumns.Length; i++)
                {
                    // if a variable is quantitative and the class interval is senseful, insert the class interval
                    if (variables[variableColumns[i]].isQuantitative && classIntervals[intervalIndex] != 0 && classIntervals[intervalIndex] != 1)
                    {
                        processedMatrix = InsertClassIntervals(processedMatrix, variableColumns[i], classIntervals[intervalIndex]);
                        intervalIndex++; // this is because intervals are not the same index as i
                    }
                    else continue;
                }
            }

            // actually calculate the joint distribution
            string[][] valuesMatrix = new string[variableColumns.Length][];
            string jointValue;
            // for each statistical unit
            for (int currentRow = 0; currentRow < numRows; currentRow++)
            {
                for (int i = 0; i < variableColumns.Length; i++)
                    valuesMatrix[i] = processedMatrix[currentRow][variableColumns[i]].ToLower().Trim().Split(',').Select(s => string.IsNullOrEmpty(s) ? "N.A" : s.Trim()).ToArray();

                // calculate the cartesian product of the matrix
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

        // for the deep cloning of the matrix
        public static string[][]? CloneMatrix(string[][] source)
        {
            if (source == null) return default;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(stream) as string[][];
            }
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