import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common'; // Required for common directives like ngIf, ngFor
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; // For form handling
import { AuthenticationService } from '../authentication-service';

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

  @Output() refreshEvent = new EventEmitter<string>();

  constructor(private authenticationService: AuthenticationService) {}

  authenticate() {
    // Using callback instead of returning observable in order to demonstrate the technique
    this.authenticationService.login(this.username, this.password, () => this.refreshEvent.emit('refresh'));
  }

}
