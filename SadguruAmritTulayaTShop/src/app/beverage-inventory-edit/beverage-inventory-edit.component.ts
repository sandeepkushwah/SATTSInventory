import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';

import BeverageStoreService from '../shared/api/beverage-store.service';
import Beverage from '../shared/models/beverage';


@Component({
  selector: 'app-beverage-inventory-edit',
  templateUrl: './beverage-inventory-edit.component.html',
  styleUrls: ['./beverage-inventory-edit.component.css']
})
export class BeverageInventoryEditComponent implements OnInit {


  beverage: Beverage = new Beverage();
  sub: Subscription;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private beverageStoreService: BeverageStoreService) { }


    ngOnInit() {
      this.sub = this.route.params.subscribe(params => {
        const id = params['id'];
        if (id) {
          this.beverageStoreService.get(id).subscribe((beverage: any) => {
            if (beverage) {
              this.beverage = beverage;
            } else {
              console.log(
                `Tea with id '${id}' not found, returning to list`
              );
              this.gotoList();
            }
          });
        }
      });
    }

    ngOnDestroy() {
      this.sub.unsubscribe();
    }
    
  gotoList() {
    this.router.navigate(['/beverage-inventory-list']);
  }


  save(form: any) {
    this.beverageStoreService.save(form).subscribe(
      result => {
        this.gotoList();
      },
      error => console.error(error)
    );
  }
}
