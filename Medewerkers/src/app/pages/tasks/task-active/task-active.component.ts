import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Routes } from 'src/app/enums/routes.enum';
import { Task } from 'src/app/interfaces/task.interface';
import { TaskService } from 'src/app/services/task.service';

@Component({
	selector: 'task-active',
	templateUrl: './task-active.component.html',
	styleUrls: ['./task-active.component.scss'],
})
export class TaskActiveComponent implements OnInit, OnDestroy {
	@Input("dateSelected") private _dateSelected: Date;

	private _subscriptions: Subscription[];

	private _task: Task;
	get task() { return this._task}

	get showItem(): boolean {
		if (!this._task) {
			return false;
		}
		else {
			if (this._task.datePlanned.getTime() !== this._dateSelected.getTime()) {
				return false;
			}
			return true;
		}
	}

	constructor(
		private router: Router,
		private taskService: TaskService
	) { }

	ngOnInit(): void { 
		this._subscriptions = [
			this.taskService.task_active$.subscribe((task_active: Task) => {
				this._task = task_active;
			}),
		];
	}

	ngOnDestroy(): void {
		this._subscriptions.forEach((subscription: Subscription) => { subscription.unsubscribe(); });
	}

	public getTaskDetails() {
		this.router.navigate([Routes.Tasks, this.task.id]);
	}
}
