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
        if (error.status === 403) {
          alert('You are not authorised to list the authors!');
        }
      },
      complete: () => {
        this.cdRef.detectChanges();
      }
    });
  }
}
