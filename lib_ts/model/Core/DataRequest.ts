import { FilterItem, FilterExpression, OrderingItem } from "../..";
import { FilterOperator } from "../Options/FilterOperator";

export default class DataRequest {
  itemsPerPage: number = 1;

  currentPage: number = 0;

  filters: FilterItem[] = new Array<FilterItem>();

  expressions: FilterExpression[] = new Array<FilterExpression>();

  operators: FilterOperator[] = new Array<FilterOperator>();

  ordering: OrderingItem[] = new Array<OrderingItem>();

  select: string[] = new Array<string>();

  grouping: string = "";
}
