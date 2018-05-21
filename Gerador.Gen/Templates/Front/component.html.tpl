<!--EXT-->
 <div class="gc-body__heading">
  <ol class="breadcrumb breadcrumb-app">
    <li class="breadcrumb-item"><a href="/">Home >> {{ vm.actionTitle }}</a></li>
  </ol>

  <div class="gc-heading__controls">
    <div class="container-fluid">
      <div class="row">
        <div class="col d-flex justify-content-end">
          <a href="javascript:history.back()" class="btn btn-sm btn-outline-secondary mr-auto p-2">
            <span class="fa fa-arrow-left" aria-hidden="true"></span> {{vm.generalInfo.voltar.label}}
          </a>
          <button type="button" class="btn btn-sm btn-primary btn-primary-app  p-2" (click)="onShowFilter()">
            <span class="fa fa-filter" aria-hidden="true"></span> {{vm.generalInfo.filtro.label}}
          </button>
          <button type="button" class="btn btn-sm btn-success btn-success-app p-2" (click)="onCreate()">
            <span class="fa fa-plus" aria-hidden="true"></span> {{vm.generalInfo.novoItem.label}}
          </button>
        </div>
      </div>
    </div>
  </div>
  <div class="container-fluid">
    <div class="row">
      <div class="col">
        <h2 class="title--main">
          <span class="fa fa-home" aria-hidden="true"></span> {{ vm.actionTitle }} <small>{{ vm.actionDescription }}</small>
        </h2>
      </div>
    </div>
  </div>
</div>

<div class="container-fluid">
  
  <div bsModal #filterModal="bs-modal" class="gc-modal modal fade">
		<div class="modal-dialog modal-lg">
		<form #formFilter="ngForm" (ngSubmit)="onFilter(vm.modelFilter)">
			<div class="modal-content">
				<div class="modal-header">
				{{vm.generalInfo.filtro.label}}
				<button type="button" class="close pull-right" aria-label="Close" (click)="onCancel()">
					<span aria-hidden="true">&times;</span>
				</button>
				</div>
				<div class="modal-body">
					<app-<#classNameLowerAndSeparator#>-container-filter [(vm)]="vm" *ngIf="_showContainerFilters"></app-<#classNameLowerAndSeparator#>-container-filter>
				</div>
				<div class="modal-footer">
					<button class="btn btn-primary btn-default-app" type="button" (click)="onCancel()">Fechar</button>
					<button class="btn btn-default btn-primary-app" type="button" (click)="onClearFilter()">Limpar</button>
					<button class="btn btn-success btn-success-app" type="submit">
						<span class="fa fa-search"></span>
						{{vm.generalInfo.filtrar.label}}
					</button>
				</div>
			</div>
		</form>
		</div>
  </div>

  <div class="row">
    <div class="col">
      <div class="card gc-grid">
        <div class="card-block gc-grid__body">
			<make-grid [(vm)]="vm" (edit)="onEdit($event)" (details)="onDetails($event)" (print)="onPrint($event)" (deleteConfimation)="onDeleteConfimation($event)" (orderBy)="onOrderBy($event)"></make-grid>
        </div>
        <div class="card-footer gc-grid__footer">
          <div class="gc-pagination gc-pagination-app">
            <make-pagination [(vm)]="vm" (pageChanged)="onPageChanged($event)"></make-pagination>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>

<div bsModal #saveModal="bs-modal" class="gc-modal modal fade">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header">
        <h4 class="modal-title pull-left">Manutenção de {{ vm.actionTitle }}</h4>
        <button type="button" class="close pull-right" aria-label="Close" (click)="onCancel()">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <form (ngSubmit)="onSave(vm.model)" novalidate>
        <div class="modal-body">
          <app-<#classNameLowerAndSeparator#>-container-create [(vm)]="vm" *ngIf="_showContainerCreate"></app-<#classNameLowerAndSeparator#>-container-create>
        </div>
        <div class="modal-footer">
          <button class="btn btn-default btn-default-app" type="button" (click)="onCancel()">{{vm.generalInfo.cancelar.label}}</button>
          <button type="submit" class="btn btn-success btn-success-app" [disabled]="vm != null && vm.form.invalid" >{{vm.generalInfo.salvar.label}}</button>
		  <label class="custom-control custom-checkbox">
            <input type='checkbox' [(ngModel)]='vm.manterTelaAberta' name='manterTelaAberta' class="custom-control-input" />
            <span class="custom-control-indicator"></span>
            <span class="custom-control-description">Manter Aberta?</span>
          </label>
        </div>
      </form>
    </div>
  </div>
</div>

<div bsModal #editModal="bs-modal" class="gc-modal modal fade">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header">
        <h4 class="modal-title pull-left">Manutenção de {{ vm.actionTitle }}</h4>
        <button type="button" class="close pull-right" aria-label="Close" (click)="onCancel()">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <form (ngSubmit)="onSave(vm.model)" novalidate>
		<div class="modal-body">
			<app-<#classNameLowerAndSeparator#>-container-edit [(vm)]="vm" *ngIf="_showContainerEdit"></app-<#classNameLowerAndSeparator#>-container-edit>
		</div>
        <div class="modal-footer">
          <button class="btn btn-default btn-default-app" type="button" (click)="onCancel()">{{vm.generalInfo.cancelar.label}}</button>
          <button type="submit" class="btn btn-success btn-success-app" [disabled]="vm != null && vm.form.invalid">{{vm.generalInfo.salvar.label}}</button>
		  <label class="custom-control custom-checkbox">
            <input type='checkbox' [(ngModel)]='vm.manterTelaAberta' name='manterTelaAberta' class="custom-control-input" />
            <span class="custom-control-indicator"></span>
            <span class="custom-control-description">Manter Aberta?</span>
          </label>
        </div>
      </form>
    </div>
  </div>
</div>

<div bsModal #detailsModal="bs-modal" class="gc-modal modal fade">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header">
        <h4 class="modal-title pull-left">Detalhes de {{ vm.actionTitle }}</h4>
        <button type="button" class="close pull-right" aria-label="Close" (click)="onCancel()">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
	  <div class="modal-body">
		<app-<#classNameLowerAndSeparator#>-container-details [(vm)]="vm" *ngIf="_showContainerDetails"></app-<#classNameLowerAndSeparator#>-container-details>
	  </div>
      <div class="modal-footer">
        <button class="btn btn-default btn-default-app" type="button" (click)="onCancel()">{{vm.generalInfo.cancelar.label}}</button>
      </div>
    </div>
  </div>
</div>
