<div class="col-lg-12">
  <div class="card gc-grid">
    <div class="card-header gc-grid__header">
      <h3><i class="fa fa-edit"></i> {{ vm.actionTitle }}</h3>
      <p>{{ vm.actionDescription }}</p>
    </div>
    <div class="card-block gc-grid__body">
		<app-<#classNameLowerAndSeparator#>-container-details [(vm)]="vm"></app-<#classNameLowerAndSeparator#>-container-details>
		<div class="modal-footer">
			<a href="javascript:history.back()" class="btn btn-primary btn-sm" >
				<i class="fa fa-reply"></i> Voltar
			</a>
		</div>
    </div>
  </div>
</div>


