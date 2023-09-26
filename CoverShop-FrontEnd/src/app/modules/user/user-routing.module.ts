import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductOverviewComponent } from './product-overview/product-overview.component';

const routes: Routes = [
  {
    path: 'overview',
    component: ProductOverviewComponent,
    loadChildren: () => import('./product-overview/product-overview.module').then((m) => m.ProductOverviewModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
