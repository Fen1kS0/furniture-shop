import {Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {CdkTable} from "@angular/cdk/table";
import {MatSort} from "@angular/material/sort";
import {Dialog} from "@angular/cdk/dialog";
import {MatDialog} from "@angular/material/dialog";
import {ConfirmDialogComponent} from "../../dialogs/common/confirm-dialog/confirm-dialog.component";
import {Furniture} from "../../../dto/furniture/Furniture";
import {FurnitureService} from "../../../services/furniture.service";
import {FurnitureRequest} from "../../../dto/furniture/FurnitureRequest";
import {DialogFurnitureComponent} from "../../dialogs/furniture/dialog-furniture/dialog-furniture.component";

@Component({
  selector: 'app-furniture-list',
  templateUrl: './furniture-list.component.html',
  styleUrls: ['./furniture-list.component.css']
})
export class FurnitureListComponent implements OnInit {

  public dataSource = new MatTableDataSource<Furniture>();
  displayedColumns = ['model', 'name', 'price', 'characteristics', 'actions'];
  loading = false;

  @ViewChild(CdkTable) table: CdkTable<any>;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    public furnitureService: FurnitureService,
    public dialog: Dialog,
    public matDialog: MatDialog,
  ) {
  }

  ngOnInit(): void {
    this.loadFurniture();
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }

  loadFurniture(): void {
    this.loading = true
    this.furnitureService.getAll().subscribe(next => {
      this.loading = false
      this.dataSource.data = next
    })
  }

  addNew(): void {
    const dialogRef = this.matDialog.open(DialogFurnitureComponent, {
      data: {
        submitLabel: 'Добавить',
        submitCallback: (furniture: FurnitureRequest) => {
          this.furnitureService.add(furniture).subscribe(next => {
            dialogRef.close(next);
          })
        }
      }
    });

    dialogRef.afterClosed().subscribe(() => {
      this.loadFurniture()
    });
  }

  startEdit(furniture: Furniture): void {
    const dialogRef = this.matDialog.open(DialogFurnitureComponent, {
      data: {
        furniture: furniture,
        submitLabel: 'Сохранить',
        submitCallback: (furnitureRequest: FurnitureRequest) => {
          this.furnitureService.update(furniture.model, furnitureRequest).subscribe(next => {
            dialogRef.close(next);
          })
        }
      }
    });

    dialogRef.afterClosed().subscribe(() => {
      this.loadFurniture()
    });
  }

  deleteItem(furniture: Furniture): void {
    let furnitureInfo = `Мебель: ${furniture.name}`;
    const dialogRef = this.dialog.open<boolean>(ConfirmDialogComponent, {
      data: {
        title: 'Удалить запить?',
        body: furnitureInfo
      }
    });

    dialogRef.closed.subscribe(isConfirm => {
      if (isConfirm) {
        this.furnitureService.delete(furniture.model).subscribe(() => {
          this.loadFurniture()
        })
      }
    });
  }
}
