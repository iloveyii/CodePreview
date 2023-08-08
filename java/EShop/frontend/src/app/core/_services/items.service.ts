import {HttpClient} from "@angular/common/http";
import {Injectable} from '@angular/core';
import {Observable} from "rxjs";
import {ItemDto} from "../../shared/_models/dtos/item-dto";
import {BaseHttpService} from "../../shared/_services/base-http.service";

@Injectable({
  providedIn: 'root'
})
export class ItemsService extends BaseHttpService {

  constructor(public override http: HttpClient) {
    super('api/items', http);
  }


  getItems(): Observable<Array<ItemDto>> {
    return this.get();
  }

  getItem(id: string): Observable<ItemDto> {
    return this.get(`${id}`);
  }

  createItem(item: ItemDto): Observable<ItemDto> {
    return this.post<ItemDto>(item, '');
  }

  updateItem(id: string, value: ItemDto): Observable<any> {
    return this.put<ItemDto>(value, `${id}`);
  }

  deleteItem(id: string): Observable<any> {
    return this.delete(`${id}`);
  }
}
