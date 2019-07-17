import { FilterItem, FilterExpression, OrderingItem } from "../..";
import { FilterOperator } from "../Options/FilterOperator";

export default class DataRequest {
  itemsPerPage: number;

  currentPage: number;

  filters: FilterItem[] = new Array<FilterItem>();

  expressions: FilterExpression[] = new Array<FilterExpression>();

  operators: FilterOperator[] = new Array<FilterOperator>();

  ordering: OrderingItem[] = new Array<OrderingItem>();

  select: string[] = new Array<string>();

  grouping: string = "";

  constructor(itemsPerPage: number, currentPage: number) {
    this.itemsPerPage = itemsPerPage;
    this.currentPage = currentPage;
  }
}
