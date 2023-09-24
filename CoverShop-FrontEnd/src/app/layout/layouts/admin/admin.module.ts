import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminLayoutComponent } from './admin.component';
import { HeaderModule } from '../../common/header/header.module';
import { BannerModule } from '../../common/banner/banner.module';
import { CardModule } from 'src/app/shared/ui/card/card.module';

@NgModule({
  declarations: [
    AdminLayoutComponent
  ],
  imports: [
    CommonModule,
    HeaderModule,
    BannerModule,
    CardModule
  ],
  exports: [
    AdminLayoutComponent
  ]
})
export class AdminLayoutModule { }
