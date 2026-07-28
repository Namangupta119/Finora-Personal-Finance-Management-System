import { Injectable } from "@angular/core";
import { StorageKey } from "../constants/storage-keys";


@Injectable({
    providedIn: 'root'
})

export class StorageService {

    saveAccessToken(token: string): void {
        localStorage.setItem(StorageKey.accessToken, token);
    }

    saveRefreshToken(token: string): void {
        localStorage.setItem(StorageKey.refreshToken, token);
    }

    getAccessToken(): string | null {
        return localStorage.getItem(StorageKey.accessToken);
    }

    getRefreshToken(): string | null {
        return localStorage.getItem(StorageKey.refreshToken);
    }

    clear(): void {
        localStorage.removeItem(StorageKey.accessToken);
        localStorage.removeItem(StorageKey.refreshToken);
    }
}
