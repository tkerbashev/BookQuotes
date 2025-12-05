import { ChangeDetectorRef, Component, inject, signal } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';
import { Authors } from "./authors/authors";
import { Selection } from "./selection/selection";
import { State } from '../common';
import { Books } from "./books/books";
import { Quotes } from "./quotes/quotes";
import { Login } from "./login/login";
import { CommonModule } from '@angular/common';
import { AuthenticationService } from './authentication-service';

@Component({
  selector: 'bq-root',
  imports: [Authors, Selection, Books, Quotes, Login, CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('BookQuotesUI');
  private authenticationService: AuthenticationService = inject(AuthenticationService);

  constructor(private cdRef: ChangeDetectorRef) {}

  public state: State = State.selector;
  public StateEnum = State;

  public updateState(newState: State) {
    this.state = newState;
    console.log('The parent received a message to update the state to: ' + this.state);
  }

  public isAuthenticated(): boolean {
    return this.authenticationService.IsAuthenticated();
  }

  public refreshView(): void {
    console.log('Refreshing the view after login.');
    this.cdRef.detectChanges();
  }
}
