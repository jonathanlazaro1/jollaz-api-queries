"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class FilterItem {
    constructor(name, parameter, criterion) {
        this.matchCase = false;
        this.isAdvanced = false;
        this.not = false;
        this.name = name;
        this.parameter = parameter;
        this.criterion = criterion;
    }
}
exports.default = FilterItem;
