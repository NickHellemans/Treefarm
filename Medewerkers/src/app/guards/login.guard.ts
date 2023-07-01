// Angular
import { Injectable } from '@angular/core';
import { CanLoad, Route, Router, UrlSegment, UrlTree } from '@angular/router';

// RxJS
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';

// External
	// Auth0
		import { User } from '@auth0/auth0-angular';

// Self
	// Services
		import { LoginService } from '../services/login.service';

	// Enums
		import { Routes } from '../enums/routes.enum';

@Injectable({
	providedIn: 'root'
})
export class LoginGuard implements CanLoad {

	constructor(
		private router: Router,
		private loginService: LoginService
	) {

	}

	canLoad(
		route: Route,
		segments: UrlSegment[]): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
		return this.loginService.user$
			.pipe(				
				map((user: User) => {
					if (route.path === Routes.Login.substring(1)) {
						if (user) {
							return this.router.createUrlTree([Routes.Tasks]);
						}
						return true;
					}
					
					if (user) {
						return true;
					}
					return this.router.createUrlTree([Routes.Login]);
				})
			);
	}
}
