import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Quote } from '../../common';

@Injectable({
  providedIn: 'root',
})
export class QuotesService {
  constructor(private httpClient: HttpClient) {
  }
  
  getAllQuotes(): Observable<Quote[]> {
    return this.httpClient.get<Quote[]>('/api/Quote/');
  }}
