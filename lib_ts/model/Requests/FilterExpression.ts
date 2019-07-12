import FilterItem from "../Requests/FilterItem";
import { FilterOperator } from "../Options/FilterOperator";

export default class FilterExpression {
  filters?: FilterItem[];

  operators?: FilterOperator[];
}
