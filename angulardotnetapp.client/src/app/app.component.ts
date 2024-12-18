import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators} from '@angular/forms';
import UserSubmitService from './username-submit.component.service';

interface UserName {
  firstName: string;
  lastName: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public forecasts: UserName[] = [];
  public submitForm: FormGroup;
  public submitMessage: string = '';
  constructor(private http: HttpClient,
    private fb: FormBuilder,
    private submitService: UserSubmitService) {
    this.submitForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required]
    });
  }

  ngOnInit() {
  }

  onSubmit() {
    if (this.submitForm.valid) {
      this.submitService.submitUser(this.submitForm.value)
        .subscribe({
          next: (response) => {
            this.submitMessage = 'Submit successful!';
            this.submitForm.reset();
          },
          error: (error) => {
            this.submitMessage = 'Submit failed.';
          }
        });
    }
  }

  //getForecasts() {
  //  this.http.get<WeatherForecast[]>('/weatherforecast').subscribe(
  //    (result) => {
  //      this.forecasts = result;
  //    },
  //    (error) => {
  //      console.error(error);
  //    }
  //  );
  //}

  title = 'angulardotnetapp.client';
}
