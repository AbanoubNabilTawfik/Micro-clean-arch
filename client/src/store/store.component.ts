import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrands, IProduct, ITypes } from '../shared/models/product';
import { StoreService } from './store.service';
import { StoreParams } from '../shared/models/storeParams';

@Component({
  selector: 'app-store',
  standalone: false,
  templateUrl: './store.component.html',
  styleUrl: './store.component.scss',
})
export class StoreComponent implements OnInit {
  @ViewChild('search') searchTerm?: ElementRef;
  products: IProduct[] = [];
  brands: IBrands[] = [];
  types: ITypes[] = [];
  storeParams = new StoreParams();
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price :Ascending', value: 'priceAsc' },
    { name: 'Price :Descending', value: 'priceDesc' },
  ];
  iterPerPageOptions=[
    {value:'10',name:'Default'},
    {value:'5',name:'5 / page'},
    {value:'10',name:'10 / page'},
    {value:'15',name:'15 / page'},
    {value:'20',name:'20 / page'},
    {value:'25',name:'25 / page'},

  ]
   currentPage = 4;
  smallnumPages = 0;
  totalCount=0;
  constructor(private storeServcie: StoreService) {}
  ngOnInit(): void {
    this.getAllProducts();
    this.getAllBrands();
    this.getAllTypes();
  }

  getAllProducts() {
    this.storeServcie.getAllProducts(this.storeParams).subscribe({
      next: (res) => {
        this.products = res.data;
        this.storeParams.pageNumber=res.pageIndex;
        this.storeParams.pageSize=res.pageSize;
        this.totalCount=res.count;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  getAllBrands() {
    this.storeServcie.getAllBrands().subscribe({
      next: (data) => {
        this.brands = [{ id: '', name: 'All' }, ...data];
      },
    });
  }
  getAllTypes() {
    this.storeServcie.getAllTypes().subscribe({
      next: (data) => {
        this.types = [{ id: '', name: 'All' }, ...data];
      },
    });
  }

  onBrandSelected(brandId: string) {
    this.storeParams.brandId = brandId;
    this.getAllProducts();
  }

  onTypeSelected(typeId: string) {
    this.storeParams.typeId = typeId;
    this.getAllProducts();
  }

  onSortSelected(sort:any)
  {
    this.storeParams.sort=sort.value;
    this.getAllProducts();
  }
  onPageChanged(event:any)
  {
    this.storeParams.pageNumber=event.page;
    this.getAllProducts();
  }
  onSearch()
  {
    this.storeParams.search = this.searchTerm?.nativeElement.value;
    this.storeParams.pageNumber=1;
    this.getAllProducts();
  }
  onResetFilters(){
    this.storeParams.brandId='';
    this.storeParams.typeId='';
    this.getAllProducts();
  }
  onItemPerPageChanged(target:any)
  {
    this.storeParams.pageSize=target.value;
    this.getAllProducts();
  }
}
