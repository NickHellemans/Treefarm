// Angular
import { Component, Input, OnInit } from '@angular/core';

// Self
	// Interfaces
		import { Task, TaskStatus } from 'src/app/interfaces/task.interface';

@Component({
	selector: '[task-list-item]',
	templateUrl: './task-list-item.component.html',
	styleUrls: ['./task-list-item.component.scss'],
})
export class TaskListItemComponent implements OnInit {
	@Input("task") private _task: Task;
	get task() { return this._task; }

	@Input("disable") private _disable: boolean;
	get disable() { return this._disable; }

	get taskStatusText() {
		return TaskStatus[this.task.status];
	}

	get showButton(): boolean {
		return this._task.status !== TaskStatus.Done;
	}

	get buttonStyle(): String {
		let style = "";
		switch (this._task.status) {
			case TaskStatus.Done:
				style = ""
				break;
			default:
				style = "d-none"
				break;
		}
		return style;
	}

	constructor() { }

	ngOnInit() { }	
}
