<div class="container-fluid">
    <div class="card">
        <div class="card-header">
          <h3><i class="fa fa-edit"></i> {{ vm.actionTitle }}</h3>
          <p>{{ vm.actionDescription }}</p>
        </div>
        <div class="card-body">
            <app-<#classNameLowerAndSeparator#>-container-details [(vm)]="vm"></app-<#classNameLowerAndSeparator#>-container-details>
        </div>
        <div class="card-footer d-flex justify-content-end">
             <a href="javascript:history.back()" class="btn btn-secondary">
                <i class="fa fa-reply"></i> Voltar
            </a>
            <button class="btn btn-success " type="button" (click)="onPrint()"><i class="fa fa-print"></i> Imprimir</button>
        </div>
    </div>
</div>


