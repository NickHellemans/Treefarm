import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

// Guards
import { LoginGuard } from './guards/login.guard';

const routes: Routes = [
	{
		path: '',
		redirectTo: 'login',
		pathMatch: 'full'
	},
	{
		path: 'login',
		canLoad: [LoginGuard],
		loadChildren: () => import('./pages/login/login.module').then(m => m.LoginPageModule)
	},
	{
		path: 'tasks',
		canLoad: [LoginGuard],
		loadChildren: () => import('./pages/tasks/tasks.module').then(m => m.TasksPageModule)
	},
	{
		path: 'about',
		canLoad: [LoginGuard],
		loadChildren: () => import('./pages/about/about.module').then( m => m.AboutPageModule)
	},
];

@NgModule({
	imports: [
		RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
	],
	exports: [RouterModule]
})
export class AppRoutingModule { }
