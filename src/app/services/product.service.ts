import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

export interface Product {
  id: number;
  name: string;
  price: number;
  description: string;
  imageUrl: string;
  category: string;
}

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private products: Product[] = [
    {
      id: 1,
      name: 'Classic T-Shirt',
      price: 29.99,
      description: 'Comfortable cotton t-shirt',
      imageUrl: 'assets/images/tshirt.jpg',
      category: 'T-Shirts'
    },
    {
      id: 2,
      name: 'Denim Jeans',
      price: 59.99,
      description: 'Classic fit denim jeans',
      imageUrl: 'assets/images/jeans.jpg',
      category: 'Pants'
    },
    {
      id: 3,
      name: 'Casual Shirt',
      price: 39.99,
      description: 'Casual button-up shirt',
      imageUrl: 'assets/images/shirt.jpg',
      category: 'Shirts'
    }
  ];

  private productsSubject = new BehaviorSubject<Product[]>(this.products);

  constructor() {}

  getProducts(): Observable<Product[]> {
    return this.productsSubject.asObservable();
  }

  getProductById(id: number): Product | undefined {
    return this.products.find(product => product.id === id);
  }

  getProductsByCategory(category: string): Product[] {
    return this.products.filter(product => product.category === category);
  }
} 