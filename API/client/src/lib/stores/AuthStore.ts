import { makeAutoObservable } from "mobx";

export default class AuthStore {

  isLoggedIn: boolean = false;
  userName: string | null = null;
 // const { login } =  useAccounts();

  constructor() {
    makeAutoObservable(this);
  }

  login(user: string) {
   
    // const result = await login.mutateAsync(data);
    this.isLoggedIn = true;
    this.userName = user;

  }

  logout() {
    this.isLoggedIn = false;
    this.userName = null;
  }
 }
