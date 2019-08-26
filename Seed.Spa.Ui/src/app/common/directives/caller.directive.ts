import { Directive, ElementRef, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgModel } from '@angular/forms';

@Directive({
    selector: '[caller]',
    providers: [NgModel]
})

export class CallerDiretive implements OnInit {

    @Output() caller: EventEmitter<any> = new EventEmitter<any>();
    constructor() {
    }

    ngOnInit() {
        this.caller.emit();
    }

}