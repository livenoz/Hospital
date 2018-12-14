export class PaginatedListModel<T> {
    pageIndex: number;
    totalPages: number;
    items: Array<T>;
    totalItems: number;
}
