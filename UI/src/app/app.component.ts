import { Component } from '@angular/core';
import { NumberSymbol } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Expenses Tracker';

  transactions: Transaction[] = [
  {
    amount: 1,
    category: '',
    date: '',
    type: '',
    vendor: ''
  },
  {
    amount: 100,
    category: '',
    date: '',
    type: '',
    vendor: ''
  },
]

}

export class Transaction {
  public amount: Number;
  public type: string;
  public vendor: string;
  public category: string;
  public date: string;
}