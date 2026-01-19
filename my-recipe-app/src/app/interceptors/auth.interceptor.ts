import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth-service';
import { catchError, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const token = authService.getToken();

  // Eğer token varsa isteği klonla ve header ekle
  if (token) {
    const cloned = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
    
    return next(cloned).pipe(
    catchError((error) => {
      // Eğer hata 401 ise oturum düşmüştür
      if (error.status === 401) {
        authService.logout(); // LocalStorage'ı temizler ve /login'e yönlendirir
      }
      return throwError(() => error);
    })
  );
  }

  // Token yoksa isteği olduğu gibi devam ettir
  return next(req);
};