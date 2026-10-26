import { computed, Directive, input } from '@angular/core';

@Directive({
  selector: '[appStockBajo]',
  host: {
    '[class.bg-red-100]': 'agotado()',
    '[class.border-red-500]': 'agotado()',
    '[class.bg-amber-100]': 'bajo()',
    '[class.border-amber-500]': 'bajo()',
    '[class.border-l-4]': 'agotado() || bajo()',
  },
})
export class DirectivaStock {
  stock = input.required<number>({ alias: 'appStockBajo' });
  umbral = input(5); // por debajo o igual a este valor se considera stock bajo

  agotado = computed(() => this.stock() <= 0);
  bajo = computed(() => this.stock() > 0 && this.stock() <= this.umbral());
}
