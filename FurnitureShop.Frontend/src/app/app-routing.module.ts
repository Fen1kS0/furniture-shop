import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {BuyersListComponent} from "./components/tables/buyers-list/buyers-list.component";
import {FurnitureListComponent} from "./components/tables/furniture-list/furniture-list.component";
import {ContractsListComponent} from "./components/contracts/contracts-list/contracts-list.component";
import {AddContractComponent} from "./components/contracts/add-contract/add-contract.component";
import {EditContractComponent} from "./components/contracts/edit-contract/edit-contract.component";

const routes: Routes = [
  {path: '', redirectTo: '/contracts', pathMatch: 'full'},
  {path: 'furniture', component: FurnitureListComponent},
  {path: 'buyers', component: BuyersListComponent},
  {path: 'contracts', component: ContractsListComponent, pathMatch: 'full'},
  {path: 'contracts/create', component: AddContractComponent, pathMatch: 'full'},
  {path: 'contracts/:number', component: EditContractComponent, pathMatch: 'full'},
  {path: '**', redirectTo: '/contracts'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
