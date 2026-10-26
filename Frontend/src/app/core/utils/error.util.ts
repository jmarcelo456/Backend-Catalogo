import { HttpErrorResponse } from '@angular/common/http';

export function mensajeDeError(err: HttpErrorResponse, porDefecto: string): string {
  if (err.status === 0) return 'No se pudo establecer la conexión con el servidor';
  return err.error?.mensaje ?? porDefecto;
}
