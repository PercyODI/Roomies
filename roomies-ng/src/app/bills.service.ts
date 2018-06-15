import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Bill } from './models/bill';
import { Http } from '@angular/http';
import { Person } from './models/person';

@Injectable({
  providedIn: 'root'
})
export class BillsService {
  private peopleUrl = 'api/people';
  private billsUrl = 'api/bills';

  constructor(private http: HttpClient) { }

  getBills(): Observable<Bill[]> {
    return this.http.get<Bill[]>(this.billsUrl);
  }

  getBillsForPerson(person: Person): Observable<Bill[]> {
    return this.http.get<Bill[]>(this.peopleUrl + '/' + person.name + '/bills');
  }
}
