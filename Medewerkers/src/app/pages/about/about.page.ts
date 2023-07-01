// Angular
import { Component, OnInit } from '@angular/core';

// Self
	// Services
		import { AppInfoService } from 'src/app/services/app-info.service';

@Component({
	selector: 'app-about',
	templateUrl: './about.page.html',
	styleUrls: ['./about.page.scss'],
})
export class AboutPage implements OnInit {
	get appInfo(): { name: string, version: string } { return this.appInfoService.appInfo; }
	get owner(): string { return this.appInfoService.owner; }
	get developers(): Array<string> { return this.appInfoService.developers; }

	private _showEasterEgg = false;
	get showEasterEgg() { return this._showEasterEgg; }
	
	get easterEggUrl() { return "assets/easter-egg.png"; }

	constructor(
		private appInfoService: AppInfoService
	) { }

	ngOnInit() {
	}

	onClick(developer: string): void {
		if (developer === "Hellemans Nick") {
			this._showEasterEgg = !this._showEasterEgg;
		}
	}
}
