export class Result<T> {
    data: T;
    message: string;
    code: number;
    paging: Paging;
}
export class Paging {
    total: number;
    current_page: number;
    page_size: number;
}