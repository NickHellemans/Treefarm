// Angular
import { Component, OnDestroy, OnInit } from '@angular/core';

// RxJS
import { Subscription } from 'rxjs';

// Self
import { CodeScannerService } from 'src/app/services/code-scanner.service';

@Component({
	selector: 'buttons-camera',
	templateUrl: './buttons-camera.component.html',
	styleUrls: ['./buttons-camera.component.scss'],
})
export class ButtonsCameraComponent implements OnInit, OnDestroy {
	private _subscriptions: Subscription[];

	private _isTorchActive: boolean;
	get isTorchActive(): boolean { return this._isTorchActive; }

	constructor(
		private codeScannerService: CodeScannerService
	) { }

	ngOnInit(): void {
		this._subscriptions = [
			this.codeScannerService.isTorchActive$.subscribe((isTorchActive: boolean) => {
				this._isTorchActive = isTorchActive;
			}),
		];
	}

	ngOnDestroy(): void {
		this._subscriptions.forEach(subscription => subscription.unsubscribe());
	}

	scannerStop(): void {
		this.codeScannerService.scannerStop();
	}

	toggleTorch(): void {
		this.codeScannerService.toggleTorch();
	}
}
