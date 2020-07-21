import { Component, OnInit } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http'; 
import BeverageStoreService from '../shared/api/beverage-store.service';
import Beverage from '../shared/models/beverage';
import {BootStrap} from '../../../node_modules/bootstrap'

@Component({
  selector: 'app-beverage-inventory-list',
  templateUrl: './beverage-inventory-list.component.html',
  styleUrls: ['./beverage-inventory-list.component.css'
],
  providers: [
      BeverageStoreService
  ]
})
export class BeverageInventoryListComponent implements OnInit {

  
  beverage: Beverage = new Beverage();
  beverages: Array<Beverage>;

  constructor(private beverageStoreService: BeverageStoreService) { }


  ngOnInit(): void {
    this.beverageStoreService.getAll().subscribe(data => 
      {
          this.beverages = data;
      });
  }

refreshBeverages()
{
  this.beverageStoreService.getAll().subscribe(data => 
    {
        this.beverages = data;
    });
}

  remove(id: number) {
    this.beverageStoreService.remove(id).subscribe(
      result => {
        this.refreshBeverages();
      },
      error => console.error(error)
    );
    
  }

  
  save(form: any) {
    this.beverageStoreService.save(form).subscribe(
      result => {
        this.refreshBeverages();
      },
      error => console.error(error)
    );
  }
}
