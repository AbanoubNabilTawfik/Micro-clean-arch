export interface IProduct {
  id: string
  name: string
  description: string
  summary: string
  imageFile: string
  price: number
  brands: IBrands
  types: ITypes
}

export interface IBrands {
  name: string
  id: string
}

export interface ITypes {
  name: string
  id: string
}