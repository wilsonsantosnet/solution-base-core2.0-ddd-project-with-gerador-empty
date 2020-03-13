import { Directive, ElementRef, Renderer, Input, OnInit, HostListener } from '@angular/core';
import { NgModel, NgControl } from '@angular/forms';

@Directive({
  selector: '[unmask]',
  providers: [NgModel]
})
export class UnMaskDirective {

  @Input("unmask") patternRemove: string;

  constructor(private elementRef: ElementRef, private model: NgControl, private ngModel: NgModel) {

  }

  @HostListener('input') inputChange() {

  
    let newValue = this.elementRef.nativeElement.value;

    if (this.patternRemove == "numeric") {
      newValue = newValue.replace(/\D/g, '')
    }

    this.model.control.setValue(newValue, {
      emitEvent: true,
      emitModelToViewChange: true,
      emitViewToModelChange: true
    });

    this.ngModel.viewToModelUpdate(newValue);
  }

}
