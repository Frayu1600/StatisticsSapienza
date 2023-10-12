class Queue {

    #items = null;

    constructor() {
      this.#items = [];
    }
  
    enqueue(element) {
      this.#items.push(element);
    }
  
    dequeue() {
      if (this.isEmpty()) return -1;
      return this.#items.shift();
    }

    exists(element) {
        return this.#items.includes(element);
    }
  
    front() {
      if (this.isEmpty()) return -1;
      return this.#items[0];
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