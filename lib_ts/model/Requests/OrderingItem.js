"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class OrderingItem {
    constructor(name, descending = false) {
        this.name = name;
        this.descending = descending;
    }
}
exports.default = OrderingItem;
