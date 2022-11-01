import { Component, OnInit } from '@angular/core';
import {map, mergeAll, switchMap} from "rxjs";
import {ContractsService} from "../../../services/contracts.service";
import {BuyersService} from "../../../services/buyers.service";
import {SalesService} from "../../../services/sales.service";
import {ActivatedRoute} from "@angular/router";
import {ContractResponse} from "../../../dto/contracts/ContractResponse";

@Component({
  selector: 'app-edit-contract',
  templateUrl: './edit-contract.component.html',
  styleUrls: ['./edit-contract.component.css']
})
export class EditContractComponent implements OnInit {
  contract: ContractResponse;
  title: string;

  constructor(
    private contractService: ContractsService,
    private buyersService: BuyersService,
    private salesService: SalesService,
    private activatedRoute: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.activatedRoute.paramMap.pipe(
      switchMap(params => params.getAll('number'))
    )
      .pipe(
        map(number => this.contractService.getByNumber(+number)),
        mergeAll()
      )
      .subscribe(contract => {
        this.contract = contract

        this.title = `Контракт №${contract.number}`
      });
  }
}
