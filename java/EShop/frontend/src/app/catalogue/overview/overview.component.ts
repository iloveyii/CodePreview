import {Component, OnDestroy, OnInit} from '@angular/core';
import {MatSnackBar} from "@angular/material/snack-bar";
import {ActivatedRoute} from "@angular/router";
import {combineLatest, map, Observable, Subscription} from "rxjs";
import {ItemsService} from "../../core/_services/items.service";
import {OrdersService} from "../../core/_services/orders.service";
import {UserService} from "../../core/_services/user.service";
import {ItemDto} from "../../shared/_models/dtos/item-dto";
import {AddItemToCartRequest} from "../../shared/_models/requests/add-item-to-cart-request";
import {AuthService} from '../../core/_services/auth.service';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent implements OnInit, OnDestroy {

  $items?: Observable<Array<ItemDto>>;

  private readonly subscriptions: Subscription = new Subscription();

  constructor(private itemService: ItemsService,
              private ordersService: OrdersService,
              private userService: UserService,
              private snackBar: MatSnackBar,
              private activatedRoute: ActivatedRoute,
              private AuthService: AuthService) {
  }

  ngOnInit(): void {
    this.$items = combineLatest([
      this.activatedRoute.queryParamMap,
      this.itemService.getItems()
    ]).pipe(
      map(([paramMap, items]) => {
        console.log(paramMap, items);
        let searchString = paramMap.get('searchString') ?? '';
        const maxPrice = paramMap.get('maxPrice') ?? '10000';
        if (searchString || maxPrice) {
          searchString = searchString.toLowerCase();
          items = items
            .filter(item => item.name?.toLowerCase().includes(searchString))
            .filter(item => item.price <= parseInt(maxPrice));
        }
        return items;
      })
    )
  }

  onAddItemToCart(item: ItemDto) {
    const userId =  this.userService.getCurrentUserId();
    if (!userId) {
      this.snackBar.open('You must be logged in to add items to cart', 'OK', {duration: 3000});
      return;
    }
    const request: AddItemToCartRequest = {
      itemId: item.id,
      customerId: userId,
      quantity: 1
    };
    this.subscriptions.add(
      this.ordersService.addItemToCart(request).subscribe(
        (response) => {
          if (response.requestStatus) {
            this.snackBar.open(`Item ${item.name} added to cart`, '', {duration: 2000});
          } else {
            this.snackBar.open(`Item ${item.name} could not be added to cart`, '', {duration: 2000});
          }
        },
        error => {
          this.snackBar.open(`Item ${item.name} could not be added to cart`, '', {duration: 2000});
          console.error(error);
        }
      )
    );
  }

  ngOnDestroy()
    :
    void {
    this.subscriptions.unsubscribe();
  }

  get notSignedInBool(): any {
    return this.AuthService.isSignedIn()
  }
}
