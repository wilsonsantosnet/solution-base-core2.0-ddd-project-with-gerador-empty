import { Router } from "@angular/router";
import { ViewModel } from "../model/viewmodel";
import { retry } from "rxjs/operators";

export class ComponentBase {
  _showContainerCreate: Boolean;
  _showContainerEdit: Boolean;
  _showContainerDetails: Boolean;
  _showContainerFilters: Boolean;
  _showContainerImport: Boolean;
  
  _showBtnBack: Boolean;
  _showBtnFilter: Boolean;
  _showBtnNew: Boolean;
  _showBtnEdit: Boolean;
  _showBtnDetails: Boolean;
  _showBtnPrint: Boolean;
  _showBtnDelete: Boolean;

  _showClassName: Boolean;
  _classNames: string;

  constructor() {

    this.hideComponents();

    this._showBtnBack = true;
    this._showBtnFilter = true;
    this._showBtnNew = true;
    this._showBtnDetails = true;
    this._showBtnEdit = true;
    this._showBtnPrint = true;
    this._showBtnDelete = true;
    this._showClassName = true;
    this._classNames = 'framework-custom';

  }

  hideComponents(): void {
    this._showContainerCreate = false;
    this._showContainerEdit = false;
    this._showContainerDetails = false;
    this._showContainerImport = false;
  }

  hideContainerCreate() {
    this._showContainerCreate = false;
  }

  hideContainerEdit() {
    this._showContainerEdit = false;
  }

  showContainerCreate() {
    this._showContainerCreate = true;
  }

  showContainerEdit() {
    this._showContainerEdit = true;
  }

  showContainerDetails() {
    this._showContainerDetails = true;
  }

  showContainerFilters() {
    this._showContainerFilters = true;
  }

  showContainerImport() {
    this._showContainerImport = true;
  }

  navigateStrategy(vm: any, modal: any, router: Router, url: string) {
    if (vm.navigationModal)
      modal.show();
    else
      router.navigate([url])
  }

}
