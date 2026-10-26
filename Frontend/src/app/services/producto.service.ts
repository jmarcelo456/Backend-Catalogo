import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { Observable } from 'rxjs';
import { API_CONFIG } from '../core/config/api.config';
import { Producto, ProductoRequest } from '../models/producto.model';

@Service()
export class ProductoService {
  private readonly http = inject(HttpClient);
  private readonly url = `${API_CONFIG.baseUrl}/productos`;

  obtenerTodos(): Observable<Producto[]> {
    return this.http.get<Producto[]>(this.url);
  }

  obtenerPorId(id: number): Observable<Producto> {
    return this.http.get<Producto>(`${this.url}/${id}`);
  }

  crear(producto: ProductoRequest): Observable<Producto> {
    return this.http.post<Producto>(this.url, producto);
  }

  actualizar(id: number, producto: ProductoRequest): Observable<Producto> {
    return this.http.patch<Producto>(`${this.url}/${id}`, producto);
  }

  eliminar(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }
}
