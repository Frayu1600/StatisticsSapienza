class Node {
    constructor(data) {
        this.data = data;
        this.next = null;
    }
}

class LinkedList {

    #head = null;
    #size = null;

    constructor() {
        this.#head = null;
        this.#size = 0;
    }
 
    add(data) {
        const node = new Node(data);
        if (!this.#head) {
            this.#head = node;
        } else {
            let current = this.#head;
            while (current.next) {
                current = current.next;
            }
            current.next = node;
        }
    }
    

    remove(data) {
        if (!this.#head) return;
        if (this.#head.data == data) {
            this.#head = this.#head.next;
            return;
        }
        let current = this.#head;
        while (current.next) {
            if (current.next.data == data) {
                current.next = current.next.next;
                return;
            }
            current = current.next;
        }
    }

    retrieve(index) {
        let current = this.#head;
        for (let i = 0; i < index; i++) {
            if (!current) return null;
            current = current.next;
        }
        return current.data;
    }

    exists(data) { 
        var current = this.#head;
    
        while (current) {
            if (current.data == data) {
                return true;
            }
            current = current.next;
        }
        return false;
    }
    

    removeElement(data) {
        var current = this.#head;
        var prev = null;
    
        // iterate over the list
        while (current != null) {
            // comparing data with current
            // data if found then remove the
            // and return true
            if (current.data == data) {
                if (prev == null) {
                    this.#head = current.next;
                } else {
                    prev.next = current.next;
                }
                this.#size--;
                return current.data;
            }
            prev = current;
            current = current.next;
        }
        return -1;
    }
    
    isEmpty() {
        return this.#size == 0;
    }
    
    size() {
        return this.#size;
    }
    
    print() {
        var curr = this.#head;
        var output = "";
        while (curr) {
            output += curr.data + " ";
            curr = curr.next;
        }
        console.log(output);
    }
}