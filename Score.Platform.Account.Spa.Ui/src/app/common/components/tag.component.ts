import { Component, OnInit, Input, forwardRef, Output, EventEmitter, OnDestroy } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';
import { GlobalService } from "../../global.service";
import { ViewModel } from '../model/viewmodel';
import { ServiceBase } from '../services/service.base';


@Component({
  selector: 'tag-custom',
  template: `<tag-input [(ngModel)]='value'  (ngModelChange)="onModelChange($event)" [placeholder]="placeholder" [secondaryPlaceholder]="secondaryPlaceholder" [disabled]="disabled"></tag-input>`,
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: TagCustomComponent,
    multi: true
  }]

})
export class TagCustomComponent implements ControlValueAccessor, OnDestroy {

  @Input() readOnly: boolean;
  @Input() model: any;
  @Output() tagChange = new EventEmitter<any>();
  @Input() ctrlName: string;
  @Input() ctrlNameItem: string;
  @Input() ctrlNameItemDisplay: string;

  onTouched: any;
  onChange: any;
  placeholder: string;
  secondaryPlaceholder: string;
  disabled: boolean;
  modelJson: any;

  constructor(private serviceBase: ServiceBase) {
    this.model = {};
    this.modelJson = null;

    if (this.readOnly) {
      this.placeholder = "";
      this.secondaryPlaceholder = "";
      this.disabled = true;
    }
  }

  //get accessor
  get value(): any {

    if (this.modelJson) {
      return this.modelJson;
    }

    if (this.ctrlName) {

      this.modelJson = this.serviceBase.tagTransformCollectionToShow({
        model: this.model[this.ctrlName],
        readOnly: this.readOnly,
        ctrlName: this.ctrlName,
        ctrlNameItem: this.ctrlNameItem,
        ctrlNameItemDisplay: this.ctrlNameItemDisplay
      });
      return this.modelJson;
    }

    this.modelJson = this.serviceBase.tagTransformToShow(this.model, this.readOnly);
    return this.modelJson;

  };

  //set accessor including call the onchange callback
  set value(v: any) {
    if (v !== this.model) {

      if (this.ctrlName) {
        this.model[this.ctrlName] = this.serviceBase.tagTransformCollectionToSave({
          model: v,
          readOnly: this.readOnly,
          ctrlName: this.ctrlName,
          ctrlNameItem: this.ctrlNameItem,
          ctrlNameItemDisplay: this.ctrlNameItemDisplay
        });
      }
      else {
        this.model = this.serviceBase.tagTransformToSave(v);
      }
      this.modelJson = null;
      this.onChange(v);
    }
  }

  onModelChange($event: any) {
    this.tagChange.emit(this.model)
  }

  //From ControlValueAccessor interface
  writeValue(value: any) {
    if (value !== this.model) {
      this.model = value;
    }
  }

  //From ControlValueAccessor interface
  registerOnChange(fn: any) {
    this.onChange = fn;
  }

  //From ControlValueAccessor interface
  registerOnTouched(fn: any) {
    this.onTouched = fn;
  }

  ngOnDestroy() {
    this.model = {};
  }
}
