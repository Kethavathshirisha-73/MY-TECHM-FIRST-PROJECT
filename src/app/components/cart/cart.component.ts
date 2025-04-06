import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CartService, CartItem } from '../../services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
  standalone: true,
  imports: [CommonModule, RouterModule]
})
export class CartComponent implements OnInit {
  cartItems: CartItem[] = [];
  total: number = 0;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.loadCart();
  }

  private loadCart(): void {
    this.cartService.getCartItems().subscribe(items => {
      this.cartItems = items;
      this.total = this.cartService.getTotal();
    });
  }

  updateQuantity(item: CartItem, newQuantity: number): void {
    if (newQuantity > 0) {
      this.cartService.updateQuantity(item.product.id, newQuantity);
    }
  }

  removeItem(productId: number): void {
    this.cartService.removeFromCart(productId);
  }

  clearCart(): void {
    this.cartService.clearCart();
  }

  getProductIcon(productName: string): string {
    const name = productName.toLowerCase();
    if (name.includes('t-shirt')) return 'fa-tshirt';
    if (name.includes('shirt')) return 'fa-shirt';
    if (name.includes('jeans') || name.includes('pants')) return 'fa-socks';
    if (name.includes('hoodie') || name.includes('sweatshirt')) return 'fa-shirt';
    if (name.includes('dress')) return 'fa-person-dress';
    if (name.includes('jacket')) return 'fa-vest';
    if (name.includes('sweater')) return 'fa-vest-patches';
    if (name.includes('shorts')) return 'fa-socks';
    return 'fa-tshirt'; // default icon
  }
} 