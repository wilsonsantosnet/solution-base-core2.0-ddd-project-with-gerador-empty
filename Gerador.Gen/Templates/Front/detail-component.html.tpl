<section class="container-fluid">
  <section class="card">
    <header class="card-header">
      <div class="row align-items-center">
        <div class="col">
          <span class="fa fa-angle-double-right" aria-hidden="true"></span> {{ vm.actionTitle }}<br>
          <small class="text-muted">{{ vm.actionDescription }}</small>
        </div>
      </div>
    </header>
    <article class="card-body">
      <app-<#classNameLowerAndSeparator#>-container-details [(vm)]="vm"></app-<#classNameLowerAndSeparator#>-container-details>
    </article>
    <footer class="card-footer  d-flex justify-content-end">
      <a href="javascript:history.back()" class="btn btn-primary btn-sm">
        <i class="fa fa-reply"></i> Voltar
      </a>
    </footer>
  </section>
</section>
