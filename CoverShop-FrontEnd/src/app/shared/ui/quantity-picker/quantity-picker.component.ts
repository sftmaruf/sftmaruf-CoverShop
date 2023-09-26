import { Component } from '@angular/core';

@Component({
  selector: 'quantity-picker',
  templateUrl: './quantity-picker.component.html',
  styleUrls: ['./quantity-picker.component.scss']
})
export class QuantityPickerComponent {
  quantity: number = 0;
  private maxQuantity: number = 100;

  increment() {
    if(this.maxQuantity > this.quantity) this.quantity++;
  }

  decrement() {
    if(this.quantity > 0) this.quantity--;
  }
}
