export type AlertType = 'success' | 'danger' | 'warning';

export interface Alert {
  type: AlertType;
  text: string;
}
