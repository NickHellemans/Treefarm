import { Component, Input, OnInit } from '@angular/core';
import { Task, TaskStatus } from 'src/app/interfaces/task.interface';
import { TaskService } from 'src/app/services/task.service';

@Component({
	selector: 'task-button-pause',
	templateUrl: './task-button-pause.component.html',
	styleUrls: ['./task-button-pause.component.scss'],
})
export class TaskButtonPauseComponent implements OnInit {
	@Input("size") private _size?: "small" | "large" | "default" | undefined = undefined;
	get size() { return this._size; }

	@Input("shape") private _shape?: "round" | undefined = undefined;
	get shape() { return this._shape; }

	@Input("task") private _task: Task;

	get showButton(): boolean { return this._task.status === TaskStatus.InProgress ||this._task.status === TaskStatus.Paused; }

	get buttonText(): string {
		let text: string;
		switch (this._task.status) {
			case TaskStatus.InProgress:
				text = "Pauzeer taak";
				break;
			case TaskStatus.Paused:
				text = "Hervat taak";
				break;
		}
		return text;
	}

	get ionIcon_shape(): string {
		let shape: string;
		switch (this._task.status) {
			case TaskStatus.InProgress:
				shape = "pause";
				break;
			case TaskStatus.Paused:
				shape = "play";
				break;
			default:
				shape = "stop";
				break;
		}
		shape += "-circle-outline"
		return shape;
	}

	constructor(
		private taskService: TaskService
	) { }

	ngOnInit(): void { }

	public updateTaskStatus($event: Event) {
		$event.stopPropagation();
		this.taskService.taskPause(this._task);
	}
}
