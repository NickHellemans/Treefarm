// Angular
import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

// RxJS
import { throwError } from 'rxjs';

// Self
	// Services
		import { LoadingService } from '../ionic/loading.service';
		import { ToastService } from '../ionic/toast.service';

	// Env
		import { environment } from 'src/environments/environment';

@Injectable({
	providedIn: 'root'
})
export class HttpErrorService {

	constructor(
		private loadingService: LoadingService,
		private toastService: ToastService
	) { }

	public handleError(error: HttpErrorResponse) {
		if (!environment.production) {
			console.log(error);
		}

		this.loadingService.dismissLoading();

		let errorText: string;
		let toastColor: string;

		if (error.status === 401 || error.status === 440) {
			errorText = "Server session expired... trying to reconnect...";
			setTimeout(function () { window.location.reload(); }, 3000);
		}
		else if (error.status === 701) {
			errorText = "An error has occurred. Please try again.";
		}
		else if (!window.navigator.onLine) {
			return throwError(error || 'Server error');
		}
		else {
			errorText = "An error has occurred. Please try again.";
		}

		this.toastService.presentToast(errorText);
		return throwError(error || 'Server error');
	}
}
