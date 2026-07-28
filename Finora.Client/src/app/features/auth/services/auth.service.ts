import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { Observable } from 'rxjs';
import { LoginResponse } from '../models/login-response.model';
import { environment } from '../../../../environments/environments';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly http = inject(HttpClient);

  private readonly baseUrl = environment.apiUrl;

  login(request: LoginRequest): Observable<LoginResponse> {
    
    return this.http.post<LoginResponse>(
      `${this.baseUrl}/auth/login`,request);
  } 
}