import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from './layout.component';
import { AdminLayoutModule } from './layouts/admin/admin.module';

@NgModule({
  declarations: [
    LayoutComponent,
  ],
  imports: [
    CommonModule,
    AdminLayoutModule
  ]
})
  
export class LayoutModule { }
