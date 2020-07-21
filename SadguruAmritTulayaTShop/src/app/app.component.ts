import { Component, OnInit } from '@angular/core';
import BeverageStoreService from './shared/api/beverage-store.service';
import Beverage from './shared/models/beverage';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'SadguruAmritTulayaTShop';
  test = 'abc';

  beverages: Array<Beverage>;

  constructor(private beverageStoreService: BeverageStoreService) { }

  ngOnInit(): void {
    this.beverageStoreService.getAll().subscribe(data => 
      {
          this.beverages = data;
      });
  }
}
