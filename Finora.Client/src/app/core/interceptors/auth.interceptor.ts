import { HttpInterceptorFn } from "@angular/common/http";
import { StorageService } from "../services/storage.service";
import { inject } from "@angular/core";


export const authInterceptor : HttpInterceptorFn = (req, next) => {

    const storageService = inject(StorageService);

    if(req.url.includes('/auth/login'))
    {
        return next(req);
    }

    const token = storageService.getAccessToken();

    if(!token)
        return next(req);

    const clonedRequest = req.clone({
        setHeaders: {
            Authorization: `Bearer ${token}`
        }
    });

    return next(clonedRequest);
}