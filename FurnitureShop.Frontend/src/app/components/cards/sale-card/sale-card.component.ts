import {Component, Input} from '@angular/core';
import {SaleResponse} from "../../../dto/sales/SaleResponse";

@Component({
  selector: 'app-sale-card',
  templateUrl: './sale-card.component.html',
  styleUrls: ['./sale-card.component.css']
})
export class SaleCardComponent {

  @Input() sale: SaleResponse;
  @Input() deleteCallback: (contractNumber: number, furnitureModel: number) => void;

  constructor() { }
}
