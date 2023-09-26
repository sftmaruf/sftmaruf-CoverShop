import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuantityPickerComponent } from './quantity-picker.component';



@NgModule({
  declarations: [
    QuantityPickerComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    QuantityPickerComponent
  ]
})
export class QuantityPickerModule { }
