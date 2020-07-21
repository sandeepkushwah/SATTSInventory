import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from '../../../../node_modules/rxjs';
import Beverage from '../models/beverage';

@Injectable()
export default class BeverageStoreService {
  public API = 'http://127.0.0.1:43593/api/Beverages';
  public BeverageInventory_API = `${this.API}`;
  constructor(private http: HttpClient) { }
  getAll(): Observable<Array<Beverage>> {
    return this.http.get<Array<Beverage>>(this.BeverageInventory_API);
  }
  get(id: number) {
    return this.http.get(`${this.BeverageInventory_API}/${id}`);
  }
  save(beverage: Beverage): Observable<Beverage> {
    let result: Observable<Beverage>;
    if (beverage.Id) {
      result = this.http.put<Beverage>(`${this.BeverageInventory_API}/${beverage.Id}`, beverage);
    }
    else {
      result = this.http.post<Beverage>(this.BeverageInventory_API, beverage);
    }
    return result;
  }
  remove(id: number) {
    return this.http.delete(`${this.BeverageInventory_API}/${id.toString()}`);
  }
}
