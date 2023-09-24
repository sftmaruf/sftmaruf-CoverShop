import { Component } from '@angular/core';
import { Product } from 'src/app/Test/product.type';
import { products } from 'src/app/Test/products.data';

@Component({
  selector: 'admin-layout',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminLayoutComponent {
  products: Product[] = products;
}
