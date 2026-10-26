import { Component, computed, inject, input, OnInit, signal } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ProductoRequest } from '../models/producto.model';
import { Categoria } from '../models/categoria.model';
import { ProductoService } from '../services/producto.service';
import { CategoriaService } from '../services/categoria.service';
import { mensajeDeError } from '../core/utils/error.util';

@Component({
  selector: 'app-producto-form-component',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './producto-form-component.html',
})
export class ProductoFormComponent implements OnInit {
  private readonly fb = inject(NonNullableFormBuilder);
  private readonly router = inject(Router);
  private readonly productoService = inject(ProductoService);
  private readonly categoriaService = inject(CategoriaService);

  id = input<string | undefined>(); // existe solo en /productos/:id/editar

  protected readonly esEdicion = computed(() => !!this.id());
  protected readonly categorias = signal<Categoria[]>([]);
  protected readonly error = signal<string | null>(null);

  protected readonly form = this.fb.group({
    nombre: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
    precio: [1, [Validators.required, Validators.min(1), Validators.max(100)]],
    stock: [1, [Validators.required, Validators.min(1)]],
    categoriaId: [0, [Validators.required, Validators.min(1)]],
  });

  ngOnInit(): void {
    this.categoriaService.obtenerTodas().subscribe({
      next: (data) => this.categorias.set(data),
      error: (err: HttpErrorResponse) =>
        this.error.set(mensajeDeError(err, 'No se pudieron cargar las categorías')),
    });

    const id = this.id();
    if (id) {
      this.productoService.obtenerPorId(Number(id)).subscribe({
        next: (p) =>
          this.form.patchValue({
            nombre: p.nombre,
            precio: p.precio,
            stock: p.stock,
            categoriaId: p.categoriaId,
          }),
        error: () => this.error.set('Producto no encontrado'),
      });
    }
  }

  protected guardar(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const request: ProductoRequest = this.form.getRawValue();
    const id = this.id();
    const peticion = id
      ? this.productoService.actualizar(Number(id), request)
      : this.productoService.crear(request);

    this.error.set(null);
    peticion.subscribe({
      next: () => this.router.navigate(['/productos']),
      error: (err: HttpErrorResponse) =>
        this.error.set(mensajeDeError(err, 'No se pudo guardar el producto')),
    });
  }
}
