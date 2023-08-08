import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { OrderTableDataSource, OrderTableItem } from './order-table-datasource';
import { OrdersService } from 'src/app/core/_services/orders.service';
import { OrderStatus } from 'src/app/shared/_models/enums/order-status';
import { OrderChangeStatusRequest } from 'src/app/shared/_models/requests/order-change-status-request';
import { OrderDto } from 'src/app/shared/_models/dtos/order-dto';


@Component({
  selector: 'order-table',
  templateUrl: './order-table.component.html',
  styleUrls: ['./order-table.component.css']
})
export class OrderTableComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<OrderTableItem>;
  dataSource: OrderTableDataSource;
  orders: any;
  ORDERED: OrderStatus.ORDERED;
  STATUS = OrderStatus;
  expandedElementTwo: OrderDto | null;

  displayedColumns = ['customerFirstName', 'customerAddress', 'customerPhone', 'createdAt', 'totalPrice', 'orderStatus', 'action'];
  

  constructor(private orderService: OrdersService, private _snackBar: MatSnackBar) {
  }

  ngOnInit(): void {
    this.retrieveOrders();
  }

  retrieveOrders(): void {
    this.orderService.getOrders().subscribe({
      next: (data) => {
        const filteredData = data.filter(
                    (emptyOrder) => emptyOrder.orderStatus != OrderStatus.CART && emptyOrder.customerFirstName != "Admin"
        );
        this.orders = filteredData.map(order => {
          order.createdAt = new Date(order.createdAt).toLocaleDateString(navigator.language);
          order.orderStatusLabel = order.orderStatus == OrderStatus.ORDERED ? 'ORDERED' : 'SHIPPED';
          return order;
        });
        this.refreshDataSource(this.orders);
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  changeOrderStatus(order: any, orderStatus: number) {
    const orderChangeStatusRequest: OrderChangeStatusRequest = {
      orderId: order.id,
      orderStatus: orderStatus
    };
    this.orderService.changeOrderStatus(orderChangeStatusRequest).subscribe({
      next: () => {
          this.openSnackBar("Order status changed to " + (orderStatus == 1 ? "ORDERED.": "SHIPPED."), "Dismiss");
      },
      error: (error:any) => {
          console.log(error);
      },
      complete: () => {
        this.retrieveOrders();
      }
    })
  }

  refreshDataSource(data:any) {
    this.dataSource = new OrderTableDataSource([]);
    this.dataSource.data = data;
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource;

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  applyFilter(event:any) {
    const filterValue = (event.target as HTMLInputElement).value;
    const value = filterValue.trim().toLowerCase();
    let data : any;
    
    if(value && value.length > 1) {
      data = this.orders.filter((order:any)=> {
        const tPrice = '' + order.totalPrice;
        return order.customerFirstName.includes(value) || order.customerAddress.includes(value) || order.customerPhone.includes(value) || tPrice.includes(value);
      });
    } else {
      data = this.orders;
    }
    this.refreshDataSource(data);
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action);
  }

  ngAfterViewInit(): void {
  }
}
