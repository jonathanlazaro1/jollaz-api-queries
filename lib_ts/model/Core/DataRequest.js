"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class DataRequest {
    constructor(itemsPerPage, currentPage) {
        this.filters = new Array();
        this.expressions = new Array();
        this.operators = new Array();
        this.ordering = new Array();
        this.select = new Array();
        this.grouping = "";
        this.itemsPerPage = itemsPerPage;
        this.currentPage = currentPage;
    }
}
exports.default = DataRequest;
