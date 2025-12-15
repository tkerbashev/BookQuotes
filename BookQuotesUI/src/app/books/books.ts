import { ChangeDetectorRef, Component, EventEmitter, Output } from '@angular/core';
import { Book } from '../../common';
import { BooksService } from './books-service';

@Component({
  selector: 'bq-books',
  imports: [],
  templateUrl: './books.html',
  styleUrl: './books.css',
})
export class Books {
  constructor( private booksService: BooksService, private cdRef: ChangeDetectorRef) {}

  public books: Book[] = [];

    ngOnInit() {
    this.booksService.getAllBooks().subscribe({
      next: (data) => {
        this.books = data;
      },
      error: (error) => {
        console.log('Error while fetching the books, status: ' + error.status);
        if (error.status === 403) {
          alert('You are not authorised to list the books!');
        }
        if (error.status === 401) {
          alert('Please log in first!');
        }
      },
      complete: () => {
        this.cdRef.detectChanges();
      }
    });
  }
}
