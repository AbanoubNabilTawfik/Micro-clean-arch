import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IResponseDto } from '../shared/models/response';
import { IProduct } from '../shared/models/product';
import { BasketService } from '../basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit{
  title = 'client';

  constructor(private basketService:BasketService){

  }

  ngOnInit(): void {
    const basket_name = localStorage.getItem("basket_username");
    if(basket_name){
      this.basketService.getBasket(basket_name);
    }
  }
}
