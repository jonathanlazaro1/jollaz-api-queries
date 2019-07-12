"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var FilterCriterion;
(function (FilterCriterion) {
    FilterCriterion[FilterCriterion["Equal"] = 0] = "Equal";
    FilterCriterion[FilterCriterion["LessThanOrEqual"] = 1] = "LessThanOrEqual";
    FilterCriterion[FilterCriterion["LessThan"] = 2] = "LessThan";
    FilterCriterion[FilterCriterion["GreaterThanOrEqual"] = 3] = "GreaterThanOrEqual";
    FilterCriterion[FilterCriterion["GreaterThan"] = 4] = "GreaterThan";
    FilterCriterion[FilterCriterion["StringContains"] = 5] = "StringContains";
    FilterCriterion[FilterCriterion["StringStartsWith"] = 6] = "StringStartsWith";
    FilterCriterion[FilterCriterion["StringEndsWith"] = 7] = "StringEndsWith";
    FilterCriterion[FilterCriterion["NotNull"] = 8] = "NotNull";
})(FilterCriterion = exports.FilterCriterion || (exports.FilterCriterion = {}));
