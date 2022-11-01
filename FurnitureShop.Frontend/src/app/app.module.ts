import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http'

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {BuyersListComponent} from './components/tables/buyers-list/buyers-list.component';
import {MatTableModule} from "@angular/material/table";
import {MatSortModule} from "@angular/material/sort";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {CdkTableModule} from "@angular/cdk/table";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MatPaginatorModule} from "@angular/material/paginator";
import {MatToolbarModule} from "@angular/material/toolbar";
import {DialogBuyerComponent} from "./components/dialogs/buyer/dialog-buyer/dialog-buyer.component";
import {MatDialogModule} from "@angular/material/dialog";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";

import {MainNavigationComponent} from './components/navigation/navigation/main-navigation.component';
import {ConfirmDialogComponent} from './components/dialogs/common/confirm-dialog/confirm-dialog.component';
import {FurnitureListComponent} from "./components/tables/furniture-list/furniture-list.component";
import {DialogFurnitureComponent} from './components/dialogs/furniture/dialog-furniture/dialog-furniture.component';

import {LayoutModule} from '@angular/cdk/layout';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatListModule} from '@angular/material/list';
import {ContractsListComponent} from "./components/contracts/contracts-list/contracts-list.component";
import {ContractCardComponent} from './components/cards/contract-card/contract-card.component';
import {ContractDetailsComponent} from './components/contracts/contract-detail/contract-details.component';
import {MatDatepickerModule} from "@angular/material/datepicker";
import {MatNativeDateModule} from "@angular/material/core";
import { ToastComponent } from './components/notifications/toast/toast.component';
import { SaleCardComponent } from './components/cards/sale-card/sale-card.component';
import { AddSaleDialogComponent } from './components/dialogs/sale/add-sale-dialog/add-sale-dialog.component';
import { EditContractComponent } from './components/contracts/edit-contract/edit-contract.component';
import { AddContractComponent } from './components/contracts/add-contract/add-contract.component';

@NgModule({
  declarations: [
    AppComponent,
    BuyersListComponent,
    DialogBuyerComponent,
    FurnitureListComponent,
    MainNavigationComponent,
    ConfirmDialogComponent,
    DialogFurnitureComponent,
    ContractsListComponent,
    ContractCardComponent,
    ContractDetailsComponent,
    ToastComponent,
    SaleCardComponent,
    AddSaleDialogComponent,
    EditContractComponent,
    AddContractComponent
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        NgbModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        CdkTableModule,
        MatTableModule,
        MatSortModule,
        MatButtonModule,
        MatIconModule,
        MatButtonModule,
        MatIconModule,
        MatSortModule,
        MatTableModule,
        MatToolbarModule,
        MatPaginatorModule,
        MatDialogModule,
        MatFormFieldModule,
        MatInputModule,
        LayoutModule,
        MatSidenavModule,
        MatListModule,
        MatDatepickerModule,
        MatNativeDateModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
