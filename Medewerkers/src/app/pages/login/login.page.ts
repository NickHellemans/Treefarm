// Angular
import { Component, OnInit } from '@angular/core';

// Self
	// Services
		import { LoginService } from 'src/app/services/login.service';

@Component({
	selector: 'app-login',
	templateUrl: './login.page.html',
	styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {

	constructor(
		private loginService: LoginService
	) { }

	ngOnInit(): void {
		this.loginService.login();
	}
}
