import { Injectable } from '@angular/core';
import { LoadingController } from '@ionic/angular';

@Injectable({
	providedIn: 'root'
})
export class LoadingService {
	private _loadingIndicator: HTMLIonLoadingElement;
	private _isLoading: boolean = false;

	constructor(
		private loadingController: LoadingController
	) { }
	
	public async showLoading(message?: string): Promise<void> {
		message = (message == "" ? undefined : message);

		// Update message if loader is present
		if (this._isLoading) {
			if (this._loadingIndicator) {
				this._loadingIndicator.message = message;
			}
			return;
		}

		this._isLoading = true;
		
		this._loadingIndicator = await this.loadingController.create({
			message: message,
			cssClass:	"custom-loading",
		});

		return await this._loadingIndicator.present().then((res) => {
			// If dismissLoading is called before present has resolved
			if (!this._isLoading) {
				this._loadingIndicator.dismiss();
			}
		});
	}

	public async dismissLoading() {
		this._isLoading = false;
		if (this._loadingIndicator) {
			await this._loadingIndicator.dismiss();
		}
	}
}
