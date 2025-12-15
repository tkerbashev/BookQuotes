import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  Logout() {
    this.bearerToken = '';
    this.isAuthenticated = false;
  }

  constructor(private httpClient: HttpClient) {
  }

  private isAuthenticated: boolean = false;
  private bearerToken: string = '';

  public IsAuthenticated(): boolean {
    return this.isAuthenticated;
  }

  login(username: string, password: string, callback: () => void = () => {}) {
    try {
      this.getUserInfo(username, password, callback);
    }
    catch (error) {
      console.error('Error during login: ', error);
    }

  }

  getUserInfo(username: string, password: string, callback: () => void = () => {}): void {
    const credentials = { "username": username,  "password": password };
    this.httpClient.post('/api/Authentication/authenticate/', credentials).subscribe({
      next: (response) => {
        this.onSuccess(response);
      },
      error: (error) => {
        this.onError(error);
      },
      complete: () => {
        console.info('complete');
        callback();
      }
    });
  }

  private onSuccess(response: any) {
    this.bearerToken = response.token;
    this.isAuthenticated = true;
  }

  private onError(error: any) {
    this.isAuthenticated = false;
    alert('Invalid username or password.');
    console.log('Login failed with status: ', error.status);
  }

  public getToken(): string {
    return this.bearerToken;
  }
}
