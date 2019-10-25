import { News } from './News';

export class NewsGroup{
    id: number;
    name: string;
    active: boolean;
    sort: number;
    news: News[];
}