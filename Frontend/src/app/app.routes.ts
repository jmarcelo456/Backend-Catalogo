import { Routes } from '@angular/router';
import { ProductosComponent } from './productos-component/productos-component';
import { ProductoFormComponent } from './producto-form-component/producto-form-component';
import { DetalleProductoComponent } from './detalle-producto-component/detalle-producto-component';
import { CategoriasComponent } from './categorias-component/categorias-component';

export const routes: Routes = [
  { path: '', redirectTo: 'productos', pathMatch: 'full' },
  { path: 'productos', component: ProductosComponent },
  { path: 'productos/nuevo', component: ProductoFormComponent },
  { path: 'productos/:id/editar', component: ProductoFormComponent },
  { path: 'productos/:id', component: DetalleProductoComponent },
  { path: 'categorias', component: CategoriasComponent },
  { path: '**', redirectTo: 'productos' },
];
