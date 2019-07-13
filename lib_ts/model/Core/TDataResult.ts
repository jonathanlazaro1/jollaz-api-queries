export default class TDataResult<T> {
  itemsTotal: number = 0;

  itemsPerPage: number = 1;

  currentPage: number = 1;

  pageCount: number = 0;

  items: T[] = new Array<T>();
}
