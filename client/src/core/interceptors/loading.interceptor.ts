import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { LoadingService } from '../services/loading.service';
import { delay, finalize } from 'rxjs';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const loadingSevice=inject(LoadingService);
  loadingSevice.loading();
  return next(req).pipe(
    delay(1000),
    finalize(()=>loadingSevice.idle())
  );
};
