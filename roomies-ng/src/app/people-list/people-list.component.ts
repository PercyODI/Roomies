import { Component, OnInit } from '@angular/core';
import { PeopleService } from '../people.service';
import { Person } from '../models/person';

@Component({
  selector: 'app-people-list',
  templateUrl: './people-list.component.html',
  styleUrls: ['./people-list.component.css']
})
export class PeopleListComponent implements OnInit {
  people: Person[];

  constructor(private peopleService: PeopleService) { }

  ngOnInit() {
    this.peopleService.getPeople()
      .subscribe(people => this.people = people);
  }

}
