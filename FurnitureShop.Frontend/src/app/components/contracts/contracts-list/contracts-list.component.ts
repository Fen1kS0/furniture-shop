import {Component, OnInit} from '@angular/core';
import {ContractResponse} from "../../../dto/contracts/ContractResponse";
import {ContractsService} from "../../../services/contracts.service";
import {Router} from "@angular/router";
import {Observable} from "rxjs";
import {ConfirmDialogComponent} from "../../dialogs/common/confirm-dialog/confirm-dialog.component";
import {Dialog} from "@angular/cdk/dialog";

@Component({
  selector: 'app-contracts-list',
  templateUrl: './contracts-list.component.html',
  styleUrls: ['./contracts-list.component.css']
})
export class ContractsListComponent implements OnInit {

  contracts$: Observable<ContractResponse[]>

  constructor(
    public contractService: ContractsService,
    public dialog: Dialog,
    public router: Router
  ) {
  }

  ngOnInit(): void {
    this.contracts$ = this.contractService.getAll()
  }

  trackByContractNumber(index: number, contract: ContractResponse): number {
    return contract.number;
  }

  showDetails(contractNumber: number): void {
    this.router.navigate(
      ['/contracts', contractNumber]
    );
  }

  deleteContract(contractNumber: number): void {
    let contractInfo = `Номер контракта: ${contractNumber}`;
    const dialogRef = this.dialog.open<boolean>(ConfirmDialogComponent, {
      data: {
        title: 'Удалить запить?',
        body: contractInfo
      }
    });

    dialogRef.closed.subscribe(isConfirm => {
      if (isConfirm) {
        this.contractService.delete(contractNumber).subscribe(() => {
          this.contracts$ = this.contractService.getAll()
        })
      }
    });
  }
}
