import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CartService } from '../../services/cart.service';
import { Observable, map } from 'rxjs';
import { CartItem } from '../../services/cart.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  standalone: true,
  imports: [CommonModule, RouterModule]
})
export class HeaderComponent {
  isAuthenticated$: Observable<boolean>;
  cartItemCount$: Observable<number>;

  constructor(
    private authService: AuthService,
    private cartService: CartService,
    private router: Router
  ) {
    this.isAuthenticated$ = this.authService.isAuthenticated$;
    this.cartItemCount$ = this.cartService.getCartItems().pipe(
      map(items => items.reduce((total, item) => total + item.quantity, 0))
    );
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
} 