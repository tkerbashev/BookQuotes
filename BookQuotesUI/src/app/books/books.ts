import { ChangeDetectorRef, Component, EventEmitter, Output } from '@angular/core';
import { State, Book } from '../../common';
import { BooksService } from './books-service';

@Component({
  selector: 'bq-books',
  imports: [],
  templateUrl: './books.html',
  styleUrl: './books.css',
})
export class Books {
  @Output() stateEvent = new EventEmitter<State>();

  constructor( private booksService: BooksService, private cdRef: ChangeDetectorRef) {}

  public books: Book[] = [];

    ngOnInit() {
    this.booksService.getAllBooks().subscribe({
      next: (data) => {
        console.log('Received authors data:', data);
        this.books = data;
        console.log('Books loaded:', this.books);
      },
      error: (error) => {
        if (error.status === 403) {
          alert('You are not authorised to list the books!');
        }
        console.error('Error fetching authors:', error);
      },
      complete: () => {
        console.log('Completed fetching authors');
        this.cdRef.detectChanges();
      }
    });
  }

  public state = State.selector;
  public StateEnum = State;

  public SetState(state: State): void {
    this.state = state;
    this.shareState();
  }

  shareState() {
    this.stateEvent.emit(this.state);
  }
}
