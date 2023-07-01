import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { TasksPageRoutingModule } from './tasks-routing.module';

import { TasksPage } from './tasks.page';
import { TaskListComponent } from './task-list/task-list.component';
import { DatePickerComponent } from './date-picker/date-picker.component';
import { TaskListItemComponent } from './task-list-item/task-list-item.component';
import { TaskButtonStartComponent } from './task-button-start/task-button-start.component';
import { TaskButtonPauseComponent } from './task-button-pause/task-button-pause.component';
import { TaskDetailComponent } from './task-detail/task-detail.component';
import { TaskActiveComponent } from './task-active/task-active.component';
import { SidebarMenuComponent } from './sidebar-menu/sidebar-menu.component';
import { ButtonsCameraComponent } from './buttons-camera/buttons-camera.component';

import { FilterByPipe } from 'src/app/pipes/filter-by.pipe';
import { SortPipe } from 'src/app/pipes/sort.pipe';

@NgModule({
	imports: [
		CommonModule,
		FormsModule,
		IonicModule,
		TasksPageRoutingModule
	],
	declarations: [
		TasksPage,
		TaskActiveComponent,
		TaskListComponent,
		TaskListItemComponent,
		TaskButtonStartComponent,
		TaskButtonPauseComponent,
		TaskDetailComponent,
		SidebarMenuComponent,
		ButtonsCameraComponent,
		DatePickerComponent,
		FilterByPipe,
		SortPipe
	]
})
export class TasksPageModule { }
