import { Component, ElementRef, Input } from '@angular/core';

declare var $: any;

@Component({
    selector: 'tree-view',
    template: `
  <ul>
    <li *ngFor="let node of treeData ; let i = index" draggable="true" (dragstart)="drag($event)" (drop)="drop($event)" (dragover)="allowDrop($event)" [id]="node.id">
      {{node.name}}
      <tree-view *ngIf="node.children" [treeData]="node.children" [nodeParent]="node"></tree-view>
    </li>
  </ul>
  `
})
export class TreeViewComponent {

    @Input() treeData: any[];
    @Input() nodeParent: any;

    constructor(private _elemetRef: ElementRef) {

    }

    allowDrop(ev: any) {
        ev.preventDefault();
    }

    drag(ev: any) {
        ev.dataTransfer.setData("id-tree", ev.target.id);
    }

    drop(ev: any) {

        ev.preventDefault();
        var data = ev.dataTransfer.getData("id-tree");
        ev.target.appendChild(document.getElementById(data));
    }


}