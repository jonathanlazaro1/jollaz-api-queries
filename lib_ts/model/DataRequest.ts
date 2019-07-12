import FilterItem from "./Requests/FilterItem";
import FilterExpression from "./Requests/FilterExpression";
import { FilterOperator } from "./Options/FilterOperator";
import OrderingItem from "./Requests/OrderingItem";

export default class DataRequest {
  itemsPerPage?: number;

  currentPage?: number;

  filters?: FilterItem[];

  expressions?: FilterExpression[];

  operators?: FilterOperator[];

  ordering?: OrderingItem[];

  select?: string[];

  grouping?: string;
}
