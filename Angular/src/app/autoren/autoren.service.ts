import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Author } from './author.model';

@Injectable({
  providedIn: 'root'
})
export class AutorenService {

  constructor(private http: HttpClient) { }

  getAuthors() {
    return this.http.get<Author[]>('http://localhost:50000/authors');
  }
}
