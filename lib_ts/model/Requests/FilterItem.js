"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const FilterCriterion_1 = require("../Options/FilterCriterion");
class FilterItem {
    constructor() {
        this.name = "";
        this.parameter = new Object();
        this.matchCase = false;
        this.isAdvanced = false;
        this.not = false;
        this.criterion = FilterCriterion_1.FilterCriterion.Equal;
    }
}
exports.default = FilterItem;
