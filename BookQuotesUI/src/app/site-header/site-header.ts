import { Component, inject } from '@angular/core';
import { Router, RouterLink } from "@angular/router";
import { AuthenticationService } from '../authentication-service';

@Component({
  selector: 'bq-site-header',
  imports: [RouterLink],
  templateUrl: './site-header.html',
  styleUrl: './site-header.css',
})
export class SiteHeader {

  constructor(private router: Router) {}

  private authenticationService: AuthenticationService = inject(AuthenticationService);

  public get logInOutText(): string {
    return this.authenticationService.IsAuthenticated() ? "Log Out" : "Log In";
  }

  logOut(): void {
    this.authenticationService.Logout();
    this.router.navigate(['/login']);
  }

}
