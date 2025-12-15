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
        if (error.status === 403) {
          alert('You are not authorised to list the books!');
        }
      },
      complete: () => {
        this.cdRef.detectChanges();
      }
    });
  }
}
