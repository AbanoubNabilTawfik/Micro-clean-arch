export interface IResponseDto<T> {
  pageIndex: number
  pageSize: number
  count: number
  data: T
}