import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {

  constructor(private httpClient: HttpClient) {
  }

  private isAuthenticated: boolean = false;
  private bearerToken: string = '';

  public IsAuthenticated(): boolean {
    return this.isAuthenticated;
  }

  login(username: string, password: string, callback: () => void = () => {}) {
    console.log('Attempting login for user: ' + username);
    try {
      this.getUserInfo(username, password, callback);
    }
    catch (error) {
      console.error('Error during login: ', error);
    }

  }

  getUserInfo(username: string, password: string, callback: () => void = () => {}): void {
    const credentials = { "username": username,  "password": password };
    console.log('Sending credentials to server');
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
    console.log('Login successful, token received');
  }

  private onError(error: any) {
    this.isAuthenticated = false;
    alert('Invalid username or password.');
    console.log('Login failed:', error.status);
  }

  public getToken(): string {
    return this.bearerToken;
  }
}
