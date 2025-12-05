import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from '../../common';

@Injectable({
  providedIn: 'root',
})
export class BooksService {
  constructor(private httpClient: HttpClient) {
  }
  
  getAllBooks(): Observable<Book[]> {
    return this.httpClient.get<Book[]>('/api/Book/');
  }}
