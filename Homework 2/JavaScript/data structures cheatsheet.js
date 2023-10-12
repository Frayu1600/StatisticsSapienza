/* ARRAY AND LISTS */

// initialization
var array = [];

// add elements
array.push(1);
array.push(6);
array.push(0);
array.push(0);

// loop 
array.forEach((value) =>{
    console.log(value);
})

// remove elements 
array.pop();       
array.slice(2, 1)   

// check existance
array.includes(0);

// retreive elements
console.log(array[0]);
console.log(array[1]);








/*SORTED LIST*/

// initialization
var sl = new SortedList();

// add elements
sl.insert(1);
sl.insert(6);
sl.insert(0);
sl.insert(0);

// loop 
console.log("sorted list = [" + sl.print() + "]");

// remove elements 
sl.remove(6)  
sl.remove(0)  

// check existance
sl.exists(0);

// retreive elements
console.log(array[0]);
console.log(array[1]);







/*DICTIONARY*/

// initialization
var dict = {};

// add elements
dict["Age"] = 23;
dict.Height = 172;

// loop 
for (var key in dict) { 
    console.log(dict[key])
}

// remove elements 
delete "Age";
delete dict.Height;

// check existance
dict.hasOwnProperty('Age');

// retreive elements
console.log(dict["Age"]);
console.log(dict.Height);





/*HASH SET*/
// initialization
var set = new Set();

// add elements
set.add(1);
set.add("cat")
set.add({Age: 23, Height: 173});

// loop 
set.forEach((value) =>{
    console.log(value);
})

// remove elements 
set.delete(1);        // at the bottom
set.delete("cat")   // remvove second element 

// check existance
set.has(1);

// retreive elements
console.log(set);






/*STACK*/

// initialization
var stack = new Stack();

// add elements
stack.push(1);
stack.push(6);
stack.push(0);
stack.push(0);

// loop 
stack.print();

// remove elements 
stack.pop();       

// check existance
stack.exists(0);

//retreive element
stack.peek();








/*QUEUE*/

// initialization
var queue = new Queue();

// add elements
queue.enqueue(1);
queue.enqueue(6);
queue.enqueue(0);
queue.enqueue(0);

// loop 
queue.print();

// remove elements 
queue.dequeue();
queue.dequeue();

// check existance
queue.exists(0);

// retreive elements
console.log(queue.front());







/* LINKED LIST */

// initialization
var linkedList = new LinkedList();
 
// add elements
linkedList.add(1);
linkedList.add(6);
linkedList.add(0);

// loop
linkedList.print();

// remove elements
linkedList.removeElement(0);
linkedList.removeElement(6);
 
// check existance
linkedList.exists(0);

// retrieve elements
console.log(linkedList.retrieve(2))
