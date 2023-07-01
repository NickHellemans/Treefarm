import { Injectable } from '@angular/core';
import { MenuController } from '@ionic/angular';

@Injectable({
	providedIn: 'root'
})
export class MenuService {
	private readonly _MENU_ID = "sidebarMenu";
	get menuId() { return this._MENU_ID; }

	constructor(
		private menuController: MenuController
	) { }

	public menuClose() {
		this.menuController.close(this._MENU_ID);
	}

	public menuEnable() {
		this._menuToggle(true);
	}

	public menuDisable() {
		this._menuToggle(false);
	}

	private _menuToggle(shouldEnable: boolean) {
		this.menuController.enable(shouldEnable, this._MENU_ID);
	}
}
