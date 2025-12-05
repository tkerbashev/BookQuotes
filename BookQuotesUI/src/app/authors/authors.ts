import { ChangeDetectorRef, Component, EventEmitter, Output } from '@angular/core';
import { State, Author } from '../../common';
import { AuthorsService } from './authors-service';

@Component({
  selector: 'bq-authors',
  imports: [],
  templateUrl: './authors.html',
  styleUrl: './authors.css',
})
export class Authors {
  @Output() stateEvent = new EventEmitter<State>();

  constructor( private authorsService: AuthorsService, private cdRef: ChangeDetectorRef) {}

  public authors: Author[] = [];

  ngOnInit() {
    this.authorsService.getAllAuthors().subscribe({
      next: (data) => {
        console.log('Received authors data:', data);
        this.authors = data;
      },
      error: (error) => {
        if (error.status === 403) {
          alert('You are not authorised to list the authors!');
        }        console.error('Error fetching authors:', error);
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
