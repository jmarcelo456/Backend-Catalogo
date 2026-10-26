import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'pipePersonalizado',
})
export class PipePersonalizadoPipe implements PipeTransform {
  transform(value: number | string | null | undefined): string {
    if (value === null || value === undefined || isNaN(Number(value))) {
      return 'Bs. 0,00';
    }
    const Decimales = Number(value).toFixed(2);

    const fortBs = Decimales.replace('.', ',');

    return `Bs. ${fortBs}`;
  }
}
