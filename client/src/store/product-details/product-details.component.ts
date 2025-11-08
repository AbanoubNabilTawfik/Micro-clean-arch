import { Component, OnInit } from '@angular/core';
import { IProduct } from '../../shared/models/product';
import { StoreService } from '../store.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from '../../basket/basket.service';

@Component({
  selector: 'app-product-details',
  standalone: false,
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit {
  product?:IProduct;
  quantity=1;
  constructor(
    private storeService:StoreService,
    private activatedRoute:ActivatedRoute,
    private bcService:BreadcrumbService,
    private basketService:BasketService
  )
  {

  }
  
  
  ngOnInit(): void {
    this.loadProduct()
  }




  dec() { if (this.quantity > 1) this.quantity--; }
  inc() { this.quantity++; }

  addToCart() {
    // purely demo; no API
    if(this.product){
      this.basketService.addItemToBasket(this.product,this.quantity)
    }
  }

  loadProduct(){
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    this.storeService.getProductById(id!).subscribe({
      next:res=>{
        this.bcService.set("@productDetails",res.name)
        this.product=res;
        
        console.log(res)
      },
      error:error=>{
        console.log(error)
      }
    })
  }
 
}
