// Angular
import { Injectable } from '@angular/core';
import { HttpParams, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

// RxJS
import { retry, catchError, map, timeout } from 'rxjs/operators';

// Capacitor
import { Browser, OpenOptions } from '@capacitor/browser';

// Self
	// Env
		import { environment } from 'src/environments/environment';

	// Services
		import { HttpErrorService } from './http-error.service';
		import { LoadingService } from '../ionic/loading.service';

@Injectable({
	providedIn: 'root'
})
export class HttpClientService {

	constructor(
		private http: HttpClient,
		private router: Router,

		private httpErrorService: HttpErrorService,
		private loadingService: LoadingService
	) { }

	public get(path: string, loadingMessage?: string) {
		const fullPath = environment.baseUrl + path;

		this.loadingService.showLoading(loadingMessage);
		return this.http.get(fullPath)
			.pipe((
				map((response: any) => {
					this.loadingService.dismissLoading();
					return response;
				})
			),
				retry(3),
				catchError((error: HttpErrorResponse) => { return this.httpErrorService.handleError(error); })
			);
	}

	public post(path: string, body: any, loadingMessage?: string) {
		const fullPath = environment.baseUrl + path;

		this.loadingService.showLoading(loadingMessage);
		return this.http.post(fullPath, body)
			.pipe((
				map((response: any) => {
					this.loadingService.dismissLoading();
					return response;
				})
			),
				catchError((error: HttpErrorResponse) => { return this.httpErrorService.handleError(error); })
			);
	}

	public put(path: string, body: any, loadingMessage?: string) {
		const fullPath = environment.baseUrl + path;

		this.loadingService.showLoading(loadingMessage);
		return this.http.put(fullPath, body)
			.pipe((
				map((response: any) => {
					this.loadingService.dismissLoading();
					return response;
				})
			),
				catchError((error: HttpErrorResponse) => { return this.httpErrorService.handleError(error); })
			);
	}

	public async open(path: string) {
		const options:	OpenOptions = { url: path };
		await Browser.open(options);
	}
}
