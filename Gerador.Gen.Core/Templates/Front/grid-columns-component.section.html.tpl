        <th class="text-nowrap">
          <span class="table-sort">
            {{ vm.infos | traduction: '<#propertyName#>' }}
            <a href='#' (click)='onOrderBy($event,"<#propertyName#>")'><i class="fa fa-sort" aria-hidden="true"></i></a>
          </span>
        </th>