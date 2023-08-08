import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { CoreModule } from '../core/core.module';

import { AdminPanelRoutingModule } from './admin-panel-routing.module';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { DeleteConfirmationDialogComponent } from './delete-confirmation-dialog/delete-confirmation-dialog.component';
import { UpdateItemComponent } from './update-item/update-item.component';
import { OrderTableComponent } from './order-table/order-table.component';

@NgModule({
  declarations: [AdminPanelComponent, DeleteConfirmationDialogComponent, UpdateItemComponent, OrderTableComponent],
  imports: [CommonModule, AdminPanelRoutingModule, CoreModule],
})
export class AdminPanelModule {}
