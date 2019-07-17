import { FilterCriterion } from "../Options/FilterCriterion";

export default class FilterItem {
  name: string;

  parameter: any;

  criterion: FilterCriterion;

  matchCase: boolean;

  isAdvanced: boolean;

  not: boolean;

  constructor(
    name: string,
    parameter: any,
    criterion: FilterCriterion,
    matchCase = false,
    isAdvanced = false,
    not = false
  ) {
    this.name = name;
    this.parameter = parameter;
    this.criterion = criterion;
    this.matchCase = matchCase;
    this.isAdvanced = isAdvanced;
    this.not = not;
  }
}
