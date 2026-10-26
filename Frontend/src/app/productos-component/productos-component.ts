import { Component, computed, inject, input, OnInit, signal } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { Producto } from '../models/producto.model';
import { Categoria } from '../models/categoria.model';
import { ProductoService } from '../services/producto.service';
import { CategoriaService } from '../services/categoria.service';
import { mensajeDeError } from '../core/utils/error.util';
import { PipePersonalizadoPipe } from '../../pipes/pipe-personalizado-pipe';
import { DirectivaStock } from '../directivas/directiva-stock';

@Component({
  selector: 'app-productos-component',
  imports: [RouterLink, PipePersonalizadoPipe, DirectivaStock],
  templateUrl: './productos-component.html',
})
export class ProductosComponent implements OnInit {
  private readonly productoService = inject(ProductoService);
  private readonly categoriaService = inject(CategoriaService);

  categoriaId = input<string | undefined>(); // llega desde ?categoriaId=

  protected readonly productos = signal<Producto[]>([]);
  protected readonly categorias = signal<Categoria[]>([]);
  protected readonly loading = signal(false);
  protected readonly error = signal<string | null>(null);

  protected readonly productosFiltrados = computed(() => {
    const id = this.categoriaId();
    return id ? this.productos().filter((p) => p.categoriaId === Number(id)) : this.productos();
  });

  ngOnInit(): void {
    this.cargar();
  }

  protected cargar(): void {
    this.loading.set(true);
    this.error.set(null);

    this.productoService
      .obtenerTodos()
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: (data) => this.productos.set(data),
        error: (err: HttpErrorResponse) =>
          this.error.set(mensajeDeError(err, 'No se pudieron cargar los productos')),
      });

    this.categoriaService.obtenerTodas().subscribe({
      next: (data) => this.categorias.set(data),
    });
  }

  protected eliminar(producto: Producto): void {
    if (!confirm(`¿Eliminar "${producto.nombre}"?`)) return;

    this.productoService.eliminar(producto.id).subscribe({
      next: () => this.productos.update((lista) => lista.filter((p) => p.id !== producto.id)),
      error: (err: HttpErrorResponse) =>
        this.error.set(mensajeDeError(err, 'No se pudo eliminar el producto')),
    });
  }
}
