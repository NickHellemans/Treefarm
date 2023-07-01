import { Injectable } from '@angular/core';
import { ToastController } from '@ionic/angular';

@Injectable({
	providedIn: 'root'
})
export class ToastService {
	private _toast: HTMLIonToastElement = null;

	constructor(
		private toastController: ToastController
	) { }

	async presentToast(
		message: string,
		color: string = "danger",
		duration: number = 3000,
	) {
		this._toast = await this.toastController.create({
			message: message,
			color: color,
			duration: duration,
			buttons: [
				{
					text: 'Sluiten',
					role: 'cancel'
				}
			]
		});

		await this._toast.present();
	}

	async dismissToast(data?: any) {
		if (this._toast) {
			await this._toast.dismiss(data);
			this._toast = null;
		}
	}
}
