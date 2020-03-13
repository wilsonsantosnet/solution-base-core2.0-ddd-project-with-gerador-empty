import { Directive, ElementRef, Renderer, Input, Output, OnInit, OnChanges, EventEmitter, OnDestroy, Optional, Self } from '@angular/core';
import { NgModel, FormControlName } from '@angular/forms';

import { ApiService } from '../services/api.service';
import { GlobalService, NotificationParameters } from "../../global.service";
import { Subscription } from 'rxjs';

declare var $: any;

@Directive({
  selector: '[datasourceaux]',
  providers: [NgModel]
})

export class DataSourceAuxDirective implements OnInit, OnDestroy {

  @Input() disabledOnInit: boolean;
  @Input() labelInitial: string;
  @Input() dataitem: string;
  @Input() dataAux: any[];

  @Output() change: EventEmitter<any>;

  accessor: any;

  _notificationEmitter: Subscription;

  constructor(private _elemetRef: ElementRef, private _renderer: Renderer, private api: ApiService<any>, private ngModel: NgModel, @Optional() @Self() private controlName: FormControlName) {

    this.change = new EventEmitter<any>();
    this.labelInitial = "Selecione";

  }

  ngOnInit() {

    if (!this.disabledOnInit)
      this.datasource(this._elemetRef.nativeElement);

    this._notificationEmitter = GlobalService.notification.subscribe((not) => {

      if (not.event == "create" || not.event == "edit" || not.event == "init") {
        this.init();
      }

      if (not.event == "change") {
        if (not.data.dataitem == this.dataitem) {
          this.datasource(this._elemetRef.nativeElement, not.data.parentFilter);
        }
      }
    });
  }

 init() {
    $(this._elemetRef.nativeElement).val(null).trigger('change');
    setTimeout(() => {
      this.datasource(this._elemetRef.nativeElement);
    },250)
  }

  get control() {

    if (!this.controlName) {
      return null;
    }

    return this.controlName.control;
  }

  hasFormControl() {
    return this.controlName && this.controlName.control;
  }

  private datasource(el, parentFilter?: any) {

    el.options.length = 0;
    let selectedValue = null;

    if (this.ngModel.valueAccessor) {
      this.accessor = this.ngModel.valueAccessor;      
      if (this.accessor.value) {
        selectedValue = this.accessor.value;        
      }
    }

    if (!this.existsDefaultItem(el))
      this.addOption(el, undefined, this.labelInitial)

    this.select(el, selectedValue);

  }

  private select(el, selectedValue) {

    for (var i = 0; i < this.dataAux.length; i++) {
      this.addOption(el, this.dataAux[i].id, this.dataAux[i].name);
    }

    if (selectedValue)
      el.value = this.accessor.value;

  }

  private addOption(el, value, text) {

    if (this.existsItem(el, value))
      return;

    let option = document.createElement("option");
    option.text = text;
    option.value = value;
    el.add(option);
  }

  private existsItem(el, value) {

    let found = false;
    if (el.options) {
      for (var i = 0; i < el.options.length; i++) {
        if (el.options[i].value == value)
          found = true;
      }
    }
    return found;
  }

  private existsDefaultItem(el) {

    let found = false;
    if (el.options) {
      for (var i = 0; i < el.options.length; i++) {
        if (el.options[i].text == this.labelInitial)
          found = true;
      }
    }
    return found;
  }

  ngOnDestroy() {

    if (this._notificationEmitter)
      this._notificationEmitter.unsubscribe();
  }

}
