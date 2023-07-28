// Interface for the pagination object retrieved from the backend, uses generic type so that the pagination data can be of any type
export interface Pagination<T> {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: T;
}
