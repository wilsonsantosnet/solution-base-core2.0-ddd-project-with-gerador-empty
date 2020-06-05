<!--EXT-->

<section class="container-fluid">
    <section class="card">
      <header class="card-header">
        <div class="row align-items-center">
          <div class="col">
            <span class="fa fa-edit hidden-xs" aria-hidden="true"></span> {{vm.generalInfo | traduction:vm.actionTitle}}<br>
            <small class="text-muted">{{ vm.actionDescription }}</small>
          </div>
          <div class="col text-right">
            <a *ngIf="_showBtnBack" class="btn py-0 hidden-xs hidden-sm" href="javascript:history.back()" title="{{vm.generalInfo | traduction:'voltar'}}">
              <span class="fa fa-arrow-left" aria-hidden="true"></span> {{vm.generalInfo | traduction:'voltar'}}
            </a>
            <a *ngIf='_showBtnNew && vm | isAuth:"CanWrite"' class="btn py-0" (click)="onCreate()" title="{{vm.generalInfo | traduction:'novoItem'}}">
              <span class="fa fa-plus" aria-hidden="true"></span> {{vm.generalInfo | traduction:'novoItem'}}
            </a>
          </div>
        </div>
      </header>
      <article class="card-body">
        <make-grid [(vm)]="vm" (edit)="onEdit($event)" (details)="onDetails($event)" (print)="onPrint($event)" (deleteConfimation)="onDeleteConfimation($event)" (orderBy)="onOrderBy($event)"  (filter)="onFilter($event)" [showFilters]='true' [showPrint]='_showBtnPrint && vm | isAuth:"CanReadOne"' [showDelete]='_showBtnDelete && vm | isAuth:"CanDelete"' [showDetails]='_showBtnDetails && vm | isAuth:"CanReadOne"' [showEdit]='_showBtnEdit && vm | isAuth:"CanReadOne"'></make-grid>
      </article>
      <footer class="card-footer">
        <make-pagination [(vm)]="vm" (pageChanged)="onPageChanged($event)"></make-pagination>
      </footer>
    </section>
</section>


<div bsModal [config]="{backdrop: 'static'}" #saveModal="bs-modal" class="modal fade">
  <div class="modal-dialog" [ngClass]="{'modal-lg': !isParent}">
    <div class="modal-content">
      <div class="modal-header">
        <h4 class="modal-title pull-left">Manutenção de {{vm.generalInfo | traduction:vm.actionTitle}}</h4>
        <button type="button" class="close pull-right" aria-label="Close" (click)="onCancel()">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <form (ngSubmit)="onSave(vm.model)" novalidate>
        <div class="modal-body pre-scrollable">
          <app-<#classNameLowerAndSeparator#>-container-create [(vm)]="vm" *ngIf="_showContainerCreate"></app-<#classNameLowerAndSeparator#>-container-create>
        </div>
        <div class="modal-footer">
          <div class="form-check form-check-inline mr-1">
            <input type='checkbox' [(ngModel)]='vm.manterTelaAberta' name='manterTelaAberta' class="form-check-input" />
            <label class="form-check-label" for="inline-checkbox1">Manter Aberta?</label>
          </div>
          <button class="btn btn-default" type="button" (click)="onCancel()">
			<i class="icon-close icons"></i>
			{{vm.generalInfo| traduction:'cancelar'}}
		  </button>
          <button type="submit" class="btn btn-success " [disabled]="vm != null && vm.form.invalid">
			<i class="icon-check icons"></i>
			{{vm.generalInfo | traduction:'salvar'}}
		  </button>
        </div>
      </form>
    </div>
  </div>
</div>

<div bsModal [config]="{backdrop: 'static'}" #editModal="bs-modal" class="modal fade">
  <div class="modal-dialog" [ngClass]="{'modal-lg': !isParent}">
    <div class="modal-content">
      <div class="modal-header">
        <h4 class="modal-title pull-left">Manutenção de {{vm.generalInfo | traduction:vm.actionTitle}} </h4>
        <button type="button" class="close pull-right" aria-label="Close" (click)="onCancel()">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <form (ngSubmit)="onSave(vm.model)" novalidate>
        <div class="modal-body pre-scrollable">
          <app-<#classNameLowerAndSeparator#>-container-edit [(vm)]="vm" *ngIf="_showContainerEdit"></app-<#classNameLowerAndSeparator#>-container-edit>
        </div>
        <div class="modal-footer">
          <div class="form-check form-check-inline mr-1">
            <input type='checkbox' [(ngModel)]='vm.manterTelaAberta' name='manterTelaAberta' class="form-check-input" />
            <label class="form-check-label" for="inline-checkbox1">Manter Aberta?</label>
          </div>
		  <button class="btn btn-default" type="button" (click)="onCancel()">
			<i class="icon-close icons"></i>
			{{vm.generalInfo| traduction:'cancelar'}}
		  </button>
          <button type="submit" class="btn btn-success " [disabled]="vm != null && vm.form.invalid">
			<i class="icon-check icons"></i>
			{{vm.generalInfo | traduction:'salvar'}}
		  </button>        
		 </div>
      </form>
    </div>
  </div>
</div>
