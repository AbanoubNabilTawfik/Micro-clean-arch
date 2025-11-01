import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StoreRoutingModule } from './store-routing.module';
import { StoreComponent } from './store.component';
import { HttpClientModule } from '@angular/common/http';
import { ProductItemComponent } from './product-item/product-item.component';
import { SharedModule } from '../shared/shared.module';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    StoreComponent,
    ProductItemComponent
  ],
  imports: [
    CommonModule,
    StoreRoutingModule,
    HttpClientModule,
    SharedModule,
    FormsModule
  ],
  exports:[
    StoreComponent
  ]
})
export class StoreModule { }
