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
      name: 'Crop T-Shirt Combo',
      price: 999,
      description: 'Comfortable cotton t-shirt ',
      imageUrl: 'assets/tshirt.jpg',
      category: 'T-Shirts'
    },
    {
      id: 2,
      name: 'Classic T-Shirt',
      price: 1399,
      description: 'Couple Combo T-Shirt',
      imageUrl: 'assets/tshirt1.jpg',
      category: 'T-Shirts'
    },
    {
      id: 3,
      name: 'Denim Jeans',
      price: 899.99,
      description: 'Classic fit denim jeans',
      imageUrl: 'assets/jeans.jpg',
      category: 'Pants'
    },
    {
      id: 4,
      name: 'Denim Jeans',
      price: 1111.99,
      description: 'Classic fit denim jeans',
      imageUrl: 'assets/jeans1.jpg',
      category: 'Pants'
    },
    {
      id: 5,
      name: 'Denim Jeans',
      price: 1199.99,
      description: 'Classic fit denim jeans',
      imageUrl: 'assets/jeans2.jpg',
      category: 'Pants'
    },
    {
      id: 6,
      name: 'Casual Shirt',
      price: 889.99,
      description: 'Casual button-up shirt',
      imageUrl: 'assets/shirt.jpg',
      category: 'Shirts'
    },
    {
      id: 7,
      name: 'Ethnic Combo',
      price: 1133.99,
      description: 'Stay cool and stylish in Ethnic wear with  lightweight and breathable Fabrics !',
      imageUrl: 'assets/dress.jpg',
      category: 'Dresses'
    },
    {
      id: 8,
      name: 'Causal Wear',
      price: 1300.99,
      description: 'Causals combo',
      imageUrl: 'assets/dress1.jpg',
      category: 'Dresses'
    },
    {
      id: 9,
      name: 'Traditional Wear',
      price: 999.99,
      description: 'Ethnic Anarkali ',
      imageUrl: 'assets/dress3.jpg',
      category: 'Dresses'
    },
    {
      id: 10,
      name: 'Designer Wear',
      price: 449.99,
      description: 'Designer Anarkali ',
      imageUrl: 'assets/dress4.jpg',
      category: 'Dresses'
   },
   {
      id: 11,
      name: 'Designer Lehenga',
      price: 1221.99,
      description: 'Stylish and comfortable Designer Lehenga',
      imageUrl: 'assets/dress5.jpg',
      category: 'Dresses'
   },
   {
      id: 12,
      name: 'Summer Dress',
      price: 3117.99,
      description: 'Stay cool and stylish this summer with our lightweight and breathable summer dress! ',
      imageUrl: 'assets/dress6.jpg',
      category: 'Dresses'
    },
    /*{
      id: 13,
      name: 'Hoodies',
      price: 1500,
      description: 'Stay warm and cozy with our premium hoodies!',
      imageUrl: 'assets/hoody.jpg',
      category: 'Hoodies'
    },*/
    {
      id: 14,
      name: 'Pants',
      price: 1999.00,
      description: 'Stay BRIGHT!',
      imageUrl: 'assets/pants.jpg',
      category: 'Pants'
    },
    {
      id: 15,
      name: 'Hoodies',
      price: 1000.99,
      description: 'Stay warm and cozy with our premium hoodies!',
      imageUrl: 'assets/hoody.jpg',
      category: 'Hoodies'
    },
    {
      id: 16,
      name: 'Jackets',
      price: 1930.99,
      description: 'Stay warm and cozy with our premium hoodies!',
      imageUrl: 'assets/jacket.jpg',
      category: 'Jackets'
    },
    {
      id: 17,
      name: 'Winter Sweaters',
      price: 999.99,
      description: 'Warm knit sweater for cold weather!',
      imageUrl: 'assets/sweater.jpg',
      category: 'Sweaters'
    },
    {
      id: 18,
      name: 'Cargo Shorts',
      price: 1200.00,
      description: ' Comfortable cargo shorts with multiple pockets',
      imageUrl: 'assets/short.jpg',
      category: 'Shorts'
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
        return 'tshirt';
      case 'pants':
        return 'socks'; // Using socks icon as pants icon is not available
      case 'hoodies':
        return 'fa-shirt';
      case 'dresses':
        return 'person-dress';
      case 'jackets':
        return 'vest';
      case 'shirts':
        return 'shirt';
      case 'sweaters':
        return 'vest-patches';
      case 'shorts':
        return 'socks'; // Using socks icon as shorts icon is not available
      default:
        return 'shirt';
    }
  }
} 