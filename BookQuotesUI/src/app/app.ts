import { ChangeDetectorRef, Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthenticationService } from './authentication-service';
import { SiteHeader } from "./site-header/site-header";

@Component({
  selector: 'bq-root',
  imports: [SiteHeader, CommonModule, RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('BookQuotesUI');
  private authenticationService: AuthenticationService = inject(AuthenticationService);

  public isAuthenticated(): boolean {
    return this.authenticationService.IsAuthenticated();
  }
}
