class SortedList {

    #items = null;

    constructor() {
      this.#items = [];
    }
  
    insert(element) {
      if (this.isEmpty()) this.#items.push(element);
      else {
        let inserted = false;

        for (let i = 0; i < this.#items.length; i++) {
          if (element < this.#items[i]) {
            this.#items.splice(i, 0, element);
            inserted = true;
            break;
          }
        }

        if (!inserted) {
          this.#items.push(element); // Add to the end if it's the largest element
        }
      }
    }

    exists(element) {
        return this.#items.includes(element);
    }
  
    remove(element) {
      const index = this.#items.indexOf(element);
      if (index != -1) this.#items.splice(index, 1);
    }
  
    isEmpty() {
      return this.#items.length == 0;
    }
  
    size() {
      return this.#items.length;
    }
  
    print() {
      return this.#items;
    }
}
  