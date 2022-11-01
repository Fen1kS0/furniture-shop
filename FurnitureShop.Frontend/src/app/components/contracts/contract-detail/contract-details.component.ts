import {Component, Input, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {BehaviorSubject} from "rxjs";
import {ContractsService} from "../../../services/contracts.service";
import {ContractResponse} from "../../../dto/contracts/ContractResponse";
import {Buyer} from "../../../dto/buyers/Buyer";
import {BuyersService} from "../../../services/buyers.service";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {UpdateContractRequest} from "../../../dto/contracts/UpdateContractRequest";
import {ToastService} from "../../../services/toast.service";
import {ToastInfo} from "../../../dto/notifications/ToastInfo";
import {ToastType} from "../../../dto/notifications/ToastType";
import {SaleResponse} from "../../../dto/sales/SaleResponse";
import {SalesService} from "../../../services/sales.service";
import {Dialog} from "@angular/cdk/dialog";
import {AddSaleDialogComponent} from "../../dialogs/sale/add-sale-dialog/add-sale-dialog.component";
import {AddSaleRequest} from "../../../dto/sales/AddSaleRequest";
import {AddContractRequest} from "../../../dto/contracts/AddContractRequest";

@Component({
  selector: 'app-contract-details',
  templateUrl: './contract-details.component.html',
  styleUrls: ['./contract-details.component.css']
})
export class ContractDetailsComponent implements OnInit {
  private readonly emptyContractNumber: number = 0;

  @Input() contract: ContractResponse | undefined;
  @Input() title: string;

  buyers: Buyer[];
  selectedBuyer: Buyer;

  form: FormGroup;

  sales: SaleResponse[] = [];
  private deletedSales: SaleResponse[] = [];
  private addedSales: AddSaleRequest[] = [];

  constructor(
    private contractService: ContractsService,
    private buyersService: BuyersService,
    private salesService: SalesService,
    private activatedRoute: ActivatedRoute,
    private toastService: ToastService,
    private dialog: Dialog
  ) {

  }

  ngOnInit(): void {
    if (this.contract) {
      this.salesService.getSaleByContract(this.contract.number).subscribe(sales => {
        this.sales = sales;
      })

      this.buyersService.getByCode(this.contract.buyerCode).subscribe(buyer => {
        this.selectedBuyer = buyer
      })
    }

    this.buyersService.getAll().subscribe(buyers => {
      this.buyers = buyers;
    })

    this.initForm()
  }

  get issueDate() {
    return this.form.controls.issueDate as FormControl
  }

  get dueDate() {
    return this.form.controls.dueDate as FormControl
  }

  get contractNumber() {
    return this.contract ? this.contract.number : this.emptyContractNumber
  }

  get nameSaveButton() {
    return this.contract ? 'Сохранить' : 'Создать'
  }

  save(): void {
    if (this.contract) {
      this.updateContract()
    } else {
      this.addContract()
    }
  }

  selectBuyer(buyer: Buyer): void {
    this.selectedBuyer = buyer;
  }

  addSale() {
    const dialogRef = this.dialog.open<SaleResponse>(AddSaleDialogComponent, {
      data: {
        contractNumber: this.contractNumber
      }
    });

    dialogRef.closed.subscribe(saleResponse => {
      if (saleResponse) {
        this.addedSales.push(saleResponse)
        this.sales.push(saleResponse)
      }
    });
  }

  deleteSale(contractNumber: number, furnitureModel: number) {
    let sale = this.sales.find(sale => sale.contractNumber === contractNumber && sale.furnitureModel === furnitureModel)!
    let addedSale = this.addedSales.find(sale => sale.contractNumber === contractNumber && sale.furnitureModel === furnitureModel)

    if (addedSale) {
      this.addedSales = this.addedSales.filter(s => s !== addedSale)
    }
    else {
      this.deletedSales.push(sale)
    }

    this.sales = this.sales.filter(s => s !== sale)
  }

  private initForm(): void {
    let issueDate = this.contract ? this.contract.issueDate : new Date();
    let dueDate = this.contract ? this.contract.dueDate : new Date();

    this.form = new FormGroup({
      issueDate: new FormControl<Date>(issueDate, [
        Validators.required
      ]),
      dueDate: new FormControl<Date>(dueDate, [
        Validators.required
      ])
    })
  }

  private addContract() {
    let toast: ToastInfo = {
      header: 'Успешно!',
      body: `Контракт добавлен`,
      type: ToastType.Success
    }

    let addContractRequest: AddContractRequest = {
      issueDate: this.issueDate.value as Date,
      dueDate: this.dueDate.value as Date,
      buyerCode: this.selectedBuyer.code
    }

    this.contractService.add(addContractRequest).subscribe(contract => {
      this.setContractNumberInSales(contract.number)
      this.updateSales(toast);
    })
  }

  private updateContract() {
    let toast: ToastInfo = {
      header: 'Успешно!',
      body: `Контракт №${this.contractNumber} обновлён`,
      type: ToastType.Success
    }

    let updateContractRequest: UpdateContractRequest = {
      issueDate: this.issueDate.value as Date,
      dueDate: this.dueDate.value as Date,
      buyerCode: this.selectedBuyer.code
    }

    this.contractService.update(this.contractNumber, updateContractRequest).subscribe(() => {
      this.updateSales(toast);
    })
  }

  private updateSales(toast: ToastInfo): void {
    let saveProcessSales = new BehaviorSubject<number>(0);

    for (let addSaleRequest of this.addedSales) {
      this.salesService.add(addSaleRequest).subscribe(() => {
        saveProcessSales.next(saveProcessSales.value + 1)
      })
    }

    for (let deletedSale of this.deletedSales) {
      this.salesService.delete(deletedSale.contractNumber, deletedSale.furnitureModel).subscribe(() => {
        saveProcessSales.next(saveProcessSales.value + 1)
      })
    }

    saveProcessSales.subscribe(updatedSale => {
      if (updatedSale === this.addedSales.length + this.deletedSales.length)
        this.toastService.send(toast)
    })
  }

  private setContractNumberInSales(contractNumber: number) {
    for (let sale of this.addedSales) {
      sale.contractNumber = contractNumber
    }

    for (let sale of this.deletedSales) {
      sale.contractNumber = contractNumber
    }
  }
}
