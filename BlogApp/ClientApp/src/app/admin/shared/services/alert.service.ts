import { Injectable } from '@angular/core';
import {Subject} from 'rxjs';
import {Alert} from '../../../shared/models/alert.model';

@Injectable()
export class AlertService {

  public alert$ = new Subject<Alert>();

  constructor() { }

  success(text: string): void{
    this.alert$.next({type: 'success', text});
  }

  danger(text: string): void{
    this.alert$.next({type: 'danger', text});
  }

  warning(text: string): void{
    this.alert$.next({type: 'warning', text});
  }
}
