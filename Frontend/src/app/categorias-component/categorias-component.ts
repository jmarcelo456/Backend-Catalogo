import { Component, inject, OnInit, signal } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { finalize } from 'rxjs';
import { Categoria, CategoriaRequest } from '../models/categoria.model';
import { CategoriaService } from '../services/categoria.service';
import { mensajeDeError } from '../core/utils/error.util';

@Component({
  selector: 'app-categorias-component',
  imports: [ReactiveFormsModule],
  templateUrl: './categorias-component.html',
})
export class CategoriasComponent implements OnInit {
  private readonly fb = inject(NonNullableFormBuilder);
  private readonly categoriaService = inject(CategoriaService);

  protected readonly categorias = signal<Categoria[]>([]);
  protected readonly loading = signal(false);
  protected readonly error = signal<string | null>(null);
  protected readonly editandoId = signal<number | null>(null);

  protected readonly form = this.fb.group({
    nombre: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
    descripcion: ['', [Validators.maxLength(250)]],
  });

  ngOnInit(): void {
    this.cargar();
  }

  protected cargar(): void {
    this.loading.set(true);
    this.error.set(null);
    this.categoriaService
      .obtenerTodas()
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: (data) => this.categorias.set(data),
        error: (err: HttpErrorResponse) =>
          this.error.set(mensajeDeError(err, 'No se pudieron cargar las categorías')),
      });
  }

  protected editar(categoria: Categoria): void {
    this.editandoId.set(categoria.id);
    this.form.patchValue({
      nombre: categoria.nombre,
      descripcion: categoria.descripcion ?? '',
    });
  }

  protected cancelar(): void {
    this.editandoId.set(null);
    this.form.reset();
  }

  protected guardar(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const { nombre, descripcion } = this.form.getRawValue();
    const request: CategoriaRequest = { nombre, descripcion: descripcion || null };
    const id = this.editandoId();
    const peticion =
      id === null
        ? this.categoriaService.crear(request)
        : this.categoriaService.actualizar(id, request);

    this.error.set(null);
    peticion.subscribe({
      next: () => {
        this.cancelar();
        this.cargar();
      },
      error: (err: HttpErrorResponse) =>
        this.error.set(mensajeDeError(err, 'No se pudo guardar la categoría')),
    });
  }

  protected eliminar(categoria: Categoria): void {
    if (!confirm(`¿Eliminar la categoría "${categoria.nombre}"?`)) return;

    this.error.set(null);
    this.categoriaService.eliminar(categoria.id).subscribe({
      next: () => this.categorias.update((lista) => lista.filter((c) => c.id !== categoria.id)),
      error: (err: HttpErrorResponse) =>
        this.error.set(mensajeDeError(err, 'No se pudo eliminar la categoría')),
    });
  }
}
