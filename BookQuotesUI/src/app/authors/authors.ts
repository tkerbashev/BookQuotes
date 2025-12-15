import { ChangeDetectorRef, Component, EventEmitter, Output } from '@angular/core';
import { Author } from '../../common';
import { AuthorsService } from './authors-service';

@Component({
  selector: 'bq-authors',
  imports: [],
  templateUrl: './authors.html',
  styleUrl: './authors.css',
})
export class Authors {
  constructor( private authorsService: AuthorsService, private cdRef: ChangeDetectorRef) {}

  public authors: Author[] = [];

  ngOnInit() {
    this.authorsService.getAllAuthors().subscribe({
      next: (data) => {
        this.authors = data;
      },
      error: (error) => {
        console.log('Error while fetching the authors, status: ' + error.status);
        if (error.status === 403) {
          alert('You are not authorised to list the authors!');
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
