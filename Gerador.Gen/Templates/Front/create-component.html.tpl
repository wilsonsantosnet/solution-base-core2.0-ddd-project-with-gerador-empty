<div class="col-lg-12">
  <div class="card gc-grid">
    <div class="card-header gc-grid__header">
      <h3><i class="fa fa-edit"></i> {{ vm.actionTitle }}</h3>
      <p>{{ vm.actionDescription }}</p>
    </div>
    <div class="card-block gc-grid__body">
      <form (ngSubmit)="onSave(vm.model)" novalidate>
        <app-<#classNameLowerAndSeparator#>-container-create [(vm)]="vm"></app-<#classNameLowerAndSeparator#>-container-create>
        <div class="modal-footer">
          <a href="javascript:history.back()" class="btn btn-default btn-default-app">
            <i class="fa fa-reply"></i> Voltar
          </a>
          <button type="submit" class="btn btn-success btn-success-app" [disabled]="vm != null && vm.form.invalid">Salvar</button>
        </div>
      </form>
    </div>
  </div>
</div>