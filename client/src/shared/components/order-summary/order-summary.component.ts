import { Component } from '@angular/core';
import { BasketService } from '../../../basket/basket.service';

@Component({
  selector: 'app-order-summary',
  standalone: false,
  templateUrl: './order-summary.component.html',
  styleUrl: './order-summary.component.scss'
})
export class OrderSummaryComponent {
  constructor(public basketService:BasketService){}
}
