"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class TDataResult {
    constructor() {
        this.itemsTotal = 0;
        this.itemsPerPage = 1;
        this.currentPage = 1;
        this.pageCount = 0;
        this.items = new Array();
    }
}
exports.default = TDataResult;
