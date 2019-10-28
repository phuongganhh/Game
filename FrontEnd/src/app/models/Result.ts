export class IResult {
    message: string;
    code: number;
}
export class Result<T> extends IResult {
    data: T;
    paging: Paging;
}



export class Paging {
    total: number;
    current_page: number;
    page_size: number;
}