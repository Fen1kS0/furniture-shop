import {Component, OnInit, ViewChild} from '@angular/core';
import {BuyersService} from "../../../services/buyers.service";
import {MatTableDataSource} from "@angular/material/table";
import {Buyer} from "../../../dto/buyers/Buyer";
import {MatSort} from "@angular/material/sort";
import {CdkTable} from "@angular/cdk/table";
import {BuyerRequest} from "../../../dto/buyers/BuyerRequest";
import {DialogBuyerComponent} from "../../dialogs/buyer/dialog-buyer/dialog-buyer.component";
import {ConfirmDialogComponent} from "../../dialogs/common/confirm-dialog/confirm-dialog.component";
import {Dialog} from "@angular/cdk/dialog";
import {MatDialog} from "@angular/material/dialog";

@Component({
  selector: 'app-buyers-list',
  templateUrl: './buyers-list.component.html',
  styleUrls: ['./buyers-list.component.css']
})
export class BuyersListComponent implements OnInit {
  public dataSource = new MatTableDataSource<Buyer>();
  displayedColumns = ['code', 'name', 'address', 'numberPhone', 'actions'];
  loading = false;

  @ViewChild(CdkTable) table: CdkTable<any>;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    public buyerService: BuyersService,
    public dialog: Dialog,
    public matDialog: MatDialog
  ) {
  }

  ngOnInit(): void {
    this.loadBuyers();
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }

  loadBuyers(): void {
    this.loading = true
    this.buyerService.getAll().subscribe(next => {
      this.loading = false
      this.dataSource.data = next
    })
  }

  addNew(): void {
    const dialogRef = this.matDialog.open(DialogBuyerComponent, {
      data: {
        submitLabel: 'Добавить',
        submitCallback: (buyer: BuyerRequest) => {
          this.buyerService.add(buyer).subscribe(next => {
            dialogRef.close(next);
          })
        }
      }
    });

    dialogRef.afterClosed().subscribe(() => {
        this.loadBuyers()
    });
  }

  startEdit(buyer: Buyer): void {
    const dialogRef = this.matDialog.open(DialogBuyerComponent, {
      data: {
        buyer: buyer,
        submitLabel: 'Сохранить',
        submitCallback: (buyerRequest: BuyerRequest) => {
          this.buyerService.update(buyer.code, buyerRequest).subscribe(next => {
            dialogRef.close(next);
          })
        }
      }
    });

    dialogRef.afterClosed().subscribe(() => {
        this.loadBuyers()
    });
  }

  deleteItem(buyer: Buyer): void {
    let buyerInfo = `Покупатель: ${buyer.name}`;
    const dialogRef = this.dialog.open<boolean>(ConfirmDialogComponent, {
      data: {
        title: 'Удалить запить?',
        body: buyerInfo
      }
    });

    dialogRef.closed.subscribe(isConfirm => {
      if (isConfirm) {
        this.buyerService.delete(buyer.code).subscribe(() => {
          this.loadBuyers()
        })
      }
    });
  }
}
