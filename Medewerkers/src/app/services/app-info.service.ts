import { Injectable } from '@angular/core';

@Injectable({
	providedIn: 'root'
})
export class AppInfoService {

	// App info
	private readonly _appInfo: { name: string, version: string };
	get appInfo() { return this._appInfo; }

	private readonly _owner: string;
	get owner() { return this._owner; }

	private readonly _developers: Array<string>;
	get developers() { return this._developers; }

	constructor() { 
		this._appInfo = { name: "Boompjes", version: "1.0" };

		this._owner = "ModernWayz";

		this._developers = [
			"Hellemans Nick",
			"Van de Poel Niels",
			"Verbist Gijs",
			"Willemen Stijn"
		]
	}
}
