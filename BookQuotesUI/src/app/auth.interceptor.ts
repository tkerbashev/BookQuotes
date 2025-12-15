import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthenticationService } from './authentication-service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthenticationService); // Inject your AuthService
  const authToken = authService.getToken(); // Method to retrieve the token

  if (authToken) {
    req = req.clone({
      setHeaders: {
        CustomAuthorization: `Bearer ${authToken}`
      }
    });
  }
  return next(req);
};