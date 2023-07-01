// Angular
import { Component, Input, OnDestroy, OnInit } from '@angular/core';

// RxJS
import { Subscription } from 'rxjs';

// Self
	// Services
		import { TaskService } from 'src/app/services/task.service';

	// Interfaces
		import { Task, TaskStatus } from 'src/app/interfaces/task.interface';

@Component({
  selector: 'task-button-start',
  templateUrl: './task-button-start.component.html',
  styleUrls: ['./task-button-start.component.scss'],
})
export class TaskButtonStartComponent implements OnInit, OnDestroy {
	private _subscriptions: Subscription[];

	private _task_active: Task;
	get task_active() { return this._task_active; }

	private _currentPriority: number;

	@Input("size") private _size?: "small" | "large" | "default" | undefined = undefined;
	get size() { return this._size; }

	@Input("shape") private _shape?: "round" | undefined = undefined;
	get shape() { return this._shape; }

	@Input("task") private _task: Task;

	get ionIcon_shape(): string {
		let shape: string;
		switch (this._task.status) {
			case TaskStatus.ToDo:
				shape = "play";
				break;
			case TaskStatus.InProgress:
				shape = "stop";
				break;
			default:
				shape = "stop";
				break;
		}
		shape += "-circle-outline"
		return shape;
	}

	get buttonText(): string {
		let text: string;
		switch (this._task.status) {
			case TaskStatus.ToDo:
				text = "Start taak";
				break;
			case TaskStatus.InProgress:
				text = "Stop taak";
				break;
		}
		return text;
	}

	constructor(
		private taskService: TaskService
	) {

	}

	get shouldButtonShow(): boolean {
		switch (this._task.status) {
			case TaskStatus.ToDo:
			case TaskStatus.InProgress:
				return true;
			default:
				return false;
		}
	}

	get shouldButtonDisable(): boolean {
		const date_now = new Date();
		const date_today = new Date(date_now.getFullYear(), date_now.getMonth(), date_now.getDate());

		if (this._task_active) {
			if (this.task_active.id !== this._task.id) {
				return true;
			}
		}
		// Disable if task date doesn't match current date
		else if (this._task.datePlanned.getTime() !== date_today.getTime()) {
			return true;
		}
		// Disable if task priority doesn't match current priority
		else if (this._currentPriority !== this._task.priority) {
			return true;
		}
		return false;
	}

	ngOnInit(): void { 
		this._subscriptions = [
			this.taskService.task_active$.subscribe((task_active: Task) => {
				this._task_active = task_active;
			}),
			this.taskService.currentPriority$.subscribe((currentPriority: number) => {
				this._currentPriority = currentPriority;
			}),
		];
	}

	ngOnDestroy(): void {
		this._subscriptions.forEach((subscription: Subscription) => { subscription.unsubscribe(); });
	}

	public updateTaskStatus($event: Event) {
		$event.stopPropagation();
		this.taskService.taskStart(this._task);
	}
}
