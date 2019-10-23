import { User } from '../models/User';

export let _user: User;

export function SetUser(u: User){
    this._user = u;
}