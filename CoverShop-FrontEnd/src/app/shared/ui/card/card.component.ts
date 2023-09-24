import { Component, Input } from '@angular/core';
import { Product } from '../../../Test/product.type';

@Component({
  selector: 'card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class CardComponent {
  @Input() product!: Product;
  @Input() img: string = '';


}
