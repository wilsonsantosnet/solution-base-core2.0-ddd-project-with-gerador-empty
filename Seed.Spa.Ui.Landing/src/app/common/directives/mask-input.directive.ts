import { Directive, ElementRef, Renderer, Input, OnInit, HostListener } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';

@Directive({
  selector: '[maski]',
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: MaskInputDirective,
    multi: true
  }]
})
export class MaskInputDirective implements ControlValueAccessor {

  onTouched: any;
  onChange: any;

  @Input('maski') mask: string;

  constructor(private _elemetRef: ElementRef, private _renderer: Renderer) {

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

    if (this.mask == "")
      return;

    var valor = $event.target.value.replace(/\D/g, '');

    // retorna caso pressionado backspace
    if ($event.keyCode === 8) {
      this.onChange(valor);
      return;
    }

    var valorComMascara = this.aplicarMascara(valor);
    $event.target.value = valorComMascara;
    this.onChange(valorComMascara);
  }

  aplicarMascara(valor: any) {

    if (!this.mask)
      return;

    var pad = this.mask.toString().replace(/\D/g, '').replace(/9/g, '_');
    var valorMask = valor + pad.substring(0, pad.length - valor.length);

    if (valor.length <= pad.length) {
      this.onChange(valor);
    }

    var valorMaskPos = 0;
    valor = '';
    for (var i = 0; i < this.mask.length; i++) {
      if (isNaN(parseInt(this.mask.charAt(i)))) {
        valor += this.mask.charAt(i);
      } else {
        valor += valorMask[valorMaskPos++];
      }
    }

    if (valor.indexOf('_') > -1) {
      valor = valor.substr(0, valor.indexOf('_'));
    }

    return valor;
  }


  @HostListener('blur', ['$event'])
  onBlur($event: any) {

    if ($event.target.value.length === this.mask.length) {
      return;
    }

  }
  @HostListener('focus', ['$event'])
  onFocus($event: any) {

  }
}
