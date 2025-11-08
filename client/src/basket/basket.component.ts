import { Component } from '@angular/core';
import { BasketService } from './basket.service';
import { IBasketItem } from '../shared/models/basket';

@Component({
  selector: 'app-basket',
  standalone: false,
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.scss'
})
export class BasketComponent {
  constructor(public basketService:BasketService){

  }

  decrease(item:IBasketItem){
    this.basketService.decrementItemQuantity(item);
  }
  increase(item:IBasketItem){
    this.basketService.incrementItemQuantity(item);
  }
  remove(item:IBasketItem){
    this.basketService.removeItemFromBasket(item)
  }
}
