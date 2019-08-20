import { FilterItem, FilterExpression, OrderingItem } from "../..";
import { FilterOperator } from "../Options/FilterOperator";
import { ProcessingMethod } from "../Options/ProcessingMethod";

export default class DataRequest {
  itemsPerPage: number;

  currentPage: number;

  filters: FilterItem[] = new Array<FilterItem>();

  expressions: FilterExpression[] = new Array<FilterExpression>();

  operators: FilterOperator[] = new Array<FilterOperator>();

  ordering: OrderingItem[] = new Array<OrderingItem>();

  select: string[] = new Array<string>();

  grouping: string = "";

  methods: ProcessingMethod[] = new Array<ProcessingMethod>(
    ProcessingMethod.Order,
    ProcessingMethod.Select,
    ProcessingMethod.Group
  );

  constructor(itemsPerPage: number, currentPage: number) {
    this.itemsPerPage = itemsPerPage;
    this.currentPage = currentPage;
  }
}
