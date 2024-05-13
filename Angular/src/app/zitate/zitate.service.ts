import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Quote } from './quote.model';

@Injectable({
  providedIn: 'root'
})
export class ZitateService {

  constructor(private http: HttpClient) { }

  getQuotes(authorId: number) {
    return this.http.get<Quote[]>(`http://localhost:50000/authors/${authorId}/quotes`);
  }
}
