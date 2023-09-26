import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductOverviewComponent } from './product-overview.component';
import { QuantityPickerModule } from "../../../shared/ui/quantity-picker/quantity-picker.module";



@NgModule({
    declarations: [
        ProductOverviewComponent,
    ],
    imports: [
        CommonModule,
        QuantityPickerModule
    ]
})
export class ProductOverviewModule { }
