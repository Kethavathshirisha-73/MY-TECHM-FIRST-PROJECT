import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService, Product } from '../../services/product.service';
import { CartService } from '../../services/cart.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class ProductsComponent implements OnInit {
  products: Product[] = [
    {
      id: 1,
      name: 'Classic T-Shirt',
      price: 249.99,
      description: 'Comfortable cotton t-shirt in various colors',
      category: 'T-Shirts',
      imageUrl: ''
    },
    {
      id: 2,
      name: 'Denim Jeans',
      price: 299.99,
      description: 'Classic fit denim jeans with perfect comfort',
      category: 'Pants',
      imageUrl: ''
    },
    {
      id: 3,
      name: 'Hooded Sweatshirt',
      price: 279.99,
      description: 'Warm and cozy hoodie for casual wear',
      category: 'Hoodies',
      imageUrl: ''
    },
    {
      id: 4,
      name: 'Summer Dress',
      price: 349.99,
      description: 'Light and flowy summer dress',
      category: 'Dresses',
      imageUrl: ''
    },
    {
      id: 5,
      name: 'Sports Jacket',
      price: 399.99,
      description: 'Lightweight sports jacket for active lifestyle',
      category: 'Jackets',
      imageUrl: ''
    },
    {
      id: 6,
      name: 'Formal Shirt',
      price: 229.99,
      description: 'Classic formal shirt for professional look',
      category: 'Shirts',
      imageUrl: ''
    },
    {
      id: 7,
      name: 'Winter Sweater',
      price: 289.99,
      description: 'Warm knit sweater for cold weather',
      category: 'Sweaters',
      imageUrl: ''
    },
    {
      id: 8,
      name: 'Cargo Shorts',
      price: 259.99,
      description: 'Comfortable cargo shorts with multiple pockets',
      category: 'Shorts',
      imageUrl: ''
    }
  ];

  filteredProducts: Product[] = [];
  categories: string[] = ['T-Shirts', 'Pants', 'Hoodies', 'Dresses', 'Jackets', 'Shirts', 'Sweaters', 'Shorts'];
  selectedCategory: string = '';
  sortBy: string = 'name';

  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.filteredProducts = this.products;
    this.route.queryParams.subscribe(params => {
      if (params['category']) {
        this.selectedCategory = params['category'];
        this.filterProducts();
      }
    });
  }

  filterProducts(): void {
    if (this.selectedCategory) {
      this.filteredProducts = this.products.filter(
        product => product.category === this.selectedCategory
      );
    } else {
      this.filteredProducts = this.products;
    }
    this.sortProducts();
  }

  sortProducts(): void {
    this.filteredProducts.sort((a, b) => {
      switch (this.sortBy) {
        case 'price-low':
          return a.price - b.price;
        case 'price-high':
          return b.price - a.price;
        case 'name':
          return a.name.localeCompare(b.name);
        default:
          return 0;
      }
    });
  }

  onSortChange(): void {
    this.sortProducts();
  }

  addToCart(product: Product): void {
    this.cartService.addToCart(product);
  }

  getProductIcon(category: string): string {
    switch (category.toLowerCase()) {
      case 't-shirts':
        return 'fa-tshirt';
      case 'pants':
        return 'fa-socks'; // Using socks icon as pants icon is not available
      case 'hoodies':
        return 'fa-shirt';
      case 'dresses':
        return 'fa-person-dress';
      case 'jackets':
        return 'fa-vest';
      case 'shirts':
        return 'fa-shirt';
      case 'sweaters':
        return 'fa-vest-patches';
      case 'shorts':
        return 'fa-socks'; // Using socks icon as shorts icon is not available
      default:
        return 'fa-shirt';
    }
  }
} 