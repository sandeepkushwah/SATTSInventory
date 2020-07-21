import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule, HttpClient} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';  
import { BeverageInventoryListComponent } from './beverage-inventory-list/beverage-inventory-list.component';
import { BeverageInventoryEditComponent } from './beverage-inventory-edit/beverage-inventory-edit.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import BeverageStoreService from './shared/api/beverage-store.service';



@NgModule({
  declarations: [
    AppComponent,
    BeverageInventoryListComponent,
    BeverageInventoryEditComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule
  ],
  providers: [BeverageStoreService],
  bootstrap: [AppComponent]
})
export class AppModule { }
