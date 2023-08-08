import {Component, OnInit} from '@angular/core';
import {MatSnackBar} from "@angular/material/snack-bar";
import {map, Observable} from 'rxjs';
import {AuthService} from './core/_services/auth.service';
import {OrdersService} from "./core/_services/orders.service";
import {UserService} from './core/_services/user.service';
import {OrderItemsCountRequest} from './shared/_models/requests/order-items-count-request';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'frontend';
  date = new Date();

  $cartItemCount?: Observable<number>;

  constructor(private orderService: OrdersService,
              private userService: UserService,
              private AuthService: AuthService,
              private snackBar: MatSnackBar) {
  }

  ngOnInit(): void {
    this.getCartItemsCount();
  }

  getCartItemsCount(): void {
    // @Todo - after ordering GUI
    const userId = this.userService.getCurrentUserId();
    if (!userId) return;
    const orderItemsCount: OrderItemsCountRequest = {
      userId
    };
    this.$cartItemCount = this.orderService.getOrderItemsCount(orderItemsCount)
      .pipe(map((resp) => {
        return resp?.count ?? 0;
      }));
  }

  logout(): void {
    this.AuthService.logOut()
    this.snackBar.open('You are logged out', '', {
      duration: 2000,
    });
  }

  get user(): any {
    return this.AuthService.getUserName()
  }

  get notSignedInBool(): any {
    return this.AuthService.isSignedIn()
  }

  get isAdmin(): any {
    return this.AuthService.getIsAdmin()
  }

}
