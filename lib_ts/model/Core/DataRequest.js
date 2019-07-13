"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class DataRequest {
    constructor() {
        this.itemsPerPage = 1;
        this.currentPage = 0;
        this.filters = new Array();
        this.expressions = new Array();
        this.operators = new Array();
        this.ordering = new Array();
        this.select = new Array();
        this.grouping = "";
    }
}
exports.default = DataRequest;
