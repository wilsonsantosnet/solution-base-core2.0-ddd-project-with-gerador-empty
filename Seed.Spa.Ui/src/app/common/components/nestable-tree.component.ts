import { Component, ElementRef, Input, OnInit, EventEmitter, Output, OnChanges, SimpleChanges } from '@angular/core';

declare var $: any;

@Component({
  selector: 'nestable-tree',
  template: `
    <div class="dd" [id]="id" [nestable]="data" (change)="onChangeNestabale($event)">
      <ol class="dd-list root" >
      </ol>
    </div>`
})
export class NestabaleTreeComponent implements OnInit, OnChanges {


  @Input() data: any[];
  @Input() id: any;
  @Output() change = new EventEmitter<any>();

  constructor(private el: ElementRef) {

  }

  ngOnInit() {
    
  }


  ngOnChanges(changes: SimpleChanges): void {
    
  }

  onChangeNestabale(e) {
    this.change.emit(e);
  }
}
