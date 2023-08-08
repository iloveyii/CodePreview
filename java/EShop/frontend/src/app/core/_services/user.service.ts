import {HttpClient} from "@angular/common/http";
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {UserDto} from "src/app/shared/_models/dtos/user-dto";
import {AuthService} from '../../core/_services/auth.service';
import {BaseHttpService} from "../../shared/_services/base-http.service";

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseHttpService {

  constructor(public override http: HttpClient, private authService: AuthService) {
    super('api/users', http)
  }

  public save(user: UserDto): Observable<UserDto> {
    return this.post<UserDto>(user, 'add');
  }

  public getCurrentUserId(): string | null {
    // return hardcoded admin id for now
    return this.authService.getUserId();
  }
}
