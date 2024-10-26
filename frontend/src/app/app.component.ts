import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ComplimentComponent } from './compliment/compliment.component';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ComplimentComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'frontend';
}
