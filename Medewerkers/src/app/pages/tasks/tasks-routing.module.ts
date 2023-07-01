import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TaskDetailComponent } from './task-detail/task-detail.component';
import { TaskListComponent } from './task-list/task-list.component';

import { TasksPage } from './tasks.page';

const routes: Routes = [
	{
		path: 'tasks',
		component: TasksPage,
		children: [
			{ path: '', component: TaskListComponent },
			{ path: ':id', component: TaskDetailComponent }
		]
	},
	
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class TasksPageRoutingModule { }
