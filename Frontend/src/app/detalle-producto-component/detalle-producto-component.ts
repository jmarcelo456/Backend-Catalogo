import { Component, inject, input, OnInit, signal } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { Producto } from '../models/producto.model';
import { ProductoService } from '../services/producto.service';
import { PipePersonalizadoPipe } from '../../pipes/pipe-personalizado-pipe';

@Component({
  selector: 'app-detalle-producto-component',
  imports: [RouterLink, PipePersonalizadoPipe],
  templateUrl: './detalle-producto-component.html',
})
export class DetalleProductoComponent implements OnInit {
  private readonly productoService = inject(ProductoService);

  id = input.required<string>(); // llega desde /productos/:id

  protected readonly producto = signal<Producto | null>(null);
  protected readonly loading = signal(true);
  protected readonly error = signal<string | null>(null);

  ngOnInit(): void {
    this.productoService
      .obtenerPorId(Number(this.id()))
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: (p) => this.producto.set(p),
        error: (err: HttpErrorResponse) =>
          this.error.set(
            err.status === 404 ? 'Producto no encontrado' : 'No se pudo conectar con el servidor',
          ),
      });
  }
}
