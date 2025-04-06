import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';

interface User {
  email: string;
  password: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(this.checkInitialAuthState());
  isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

  // Store users in localStorage
  private readonly USERS_KEY = 'etrends_users';
  private users: User[] = [];

  constructor(private router: Router) {
    this.loadUsers();
  }

  private loadUsers(): void {
    try {
      const usersStr = window?.localStorage?.getItem(this.USERS_KEY);
      this.users = usersStr ? JSON.parse(usersStr) : [];
    } catch {
      this.users = [];
    }
  }

  private saveUsers(): void {
    try {
      window?.localStorage?.setItem(this.USERS_KEY, JSON.stringify(this.users));
    } catch {
      console.error('Error saving users');
    }
  }

  private checkInitialAuthState(): boolean {
    try {
      const token = window?.localStorage?.getItem('token');
      return !!token;
    } catch {
      return false;
    }
  }

  register(email: string, password: string): boolean {
    try {
      // Check if email already exists
      if (this.users.some(user => user.email.toLowerCase() === email.toLowerCase())) {
        return false;
      }

      // Add new user
      this.users.push({ email, password });
      this.saveUsers();
      return true;
    } catch {
      return false;
    }
  }

  login(email: string, password: string): boolean {
    try {
      // Check if credentials match any user
      const user = this.users.find(u => 
        u.email.toLowerCase() === email.toLowerCase() && 
        u.password === password
      );

      if (user) {
        window?.localStorage?.setItem('token', 'user-token');
        window?.localStorage?.setItem('user', JSON.stringify({ email: user.email }));
        this.isAuthenticatedSubject.next(true);
        return true;
      }
      return false;
    } catch {
      return false;
    }
  }

  logout(): void {
    try {
      window?.localStorage?.removeItem('token');
      window?.localStorage?.removeItem('user');
      this.isAuthenticatedSubject.next(false);
      this.router.navigate(['/login']);
    } catch {
      console.error('Error during logout');
    }
  }

  getUser(): any {
    try {
      const userStr = window?.localStorage?.getItem('user');
      return userStr ? JSON.parse(userStr) : null;
    } catch {
      return null;
    }
  }

  isAuthenticated(): boolean {
    return this.isAuthenticatedSubject.value;
  }
} 