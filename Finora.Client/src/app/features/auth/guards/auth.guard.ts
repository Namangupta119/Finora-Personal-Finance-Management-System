import { CanActivate, CanActivateFn, Router } from "@angular/router";
import { StorageService } from "../../../core/services/storage.service";
import { inject } from "@angular/core";


export const authGuard: CanActivateFn = (route, state) => {
    
    const storageService = inject(StorageService);
    const router = inject(Router);

    const token = storageService.getAccessToken();

    if(token){
        return true;
    }

    return router.createUrlTree(['/login']);
}