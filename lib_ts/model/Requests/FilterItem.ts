import { FilterCriterion } from "../Options/FilterCriterion";

export default class FilterItem {
  name: string = "";

  parameter: any = new Object();

  matchCase: boolean = false;

  isAdvanced: boolean = false;

  not: boolean = false;

  criterion: FilterCriterion = FilterCriterion.Equal;
}
