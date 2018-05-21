<div class="card">
    <div class="card-header">
      <h3><i class="fa fa-edit"></i> {{ vm.actionTitle }}</h3>
      <p>{{ vm.actionDescription }}</p>
    </div>
	<div class="card-block">
		<app-<#classNameLowerAndSeparator#>-container-details [(vm)]="vm"></app-<#classNameLowerAndSeparator#>-container-details>
	</div>
	<div class="card-footer">
		<a href="javascript:history.back()" class="btn btn-sm btn-outline-secondary mr-auto p-2">
		<span class="fa fa-arrow-left" aria-hidden="true"></span> Voltar
		</a>
		<button class="btn btn-success btn-success-app" type="button" (click)="onPrint()"><i class="fa fa-print"></i> Imprimir</button>
	</div>
</div>

