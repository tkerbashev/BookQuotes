import { ChangeDetectorRef, Component, EventEmitter, Output } from '@angular/core';
import { Quote } from '../../common';
import { QuotesService } from './quotes-service';

@Component({
  selector: 'bq-quotes',
  imports: [],
  templateUrl: './quotes.html',
  styleUrl: './quotes.css',
})
export class Quotes {
  constructor( private quotesService: QuotesService, private cdRef: ChangeDetectorRef) {}

  public quotes: Quote[] = [];

    ngOnInit() {
    this.quotesService.getAllQuotes().subscribe({
      next: (data) => {
        this.quotes = data;
      },
      error: (error) => {
        console.error('Error fetching quotes:', error);
      },
      complete: () => {
        this.cdRef.detectChanges();
      }
    });
  }
}
