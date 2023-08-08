import {HttpClient} from "@angular/common/http";
import {Injectable} from '@angular/core';
import {Observable} from "rxjs";
import {SignInRequest} from "../../shared/_models/requests/sign-in-request";
import {SignInResponse} from "../../shared/_models/responses/sign-in-response";
import {BaseHttpService} from "../../shared/_services/base-http.service";
//import {AuthGuard} from '../../shared/_guards/auth.guard';
//import {AdminGuard} from '../../shared/_guards/admin.guard';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseHttpService {

  private static readonly LocalStorageKeyAuthToken = 'auth-token';
  private static readonly LocalStorageKeyUserId = 'user-id';
  private static readonly LocalStorageKeyUserName = 'user-name';
  private static readonly LocalStorageKeyIsAdmin = 'user-is-admin';


  constructor(public override http: HttpClient) {
    super('api/auth', http);
  }

  public signIn(request: SignInRequest): Observable<SignInResponse> {
    return this.post<SignInResponse>(request, 'signin');
  }

  public setToken(token: string) {
    localStorage.setItem(AuthService.LocalStorageKeyAuthToken, token);
  }

  public getToken(): string | null {
    return localStorage.getItem(AuthService.LocalStorageKeyAuthToken);
  }

  public setUserId(userId: string) {
    localStorage.setItem(AuthService.LocalStorageKeyUserId, userId);
  }

  public getUserId(): string | null {
    return localStorage.getItem(AuthService.LocalStorageKeyUserId);
  }

  public setUserName(username: string) {
    localStorage.setItem(AuthService.LocalStorageKeyUserName, username);
  }

  public getUserName(): string | null {
    return localStorage.getItem(AuthService.LocalStorageKeyUserName);
  }

  public setIsAdmin(isAdmin: boolean) {
    localStorage.setItem(AuthService.LocalStorageKeyIsAdmin, isAdmin.toString());
  }

  public getIsAdmin(): boolean | null {
    return localStorage.getItem(AuthService.LocalStorageKeyIsAdmin) === 'true';
  }

  public logOut(): void {
    localStorage.removeItem(AuthService.LocalStorageKeyAuthToken);
    localStorage.removeItem(AuthService.LocalStorageKeyUserId);
    localStorage.removeItem(AuthService.LocalStorageKeyUserName);
    localStorage.removeItem(AuthService.LocalStorageKeyIsAdmin);
  }

  public setLocalStorage(response: SignInResponse) {
    this.setToken(response.token);
    this.setUserId(response.userId);
    this.setUserName(response.username);
    this.setIsAdmin(response.isAdmin);
  }

  public isSignedIn(): boolean {
    return this.getUserId() !== null;
  }
}
