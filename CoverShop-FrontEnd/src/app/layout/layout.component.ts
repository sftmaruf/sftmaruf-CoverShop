import { Component } from '@angular/core';
import { Layout } from './layout.types';

@Component({
  selector: 'layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent {
  layout: Layout = Layout.Admin;
}
