document.addEventListener('DOMContentLoaded', function() {
    const fileInput = document.getElementById('fileInput');
    const buttonReadFile = document.getElementById('getData');
  
    const matrix = [];
  
    // read .tsv file content
    buttonReadFile.addEventListener('click', function() {
        const file = fileInput.files[0];
        if (file == null) return;
  
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

        console.log(matrix);
    });
});
  