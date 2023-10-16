class Stack {

    #items = null;

    constructor() {
      this.#items = [];
    }

    push(element) {
      this.#items.push(element);
    }
  
    pop() {
      if (this.isEmpty()) return -1;
      return this.#items.pop();
    }
  
    peek() {
      if (this.isEmpty()) return -1;
      return this.#items[this.#items.length - 1];
    }

    exists(element) { 
        return this.#items.includes(element);
    }
  
    isEmpty() {
      return this.#items.length == 0;
    }
  
    size() {
      return this.#items.length;
    }
  
    print() {
      console.log(this.#items);
    }
}