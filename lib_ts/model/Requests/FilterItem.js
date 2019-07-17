"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class FilterItem {
    constructor(name, parameter, criterion, matchCase = false, isAdvanced = false, not = false) {
        this.name = name;
        this.parameter = parameter;
        this.criterion = criterion;
        this.matchCase = matchCase;
        this.isAdvanced = isAdvanced;
        this.not = not;
    }
}
exports.default = FilterItem;
