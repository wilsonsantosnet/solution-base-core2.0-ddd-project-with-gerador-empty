<div bsModal #<#componentId#>Modal="bs-modal" class="modal fade" id="<#componentId#>Modal">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header">
        <h4 class="modal-title pull-left"><#componentTitle#></h4>
        <button type="button" class="close pull-right" aria-label="Close" (click)="$event.preventDefault();<#componentId#>Modal.hide()">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body pre-scrollable">
        <#componentTag#>
      </div>
      <div class="modal-footer">
        <button class="btn btn-default " type="button" (click)="$event.preventDefault();<#componentId#>Modal.hide()" >
          <i class="icon-close icons"></i>
          {{vm.generalInfo | traduction:"cancelar"}}
        </button>
      </div>
    </div>
  </div>
</div>
