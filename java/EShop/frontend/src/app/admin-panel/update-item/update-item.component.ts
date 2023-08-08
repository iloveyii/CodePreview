import { Component, Inject, Input, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ItemsService } from 'src/app/core/_services/items.service';
import { ItemDto } from 'src/app/shared/_models/dtos/item-dto';

@Component({
  selector: 'app-update-item',
  templateUrl: './update-item.component.html',
  styleUrls: ['./update-item.component.css'],
})
export class UpdateItemComponent implements OnInit {
  constructor(
    private adminPanelService: ItemsService,
    private _snackBar: MatSnackBar
  ) {}

  @Input() element: any;
  @Input() column: any;

  ngOnInit(): void {}

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action);
  }

  showItemStockEditError(itemId: any) {
    const itemStockEditError = document.getElementById(
      'item-stock-edit-error-' + itemId
    );
    itemStockEditError?.classList.remove('item-stock-edit-error-off');
    itemStockEditError?.classList.add('item-stock-edit-error-on');
  }

  hideItemStockEditError(itemId: any) {
    const itemStockEditError = document.getElementById(
      'item-stock-edit-error-' + itemId
    );
    itemStockEditError?.classList.remove('item-stock-edit-error-on');
    itemStockEditError?.classList.add('item-stock-edit-error-off');
  }

  showItemStockEditForm(itemId: any) {
    const itemStockPreview = document.getElementById(
      'item-stock-preview-' + itemId
    );
    const itemStockEdit = document.getElementById('item-stock-edit-' + itemId);

    if (itemStockPreview != null && itemStockEdit != null) {
      itemStockPreview.classList.remove('item-stock-preview-on');
      itemStockPreview.classList.add('item-stock-preview-off');

      itemStockEdit.classList.remove('item-stock-edit-off');
      itemStockEdit.classList.add('item-stock-edit-on');
    }
  }

  showItemStockPreview(itemId: any, itemStock: any) {
    const itemStockPreview = document.getElementById(
      'item-stock-preview-' + itemId
    );
    const itemStockEdit = document.getElementById('item-stock-edit-' + itemId);

    if (itemStockPreview != null && itemStockEdit != null) {
      itemStockEdit.classList.remove('item-stock-edit-on');
      itemStockEdit.classList.add('item-stock-edit-off');

      itemStockPreview.classList.remove('item-stock-preview-off');
      itemStockPreview.classList.add('item-stock-preview-on');

      const itemStockEditInput = <HTMLInputElement>(
        document.getElementById('item-stock-edit-input-' + itemId)
      );
      itemStockEditInput.value = itemStock;

      this.hideItemStockEditError(itemId);
    }
  }

  updateItemStock(item: any): void {
    const itemStockEditInput = <HTMLInputElement>(
      document.getElementById('item-stock-edit-input-' + item.id)
    );

    const updatedItem: ItemDto = {
      name: item.name,
      price: item.price,
      category: item.category,
      stock: +itemStockEditInput.value,
      description: item.description,
      image: item.image,
    };

    if (updatedItem.stock < 0 || !item.id) {
      this.showItemStockEditError(item.id);
    } else {
      this.hideItemStockEditError(item.id);

      this.adminPanelService.updateItem(item.id, updatedItem).subscribe({
        next: (response) => {
          this.openSnackBar(response.message, 'Dismiss');
        },
        error: (error) => {
          console.log(error);
        },
        complete: () => {
          setTimeout(function () {
            location.reload();
          }, 1000);
        },
      });
    }
  }
}
