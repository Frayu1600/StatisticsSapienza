
values = [];
let n = 500000;
let k = 10;

for(var i = 0; i < n; i++) {
    values.push(Math.random());
}

interval = [];
for(var i = 0; i < k; i++) {
    interval.push(0);
}


for(var i = 0; i < n; i++) 
{
    for(var j = 1; j <= k; j++) 
    {
        if(values[i] <= j/k) 
        { 
            interval[j-1]++;
            break;
        }
        else continue;
    }
}

document.querySelector("#test").innerHTML = interval;