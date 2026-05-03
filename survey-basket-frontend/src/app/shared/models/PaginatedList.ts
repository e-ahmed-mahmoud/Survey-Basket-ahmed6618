// Pagination

export interface PaginatedList<T> {
    items: T[];
    totalCount: number;
    pageNumber: number;
    pageSize: number;
}

export interface PaginationFilter {
    pageNumber?: number;
    pageSize?: number;
    search?: string;
    sort?: string;
    sortDir?: 'asc' | 'desc';
}
