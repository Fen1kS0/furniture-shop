import {Component, Inject, OnInit} from '@angular/core';
import {DIALOG_DATA, DialogRef} from "@angular/cdk/dialog";
import {FurnitureService} from "../../../../services/furniture.service";
import {Furniture} from "../../../../dto/furniture/Furniture";
import {SaleResponse} from "../../../../dto/sales/SaleResponse";

@Component({
  selector: 'app-add-sale-dialog',
  templateUrl: './add-sale-dialog.component.html',
  styleUrls: ['./add-sale-dialog.component.css']
})
export class AddSaleDialogComponent implements OnInit {

  furnitureList: Furniture[];
  selectedFurniture: Furniture;
  count: number;

  constructor(
    public dialogRef: DialogRef<SaleResponse>,
    @Inject(DIALOG_DATA) public data: { contractNumber: number },
    private furnitureService: FurnitureService
  ) {
  }

  ngOnInit(): void {
    this.furnitureService.getAll().subscribe(furniture => {
      this.furnitureList = furniture
    })
  }

  add() {
    let saleResponse: SaleResponse = {
      contractNumber: this.data.contractNumber,
      furnitureModel: this.selectedFurniture.model,
      furnitureName: this.selectedFurniture.name,
      count: this.count
    }

    this.dialogRef.close(saleResponse)
  }

  cancel() {
    this.dialogRef.close()
  }


  selectFurniture(furniture: Furniture) {
    this.selectedFurniture = furniture
  }
}
