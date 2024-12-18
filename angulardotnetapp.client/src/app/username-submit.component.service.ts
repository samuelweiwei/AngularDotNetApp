// username-submit.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export default class UserSubmitService {
  private apiUrl = '/api/submit';

  constructor(private http: HttpClient) { }

  submitUser(userData: any): Observable<any> {
    return this.http.post(this.apiUrl, userData);
  }
}
