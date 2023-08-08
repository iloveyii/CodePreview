import {HttpClient, HttpParams} from "@angular/common/http";
import {Injectable} from '@angular/core';
import {Observable} from "rxjs";
import { OrderChangeStatusRequest } from "src/app/shared/_models/requests/order-change-status-request";
import { RemoveItemFromCartResponse } from "src/app/shared/_models/responses/remove-item-from-cart-response";
import {OrderDto} from "../../shared/_models/dtos/order-dto";
import {AddItemToCartRequest} from "../../shared/_models/requests/add-item-to-cart-request";
import {OrderItemsCountRequest} from "../../shared/_models/requests/order-items-count-request";
import {AddItemToCartResponse} from "../../shared/_models/responses/add-item-to-cart-response";
import {OrderItemsCountResponse} from "../../shared/_models/responses/order-items-count-response";
import {BaseHttpService} from "../../shared/_services/base-http.service";

@Injectable({
  providedIn: 'root'
})
export class OrdersService extends BaseHttpService {

  constructor(public override http: HttpClient) {
    super('api/order', http);
  }

  public addItemToCart(request: AddItemToCartRequest): Observable<AddItemToCartResponse> {
    return this.post<AddItemToCartResponse>(request, 'cart');
  }

  public removeItemFromCart(id: string): Observable<any> {
    return this.delete(`cart/${id}`);
  }

  public changeOrderStatus(orderChangeStatusRequest: OrderChangeStatusRequest): Observable<any> {
    return this.put(orderChangeStatusRequest, `status/${orderChangeStatusRequest.orderId}`);
  }
  

  public getOrderItemsCount(orderItemsCount: OrderItemsCountRequest): Observable<OrderItemsCountResponse> {
    const params = new HttpParams({
      fromObject: {
        ...orderItemsCount
      }
    });
    return this.get('count?' + params.toString());
  }

  public getCart(userId: string): Observable<OrderDto> {
    const params = new HttpParams({
      fromObject: {userId}
    });
    return this.get('cart?' + params.toString());
  }

  public getOrders(): Observable<Array<OrderDto>> {
    return this.get();
  }

}
