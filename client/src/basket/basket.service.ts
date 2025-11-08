import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import {
  Basket,
  IBasket,
  IBasketItem,
  IBasketTotal,
} from '../shared/models/basket';
import { IProduct } from '../shared/models/product';
import { AccountService } from '../account/account.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  baseUrl: string = 'http://localhost:8010/';

  private basketSource = new BehaviorSubject<Basket | null>(null);
  basketSource$ = this.basketSource.asObservable();

  private basketTotal = new BehaviorSubject<IBasketTotal | null>(null);
  basketTotal$ = this.basketTotal.asObservable();

  constructor(private http: HttpClient,private accountService:AccountService,private router:Router) {}

  getBasket(userName: string) {
    return this.http
      .get<IBasket>(this.baseUrl + 'Basket/GetBasket/abanoub')
      .subscribe({
        next: (basket) => {
          this.basketSource.next(basket);
          this.calculateBasketTotal();
        },
      });
  }

  setBasket(baskett: IBasket) {
    console.log('api', baskett);
    return this.http
      .post<IBasket>(this.baseUrl + 'Basket/CreateBasket', baskett)
      .subscribe({
        next: (basket) => {
          console.log('adding', basket);
          this.basketSource.next(basket);
          this.calculateBasketTotal();
        },
      });
  }

  getCurrentBasket() {
    return this.basketSource.value;
  }

  addItemToBasket(item: IProduct, quantity = 1) {
    console.log('item', item);
    const itemToAdd: IBasketItem = this.mapProductToBasketItem(item);
    const basket = this.getCurrentBasket() ?? this.createBasket();
    console.log('basket', basket);

    basket.items = this.addOrUpdateItem(basket.items, itemToAdd, quantity);
    this.setBasket(basket);
  }

  mapProductToBasketItem(item: IProduct): IBasketItem {
    return {
      productId: item.id,
      productName: item.name,
      imageFile: item.imageFile,
      price: item.price,
      quantity: 0,
    };
  }

  createBasket() {
    const basket = new Basket();
    localStorage.setItem('basket_username', 'abanoub');
    return basket;
  }

  addOrUpdateItem(
    items: IBasketItem[],
    itemToAdd: IBasketItem,
    quantity: number
  ): IBasketItem[] {
    const item = items.find((x) => x.productId == itemToAdd?.productId);
    if (item) {
      item.quantity += quantity;
    } else {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }
    console.log('items after add', items);
    return items;
  }

  incrementItemQuantity(item: IBasketItem) {
    const basket = this.getCurrentBasket();
    if (!basket) return;
    const foundItemIndex = basket.items.findIndex(
      (x) => x.productId === item.productId
    );
    basket.items[foundItemIndex].quantity++;
    this.setBasket(basket);
  }

  removeItemFromBasket(item: IBasketItem) {
    const basket = this.getCurrentBasket();
    if (!basket) return;
    if (basket.items.some((x) => x.productId === item.productId)) {
      basket.items = basket.items.filter((x) => x.productId !== item.productId);
      if (basket.items.length > 0) {
        this.setBasket(basket);
      } else {
        this.deleteBasket(basket.userName);
      }
    }
  }

  deleteBasket(userName: string) {
    return this.http
      .delete(this.baseUrl + 'Basket/DeleteBasket/' + userName)
      .subscribe({
        next: (response) => {
          this.basketSource.next(null);
          this.basketTotal.next(null);
          localStorage.removeItem('basket_username');
        },
        error: (error) => {
          console.log(error);
        },
      });
  }

  decrementItemQuantity(item: IBasketItem) {
    const basket = this.getCurrentBasket();
    if (!basket) return;
    const foundItemIndex = basket.items.findIndex(
      (x) => x.productId === item.productId
    );
    if (basket.items[foundItemIndex].quantity > 1) {
      basket.items[foundItemIndex].quantity--;
      this.setBasket(basket);
    } else {
      this.removeItemFromBasket(item);
    }
  }

  checkoutBasket(basket:IBasket){
    const httpOptions = {
      headers: new HttpHeaders({
        'content-type':'application/json',
        'Authorization': this.accountService.authorizationHeaderValue
      })
    };

    return this.http.post<IBasket>(this.baseUrl+'Basket/CheckoutV2',basket,httpOptions).subscribe({
      next:basket=>{
        this.basketSource.next(null);
        this.router.navigateByUrl('/');
      }
    })
  }

  private calculateBasketTotal() {
    const basket = this.getCurrentBasket();
    if (!basket) return;
    const total = basket.items.reduce((x, y) => y.price * y.quantity + x, 0);
    this.basketTotal.next({ total });
  }
}
