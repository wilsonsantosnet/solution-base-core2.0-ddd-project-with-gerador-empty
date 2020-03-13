import { Directive, ElementRef, Renderer, Input, OnInit, HostListener } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';

declare var $: any;

@Directive({
  selector: '[maskm]',
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: MaskMoneyDirective,
    multi: true
  }]
})
export class MaskMoneyDirective implements ControlValueAccessor, OnInit {

  onTouched: any;
  onChange: any;

  constructor(private _elemetRef: ElementRef, private _renderer: Renderer) {

  }

  ngOnInit() {

    $(document).ready(() => {
      $(this._elemetRef.nativeElement).maskMoney({ prefix: 'R$ ', allowNegative: true, thousands: '.', decimal: ',', affixesStay: true });
    });

  }

  writeValue(value: any): void {

    this._elemetRef.nativeElement.value = null;
    if (value) {

      this._elemetRef.nativeElement.value = value;
    }
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  @HostListener('keyup', ['$event'])
  onKeyup($event: any) {
    var newValue = $event.target.value;

    newValue = newValue.replace(".", "");
    newValue = newValue.replace("R$", "");
    newValue = newValue.replace(",", ".");

    this.onChange(newValue);
  }

  @HostListener('blur', ['$event'])
  onBlur($event: any) {

   

  }
  @HostListener('focus', ['$event'])
  onFocus($event: any) {

  }
}
