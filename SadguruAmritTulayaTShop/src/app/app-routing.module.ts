import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {BeverageInventoryListComponent} from './beverage-inventory-list/beverage-inventory-list.component';
import {BeverageInventoryEditComponent} from './beverage-inventory-edit/beverage-inventory-edit.component';

const appRoutes: Routes = [
  { path: '', redirectTo: '/beverage-inventory-list', pathMatch: 'full' },
  {
    path: 'beverage-inventory-list',
    component: BeverageInventoryListComponent
  },
  {
    path: 'beverage-inventory-edit/:id',
    component: BeverageInventoryEditComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
