// Angular
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

// Self
	// Services
		import { MenuService } from 'src/app/services/ionic/menu.service';
		import { CodeScannerService } from 'src/app/services/code-scanner.service';
		import { LoginService } from 'src/app/services/login.service';

	// Enums
		import { Routes } from 'src/app/enums/routes.enum';

@Component({
	selector: 'sidebar-menu',
	templateUrl: './sidebar-menu.component.html',
	styleUrls: ['./sidebar-menu.component.scss'],
})
export class SidebarMenuComponent implements OnInit {
	get menuId(): string { return this.menuService.menuId; }

	constructor(
		private router: Router,

		private menuService: MenuService,
		private codeScanner: CodeScannerService,
		private loginService: LoginService
	) { }

	ngOnInit() { }

	public startScan(): void {
		this.menuService.menuClose();
		this.codeScanner.scannerStart()
	}

	public navToAbout(): void {
		this.menuService.menuClose();
		this.router.navigate([Routes.About]);
	}

	public logOut(): void {
		this.loginService.logout();
	}
}
