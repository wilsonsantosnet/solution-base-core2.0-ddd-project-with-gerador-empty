import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges, SecurityContext } from '@angular/core';
import { DatePipe, DecimalPipe, PercentPipe, CurrencyPipe } from "@angular/common";
import { DomSanitizer } from '@angular/platform-browser';
import { ApiService } from "../../common/services/api.service";
import { MaskFormatPipe } from "../pipes/mask.pipe";

declare var $: any;

@Component({
  selector: 'bind-custom',
  template: `
      <span *ngIf="tag === 'span'" [ngClass]="{'badge-success': badge && model, 'badge-danger': badge && !model, 'badge': badge }">{{ value }}</span>
      <label *ngIf="tag === 'label'">{{ value }}</label>
      <p *ngIf="tag === 'p'">{{ value }}</p>
      <div *ngIf="tag === 'div'">{{ value }}</div>
      <div *ngIf="tag === 'inner'" [innerHTML]='value' style='display: inherit;'></div>
    `,
  providers: [DatePipe, DecimalPipe, PercentPipe, CurrencyPipe, MaskFormatPipe, ApiService],
})
export class BindCustomComponent implements OnInit, OnChanges {

  value: any;
  badge: boolean;

  @Input() model: any;
  @Input() format: string;
  @Input() tag: string;
  @Input() instance: string;
  @Input() endpoint: string;
  @Input() key: string;
  @Input() aux: any;
  @Input() mask: any;

  datePipe: DatePipe;

  constructor(
    private decimalPipe: DecimalPipe,
    private percentPipe: PercentPipe,
    private currencyPipe: CurrencyPipe,
    private maskPipe: MaskFormatPipe,
    private sanitizer: DomSanitizer,
    private api: ApiService<any>) {

    this.datePipe = new DatePipe("pt-BR");
  }

  ngOnInit(): void { }


  ngOnChanges(changes: SimpleChanges): void {

    if (this.format.toLocaleLowerCase() === 'date') {
      this.value = this.datePipe.transform(this.convertDate(this.model), 'dd/MM/yyyy');
    }
    else if (this.format.toLocaleLowerCase() === 'time') {
      this.value = this.datePipe.transform(this.convertDate(this.model), 'HH:mm');
    }
    else if (this.format.toLocaleLowerCase() === 'datetime' || this.format.toLocaleLowerCase() === 'datetime?') {
      this.value = this.datePipe.transform(this.convertDate(this.model), 'dd/MM/yyyy HH:mm');
    }
    else if ((this.format.toLocaleLowerCase() === 'integer' || this.format.toLocaleLowerCase() === 'int' || this.format.toLocaleLowerCase() === 'int?') && !isNaN(this.model)) {
      this.value = this.decimalPipe.transform(this.model, '1.0-0');
    }
    else if ((this.format.toLocaleLowerCase() === 'decimal' || this.format.toLocaleLowerCase() === 'decimal?' ) && !isNaN(this.model)) {
      this.value = this.decimalPipe.transform(this.model, '1.2-2');
    }
    else if (this.format.toLocaleLowerCase() === 'percent' && !isNaN(this.model)) {
      this.value = this.percentPipe.transform(this.model, '1.2-2');
    }
    else if (this.format.toLocaleLowerCase() === 'currency' && !isNaN(this.model)) {
      this.value = this.currencyPipe.transform(this.model, 'BRL', true, '1.2-2');
    }
    else if (this.format.toLocaleLowerCase() === 'instance') {
      this._getInstance();
    }
    else if (this.format.toLocaleLowerCase() === 'dataitem') {
      this.value = this._getInDataItem(this.model, this.aux);
    }
    else if (this.format.toLocaleLowerCase() === 'changevalue') {
      this.value = this._getChangeForThis(this.model, this.aux);
    }
    else if (this.format.toLocaleLowerCase() === 'bool' || this.format.toLocaleLowerCase() === 'bool?') {
      this.value = (this.model == true ? "Sim" : "Não"); this.badge = true;
    }
    else if (this.format.toLocaleLowerCase() === 'html') {
      this.tag = "inner";
      this.value = this.sanitizer.bypassSecurityTrustHtml(this.model)
    }
    else if (this.format.toLocaleLowerCase() === 'tag') {
      if (this.model) {
        var itens = this.model.split(',');
        var content = "<ul>";
        for (var i in itens) {
          content += "<li><div class='badge badge-default'>" + itens[i] + "</div></li>";
        }
        content += "</ul>";
        this.tag = "inner";
        this.value = this.sanitizer.bypassSecurityTrustHtml(content)
      }
    }
    else if (this.format.toLocaleLowerCase() === 'mask' && this.model) {
      this.value = this.maskPipe.transform(this.model, this.mask);
    }
    else if (this.format.toLocaleLowerCase() === 'color-legend') {
      this.tag = "inner";
      let hex = String(this.model || '').replace('#', '');
      var content = '<div class="badge status-circle color-change new-color-' + hex + '"><span class="invisible">a<span></div>';
      this._treatStatus(this.model);
      this.value = content;
    }
    else {
      this.value = this.model;
    }

  }


  public convertDate(value: any) {
    if (value) {
      var datePart = value.toString().split("/");
      var convertedDate = datePart[1] + "/" + datePart[0] + "/" + datePart[2];
      return new Date(convertedDate);
    }
  }

  private _getInDataItem(model: any, dataitem: any) {


    if (dataitem) {
      var result = dataitem.filter(function (item: any) {
        return model == item.id;
      });
      return result.length > 0 ? result[0].name : "--";
    }
    return "--";
  }

  private _getChangeForThis(model: any, newValue: any) {
    return newValue;
  }

  private _getInstance() {

    if (!this.instance || !this.model) {
      return;
    }

    let filters: any = [];
    filters[this.key] = this.model

    this.api.setResource(this.instance, this.endpoint).getDataitem(filters).subscribe(data => {
      this.tag = "inner";
      this.value = "--";
      if (data.dataList.length > 0)
        this.value = this.sanitizer.sanitize(SecurityContext.URL, "<a href=\"" + this.instance.toLowerCase() + "\/details/" + data.dataList[0].id + "\">" + data.dataList[0].name + "</a>");
    });

  }

  private _treatStatus(data: any) {
    if (this.model != null) {
      let valor = $(".color-change");
      valor.each(function () {
        var pos = String($(this).attr('class')).indexOf('new-color-');
        $(this).css('color', '#' + String($(this).attr('class')).substr(pos + 10, 6));
        $(this).css('background-color', '#' + String($(this).attr('class')).substr(pos + 10, 6));
      });
      let color = valor.innerHTML;
    }
  }

}
