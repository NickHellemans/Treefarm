// Angular
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

// RxJS
import { BehaviorSubject, Observable } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

// External
	// Auth0
	import { AuthService, User } from '@auth0/auth0-angular';

// Self
	// Env
		import { environment } from 'src/environments/environment';

	// Services
		import { HttpClientService } from './httpService/http-client.service';

	// Enums
		import { Routes } from '../enums/routes.enum';
		import { ApiPath } from '../enums/api-path.enum';

	// Interfaces
		import { Employee } from '../interfaces/employee.interface';
		import { Task } from '../interfaces/task.interface';

@Injectable({
	providedIn: 'root'
})
export class LoginService {
	private _user$: Observable<User> = this.authService.user$;
	get user$() { return this._user$; } // For login guard

	private _employee$ = new BehaviorSubject<Employee>(null);
	get employee$() {return this._employee$; }
	get employee() {return this._employee$.value; }

	constructor(
		private router: Router,
		private authService: AuthService,
		private httpClientService: HttpClientService,
	) { }

	public login() {
		this.authService.loginWithRedirect().subscribe((response) => {
			if (!environment.production) {
				console.log("login", response);
			}
		});
	}

	public logout() {
		this.authService.logout()
		this.router.navigate([Routes.Login]);
	}

	public getEmployee(): Observable<Task[]> {
		return this._user$
			.pipe(
				switchMap((user: User) => {
					const path = ApiPath.EMPLOYEE_GET + user.email;
					return this.httpClientService.get(path, "Taken ophalen");
				}),
				map((response) => {
					if (!environment.production) {
						console.log("getEmployeeByEmail", response)
					}

					const employee: Employee = {
						id: response.id,
						employeeId: response.employeeId,
						firstName: response.firstName,
						lastName: response.lastName,
						email: response.email,
						userName: response.userName,
						isAdmin: response.isAdmin,
						isActive: response.isActive
					}
					this._employee$.next(employee);

					return response.tasks;
				})
			);
	}
}
