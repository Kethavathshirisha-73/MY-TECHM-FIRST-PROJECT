import { Component, OnInit } from '@angular/core';
import { ProductService, Product } from '../../services/product.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  standalone: true,
  imports: [CommonModule, RouterModule]
})
export class HomeComponent implements OnInit {
  featuredProducts: Product[] = [];
  categories: string[] = [];

  constructor(
    private productService: ProductService,
    private cartService: CartService
  ) {}

  ngOnInit(): void {
    this.loadFeaturedProducts();
    this.loadCategories();
  }

  private loadFeaturedProducts(): void {
    this.productService.getProducts().subscribe(products => {
      this.featuredProducts = products.slice(0, 4); // Show first 4 products as featured
    });
  }

  private loadCategories(): void {
    this.productService.getProducts().subscribe(products => {
      this.categories = [...new Set(products.map(product => product.category))];
    });
  }

  addToCart(product: Product): void {
    this.cartService.addToCart(product);
  }
} 