import {Component, Input} from '@angular/core';
import {ContractResponse} from "../../../dto/contracts/ContractResponse";

@Component({
  selector: 'app-contract-card',
  templateUrl: './contract-card.component.html',
  styleUrls: ['./contract-card.component.css']
})
export class ContractCardComponent {

  @Input() contract: ContractResponse;
  @Input() deleteCallback: (contractNumber: number) => void;
  @Input() detailsCallback: (contractNumber: number) => void;

  constructor() { }
}
