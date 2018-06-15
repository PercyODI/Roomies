import { Component, OnInit, Input } from '@angular/core';
import { BillsService } from '../bills.service';
import { Bill } from '../models/bill';
import { Person } from '../models/person';

@Component({
  selector: 'app-bill-list',
  templateUrl: './bill-list.component.html',
  styleUrls: ['./bill-list.component.css']
})
export class BillListComponent implements OnInit {
  @Input() person: Person;
  bills: Bill[];

  constructor(private billsService: BillsService) { }

  ngOnInit() {
    this.billsService.getBillsForPerson(this.person)
      .subscribe(bills => this.bills = bills);
  }

}
