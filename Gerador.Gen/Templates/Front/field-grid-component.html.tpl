<div class="gc-table-responsive pre-scrollable">
<table class="table table-striped table-app">
    <thead class="thead-inverse">
    <tr>
        <th width="1" class="text-center">Ações</th>
<#fieldItemsColumns#>
    </tr>
    <tr>
        <th width="1" class="text-center"></th>
<#fieldItemsFilter#>
    <tr>
    </thead>
    <tbody>
    <tr *ngFor="let item of vm.filterResult">

        <td class="text-center text-nowrap">
        <button (click)="onEdit($event, item)"  placement="top" title="Editar" class="btn btn-sm btn-primary">
            <i class="icon icon-pencil"></i>
        </button>
        <button (click)="onDetails($event, item)"  placement="top" title="Detalhes" class="btn btn-sm btn-default">
            <i class="icon icon-info"></i>
        </button>
        <button (click)="onPrint($event, item)"  placement="top" title="Imprimir" class="btn btn-sm btn-default">
            <i class="icon icon-printer"></i>
        </button>
        <button (click)="onDeleteConfimation($event, item)" placement="top" title="Excluir" class="btn btn-sm btn-danger">
            <i class="icon icon-trash"></i>
        </button>
        </td>
<#fieldItemsRows#>
    </tr>
    </tbody>
</table>
</div>