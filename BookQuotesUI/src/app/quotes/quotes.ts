import { ChangeDetectorRef, Component, EventEmitter, Output } from '@angular/core';
import { Quote, State } from '../../common';
import { QuotesService } from './quotes-service';

@Component({
  selector: 'bq-quotes',
  imports: [],
  templateUrl: './quotes.html',
  styleUrl: './quotes.css',
})
export class Quotes {
  @Output() stateEvent = new EventEmitter<State>();

  constructor( private quotesService: QuotesService, private cdRef: ChangeDetectorRef) {}

  public quotes: Quote[] = [];

    ngOnInit() {
    this.quotesService.getAllQuotes().subscribe({
      next: (data) => {
        console.log('Received authors data:', data);
        this.quotes = data;
      },
      error: (error) => {
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
