import { Result } from '../models/Result';

export function Authen(result: Result<any>) : void{
    switch(result.code){
        case 401: 
            window.location.href = '/';
            break;
        default:
            alert(result.message);
            break;
    }
}

export function Noti(mess: string){
    alert(mess);
}