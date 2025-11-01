import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IResponseDto } from '../shared/models/response';
import { IProduct } from '../shared/models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit{
  title = 'client';

  constructor()
  {

  }
  ngOnInit(): void {
    
  }
}
