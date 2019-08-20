"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const ProcessingMethod_1 = require("../Options/ProcessingMethod");
class DataRequest {
    constructor(itemsPerPage, currentPage) {
        this.filters = new Array();
        this.expressions = new Array();
        this.operators = new Array();
        this.ordering = new Array();
        this.select = new Array();
        this.grouping = "";
        this.methods = new Array(ProcessingMethod_1.ProcessingMethod.Order, ProcessingMethod_1.ProcessingMethod.Select, ProcessingMethod_1.ProcessingMethod.Group);
        this.itemsPerPage = itemsPerPage;
        this.currentPage = currentPage;
    }
}
exports.default = DataRequest;
