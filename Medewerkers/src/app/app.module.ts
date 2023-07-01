// Angular
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

// Ionic
import { IonicModule, IonicRouteStrategy } from '@ionic/angular';

// External
	// Auth0
		import { AuthModule, AuthHttpInterceptor } from '@auth0/auth0-angular';

// Self
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { TasksPageModule } from './pages/tasks/tasks.module';
import { environment } from 'src/environments/environment';

@NgModule({
	declarations: [AppComponent],
	imports: [
		BrowserModule, 
		IonicModule.forRoot(),
		HttpClientModule,
		TasksPageModule,
		AppRoutingModule,
		AuthModule.forRoot(
			{
				domain: environment.authentication.domain,
				clientId: environment.authentication.clientId,
				audience: environment.authentication.audience,				
				httpInterceptor: {
					allowedList: [`${environment.baseUrl}/*`],
				},
			}
		),
	],
	providers: [
		{ provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
		{
			provide: HTTP_INTERCEPTORS,
			useClass: AuthHttpInterceptor,
			multi: true,
		  },
	],
	bootstrap: [AppComponent],
})
export class AppModule { }
