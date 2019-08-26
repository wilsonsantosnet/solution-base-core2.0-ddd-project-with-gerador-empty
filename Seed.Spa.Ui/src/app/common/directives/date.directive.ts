import { Directive, ElementRef, Input, Output, EventEmitter, Optional, Self } from '@angular/core';

import { NgModel, FormControlName } from '@angular/forms';


declare var $: any;


@Directive({
    selector: '[datepicker]',
    providers: [NgModel]
})
export class DateDirective {

    @Input() saUiDateTimePicker: any; //configuração do plugin
    @Output() change = new EventEmitter();

    constructor(private el: ElementRef, private ngModel: NgModel, @Optional() @Self() private controlName: FormControlName) {
        this.render();
    }

    render() {
        let element = $(this.el.nativeElement);

        //iniciando plugin
        $.datetimepicker.setLocale('pt-BR'); //idioma plugin
        let options = $.extend(this.saUiDateTimePicker, {
            mask: '39/19/2999',
            format: 'd/m/Y',
            timepicker: false,
            todayButton: true,
            defaultSelect: true,
            step: 30
        });

        element.datetimepicker(options);
        this.change.emit(); //necessário para emitir o evento change

        let ultimoValor = '';
        $(element).on('change', (ret: any) => {
            let valor = $(element).val();

            if (valor != ultimoValor) {

                this.updateValue(valor, ultimoValor);
                ultimoValor = valor;
            }
        });
    }



    private updateValue(value: any, valueold: any) {

        if (this.ngModel) {
            this.ngModel.viewToModelUpdate(value);

            if (value != valueold) {
                this.ngModel.control.markAsDirty();
            }
        }

        if (this.hasFormControl()) {
            this.control.setValue(value);

            if (value != valueold) {
                this.control.markAsDirty();
            }
        }

        this.change.emit(); //necessário para emitir o evento change
    }

    private hasFormControl() { return this.controlName && this.controlName.control; }

    get control() {

        if (!this.controlName) {
            return null;
        }

        return this.controlName.control;
    }

}
