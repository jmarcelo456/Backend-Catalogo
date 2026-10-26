import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { Observable } from 'rxjs';
import { API_CONFIG } from '../core/config/api.config';
import { Categoria, CategoriaRequest } from '../models/categoria.model';

@Service()
export class CategoriaService {
  private readonly http = inject(HttpClient);
  private readonly url = `${API_CONFIG.baseUrl}/categorias`;

  obtenerTodas(): Observable<Categoria[]> {
    return this.http.get<Categoria[]>(this.url);
  }

  obtenerPorId(id: number): Observable<Categoria> {
    return this.http.get<Categoria>(`${this.url}/${id}`);
  }

  crear(categoria: CategoriaRequest): Observable<Categoria> {
    return this.http.post<Categoria>(this.url, categoria);
  }

  actualizar(id: number, categoria: CategoriaRequest): Observable<Categoria> {
    return this.http.patch<Categoria>(`${this.url}/${id}`, categoria);
  }

  eliminar(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }
}
