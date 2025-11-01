import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IResponseDto } from '../shared/models/response';
import { IBrands, IProduct, ITypes } from '../shared/models/product';
import { StoreParams } from '../shared/models/storeParams';

@Injectable({
  providedIn: 'root',
})
export class StoreService {
  baseUrl: string = 'http://localhost:8010/';
  constructor(private http: HttpClient) {}

  getAllProducts(storeParams: StoreParams) {
    let params = new HttpParams();
    if (storeParams.brandId) {
      params = params.append('brandId', storeParams.brandId);
    }
    if (storeParams.typeId) {
      params = params.append('typeId', storeParams.typeId);
    }

     if (storeParams.search) {
      params = params.append('search', storeParams.search);
    }

    params = params.append('sort', storeParams.sort);
    params = params.append('pageIndex', storeParams.pageNumber);
    params = params.append('pageSize', storeParams.pageSize);

    return this.http.get<IResponseDto<IProduct[]>>(
      `${this.baseUrl}Catalog/GetAllProducts`,
      { params }
    );
  }

  getAllBrands() {
    return this.http.get<IBrands[]>(`${this.baseUrl}Catalog/GetAllBrands`);
  }

  getAllTypes() {
    return this.http.get<ITypes[]>(`${this.baseUrl}Catalog/GetAllTypes`);
  }
}
