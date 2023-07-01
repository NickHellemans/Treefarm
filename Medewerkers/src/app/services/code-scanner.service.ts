// Angular
import { Injectable } from '@angular/core';

// RxJS
import { BehaviorSubject } from 'rxjs';

// Ionic
import { AlertButton } from '@ionic/angular';

// Capacitor
import { Capacitor } from '@capacitor/core';
import { BarcodeScanner, ScanOptions, ScanResult, SupportedFormat } from '@capacitor-community/barcode-scanner';

// Self
	// Services
		import { HttpClientService } from './httpService/http-client.service';
		import { AlertService } from './ionic/alert.service';

@Injectable({
	providedIn: 'root'
})
export class CodeScannerService {
	private _scanData$ = new BehaviorSubject<string>(null);
	get scanData$() { return this._scanData$; }

	private _isScanActive$ = new BehaviorSubject<boolean>(false);
	get isScanActive$() { return this._isScanActive$; }

	private _isTorchActive$ = new BehaviorSubject<boolean>(false);
	get isTorchActive$() { return this._isTorchActive$; }

	constructor(
		private httpClientService: HttpClientService,
		private alertService: AlertService
	) { }

	// Scanner
	async scannerStart() {
		let url: string;
		if (Capacitor.isNativePlatform()) {
			const hasPermission = await this._checkPermission();

			if (!hasPermission) {
				return BarcodeScanner.openAppSettings();
			}
			this._isScanActive$.next(true);

			
			const options: ScanOptions = {
				targetedFormats: [
					SupportedFormat.QR_CODE
				]
			}

			document.querySelector('body').classList.add('scanner-active');

			const result: ScanResult = await BarcodeScanner.startScan(options);
			if (result.hasContent) {
				this._scanData$.next(result.content);
				console.log(result.content);
				url = result.content;
			}

			this.scannerStop();
			document.querySelector('body').classList.remove('scanner-active');
		}
		else {
			url = "https://localhost:44312/Download/GetInstructions?filename=filepath_instructies.pdf";
		}

		const alert_header = "Document openen?";
		const alert_message = `Weet u zeker dat u volgend document wilt openen: ${url}`;
		const alert_buttons: AlertButton[] = [
			{
				text: "Annuleren",
				role: "cancel",
				cssClass: "alert-button-cancel",
			},
			{
				text: "Open",
				role: "confirm",
				cssClass: "alert-button-confirm",
				handler: () => {
					this.httpClientService.open(url);
				}
			}
		]
		const alert = await this.alertService.presentAlert(alert_header, alert_message, alert_buttons, "", true);
	}

	async scannerStop() {
		BarcodeScanner.stopScan();	
		document.querySelector('body').classList.remove('scanner-active');
		this._isScanActive$.next(false);
	}

	async toggleTorch() {
		const isTorchActive: boolean = this._isTorchActive$.value;

		if (!isTorchActive) {
			this._isTorchActive$.next(true);
			return await BarcodeScanner.enableTorch();
		}
		this._isTorchActive$.next(false);
		return await BarcodeScanner.disableTorch();
	}

	private async _checkPermission() {
		return new Promise(async (resolve, reject) => {
			const status = await BarcodeScanner.checkPermission({ force: true });
			if (status.granted) {
				resolve(true);
			}
			else if (status.denied) {
				resolve(false);
			}
		});
	}

	// Misc
	clearScannedData(): void {
		this._scanData$.next(null);
	}
}
