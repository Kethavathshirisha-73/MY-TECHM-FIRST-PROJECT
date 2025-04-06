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

export interface CartItem {
  product: Product;
  quantity: number;
}

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartItems: CartItem[] = [];
  private cartSubject = new BehaviorSubject<CartItem[]>([]);

  constructor() {
    this.loadCart();
  }

  private loadCart(): void {
    try {
      const savedCart = window?.localStorage?.getItem('cart');
      if (savedCart) {
        this.cartItems = JSON.parse(savedCart);
        this.cartSubject.next(this.cartItems);
      }
    } catch (error) {
      console.error('Error loading cart from localStorage:', error);
      this.cartItems = [];
      this.cartSubject.next(this.cartItems);
    }
  }

  private saveCart(): void {
    try {
      window?.localStorage?.setItem('cart', JSON.stringify(this.cartItems));
    } catch (error) {
      console.error('Error saving cart to localStorage:', error);
    }
  }

  getCartItems(): Observable<CartItem[]> {
    return this.cartSubject.asObservable();
  }

  addToCart(product: Product): void {
    try {
      const existingItem = this.cartItems.find(item => item.product.id === product.id);
      
      if (existingItem) {
        existingItem.quantity += 1;
      } else {
        this.cartItems.push({ product, quantity: 1 });
      }

      this.cartSubject.next(this.cartItems);
      this.saveCart();
    } catch (error) {
      console.error('Error adding item to cart:', error);
    }
  }

  removeFromCart(productId: number): void {
    try {
      this.cartItems = this.cartItems.filter(item => item.product.id !== productId);
      this.cartSubject.next(this.cartItems);
      this.saveCart();
    } catch (error) {
      console.error('Error removing item from cart:', error);
    }
  }

  updateQuantity(productId: number, quantity: number): void {
    try {
      const item = this.cartItems.find(item => item.product.id === productId);
      if (item) {
        item.quantity = quantity;
        if (item.quantity <= 0) {
          this.removeFromCart(productId);
        } else {
          this.cartSubject.next(this.cartItems);
          this.saveCart();
        }
      }
    } catch (error) {
      console.error('Error updating cart item quantity:', error);
    }
  }

  clearCart(): void {
    try {
      this.cartItems = [];
      this.cartSubject.next(this.cartItems);
      window?.localStorage?.removeItem('cart');
    } catch (error) {
      console.error('Error clearing cart:', error);
    }
  }

  getTotal(): number {
    try {
      return this.cartItems.reduce((total, item) => 
        total + (item.product.price * item.quantity), 0);
    } catch (error) {
      console.error('Error calculating cart total:', error);
      return 0;
    }
  }
} 