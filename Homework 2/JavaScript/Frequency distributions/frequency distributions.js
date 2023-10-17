var buttonEvalDistibution;
var variableInput;
var fileInput;
var matrix = [];
var numCols;
var numRows;

var variables = [];

document.addEventListener('DOMContentLoaded', function() {

    buttonEvalDistibution = document.getElementById('evalDistribution');
    variableInput = document.getElementById('variableInput');
    fileInput = document.getElementById('fileInput');

    fileInput.addEventListener('change', function(e) {
        const file = e.target.files[0];
        const reader = new FileReader();

        reader.onload = function(e) {
          const contents = e.target.result;
          const rows = contents.split('\n');
  
          rows.forEach(function(row) {
            const columns = row.split('\t'); 
            matrix.push(columns);
          });
        };
  
        reader.readAsText(file);
      });

      buttonEvalDistibution.addEventListener('click', function(e) {
            variables = matrix[0];

            inputs = variableInput.value.split(',');

            numCols = matrix[0].length;
            numRows = matrix.length;

            const jointDistribution = evalDistribution(inputs);

            console.table(jointDistribution);
      });
});

function evalDistribution(variables) {
    const jointDistribution = {};
  
    const varColumns = [];
    for (let i = 0; i < variables.length; i++) 
    {
      for (let j = 0; j < numCols; j++) 
      {
        if (matrix[0][j].trim().toLowerCase() == variables[i].trim().toLowerCase()) 
        {
          varColumns[i] = j;
          break;
        }
      }
    }
  
    var valuesMatrix = [];
    let jointValue;
  
    for (let i = 1; i < numRows; i++) 
    {
        valuesMatrix[0] = matrix[i][varColumns[0]].toLowerCase().trim('"').trim(' ').trim(',').split(',');
        for (let k = 1; k < varColumns.length; k++) 
            valuesMatrix[k] = matrix[i][varColumns[k]].toLowerCase().trim('"').trim(' ').trim(',').split(',');
  
      const combinations = cartesianProduct(valuesMatrix);

      combinations.forEach((combination) => {
        jointValue = combination.join(', ');
  
        if (!jointDistribution[jointValue]) jointDistribution[jointValue] = 1;
        else jointDistribution[jointValue]++;
      });
    }
  
    return Object.fromEntries(Object.entries(jointDistribution).sort((a,b) => b[1] - a[1]));
  }
  
  function cartesianProduct(items) {
    const currentItem = new Array(items.length);
    const result = [];

    function go(index) {
        if (index === items.length) {
            result.push([...currentItem]);
        } else {
            for (const item of items[index]) {
                currentItem[index] = item;
                go(index + 1);
            }
        }
    }

    go(0);
    return result;
}