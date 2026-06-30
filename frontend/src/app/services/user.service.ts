import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  // URL ของ C# Web API (ปรับ port ตามที่ backend รันจริง)
  private apiUrl = '/api/users';

  constructor(private http: HttpClient) {}

  getAll(): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl);
  }

  getById(id: number): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/${id}`);
  }

  create(user: Partial<User>): Observable<User> {
    return this.http.post<User>(this.apiUrl, user);
  }

  update(id: number, User: Partial<User>): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, User);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}