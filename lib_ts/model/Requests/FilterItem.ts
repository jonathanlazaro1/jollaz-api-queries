import { FilterCriterion } from "../Options/FilterCriterion";

export default class FilterItem {
  name: string;

  parameter: any;

  criterion: FilterCriterion;

  matchCase: boolean = false;

  isAdvanced: boolean = false;

  not: boolean = false;

  constructor(name: string, parameter: any, criterion: FilterCriterion) {
    this.name = name;
    this.parameter = parameter;
    this.criterion = criterion;
  }
}
