import {CommonModule} from '@angular/common';
import {NgModule} from '@angular/core';
import {CoreModule} from "../core/core.module";

import {CatalogueRoutingModule} from './catalogue-routing.module';
import {OverviewComponent} from './overview/overview.component';


@NgModule({
  declarations: [
    OverviewComponent
  ],
  imports: [
    CommonModule,
    CatalogueRoutingModule,
    CoreModule
  ]
})
export class CatalogueModule {
}
