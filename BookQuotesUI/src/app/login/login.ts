import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; // Required for common directives like ngIf, ngFor
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; // For form handling
import { AuthenticationService } from '../authentication-service';
import { Router, RouterLink } from "@angular/router";

@Component({
  selector: 'bq-login',
  imports: [CommonModule, FormsModule, ReactiveFormsModule], // Import necessary modules
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  // Form data and logic
  username = '';
  password = '';
  errorMessage = '';

  constructor(private authenticationService: AuthenticationService, private router: Router) {
  }

  authenticate() {
    this.authenticationService.login(this.username, this.password, () => {this.router.navigate(['/selection']);});
  }

}
