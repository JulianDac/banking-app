import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html'
})
export class CustomerComponent {
  public users: Customer[];

  constructor(http: HttpClient) {
    http.get<Customer[]>("http://localhost:5000/api/customer").subscribe(result => {
      this.users = result;
    }, error => console.error(error));
  }
}

interface Customer {
  customerID: number;
  name: string;
  tfn: number;
}
