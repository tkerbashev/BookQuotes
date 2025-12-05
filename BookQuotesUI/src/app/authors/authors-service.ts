import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Author } from '../../common';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthorsService {
  constructor(private httpClient: HttpClient) {
  }
  
  getAllAuthors(): Observable<Author[]> {
    return this.httpClient.get<Author[]>('/api/Author/');
  }
}
