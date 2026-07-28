import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../../environments/environments";
import { CreateCategoryRequest } from "../models/create-category-request";
import { Observable } from "rxjs";


@Injectable({
    providedIn: 'root',
    
})

export class CategoryServices{

    private readonly http = inject(HttpClient);
    private readonly apiUrl = `${environment.apiUrl}/categories`;

    createCategory(request: CreateCategoryRequest): Observable<string> {
        return this.http.post<string>(this.apiUrl, request);
    }
}