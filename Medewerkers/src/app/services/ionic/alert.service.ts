// Angular
import { Injectable } from '@angular/core';

// Ionic
import { AlertButton, AlertController } from '@ionic/angular';

@Injectable({
	providedIn: 'root'
})
export class AlertService {
	private _alert: HTMLIonAlertElement;

	constructor(
		private alertController: AlertController
	) { }

	/**
	 * To wait for alert dismissal
	 * ``` 
	 * const alert = await alertService.presentAlert(...);
	 * await alert.onDidDismiss();
	 * ```
	 */
	async presentAlert(header: string, message: string, buttons: AlertButton[], cssClass = "alert-container", backdropDismiss = false): Promise<HTMLIonAlertElement> {
		return new Promise(async (resolve) => {
			this._alert = await this.alertController.create({
				header: header,
				message: message,
				cssClass: cssClass,
				backdropDismiss: backdropDismiss,
				buttons: buttons
			});

			await this._alert.present();
			resolve(this._alert);
		});
	}
}
