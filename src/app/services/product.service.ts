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
      name: 'Crop T-Shirt',
      price: 999.00,
      description: 'Comfortable cotton t-shirt',
      imageUrl: 'assets/images/tshirt.jpg',
      category: 'T-Shirts'
    },
    {
      id: 2,
      name: 'Classic T-Shirt',
      price: 1299.00,
      description: 'Couple Combo T-Shirt',
      imageUrl: 'assets/images/tshirt1.jpg',
      category: 'T-Shirts'
    },
    {
      id: 3,
      name: 'Denim Jeans',
      price: 1799.00,
      description: 'Classic fit denim jeans',
      imageUrl: 'assets/images/jeans.jpg',
      category: 'Pants'
    },
    {
      id: 4,
      name: 'Denim Jeans',
      price: 1599.00,
      description: 'Classic fit denim jeans',
      imageUrl: 'assets/images/jeans1.jpg',
      category: 'Pants'
    },
    {
      id: 5,
      name: 'Denim Jeans',
      price: 1699.00,
      description: 'Classic fit denim jeans',
      imageUrl: 'assets/images/jeans2.jpg',
      category: 'Pants'
    },
    {
      id: 6,
      name: 'Casual Shirt',
      price: 1399.00,
      description: 'Casual button-up shirt',
      imageUrl: 'assets/images/shirt.jpg',
      category: 'Shirts'
    },
    {
      id: 7,
      name: 'Ethnic Combo',
      price: 1899.00,
      description: 'Stay cool and stylish this summer with our lightweight and breathable Fabric',
      imageUrl: 'assets/images/dress.jpg',
      category: 'Dresses'
    },
    {
      id: 8,
      name: 'Causals wear',
      price: 1699.00,
      description: 'Causals combo',
      imageUrl: 'assets/images/dress1.jpg',
      category: 'Dresses'
    },
    {
      id: 9,
      name: 'Traditional Wear',
      price: 1499.00,
      description: 'Ethnic Anarkali',
      imageUrl: 'assets/images/dress3.jpg',
      category: 'Dresses'
    },
    {
      id: 10,
      name: 'Designer wear',
      price: 1199.00,
      description: 'Designer Anarkali',
      imageUrl: 'assets/images/dress4.jpg',
      category: 'Dresses'
    },
    {
      id: 11,
      name: 'Designer Lehenga',
      price: 1099.00,
      description: 'Stylish and comfortable Designer Lehenga',
      imageUrl: 'assets/images/dress5.jpg',
      category: 'Dresses'
    },
    {
      id: 12,
      name: 'Summer Dress',
      price: 999.00,
      description: 'Stay cool and stylish this summer with our lightweight and breathable summer dress!',
      imageUrl: 'assets/images/dress6.jpg',
      category: 'Dresses'
    },
    /*{
      id: 13,
      name: 'Hoodies',
      price: 1599.00,
      description: 'Stay warm and cozy with our premium hoodies!',
      imageUrl: 'assets/images/hoody.jpg',
      category: 'Hoodies'
    },*/
    {
      id: 14,
      name: 'Pants',
      price: 1599.00,
      description: 'Stay BRIGHT!',
      imageUrl: 'assets/images/pants.jpg',
      category: 'Pants'
    },
    {
      id: 15,
      name: 'Hoodies',
      price: 1799.00,
      description: 'Stay warm and cozy with our premium hoodies!',
      imageUrl: 'assets/images/hoody1.jpg',
      category: 'Hoodies'
    },
    {
      id: 16,
      name: 'Jackets',
      price: 1899.00,
      description: 'Stay warm and cozy with our premium hoodies!',
      imageUrl: 'assets/images/jacket.jpg',
      category: 'Jackets'
    },
    {
      id: 17,
      name: 'Winter Sweaters',
      price: 1399.00,
      description: 'Warm knit sweater for cold weather!',
      imageUrl: 'assets/images/sweater.jpg',
      category: 'Sweaters'
    },
    {
      id: 18,
      name: 'Cargo Shorts',
      price: 999.00,
      description: 'Comfortable cargo shorts with multiple pockets',
      imageUrl: 'assets/images/short.jpg',
      category: 'Shorts'
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