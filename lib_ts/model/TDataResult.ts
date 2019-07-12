export default class TDataResult<T> {
  itemsTotal?: number;

  itemsPerPage?: number;

  currentPage?: number;

  pageCount?: number;

  items?: T[];
}
