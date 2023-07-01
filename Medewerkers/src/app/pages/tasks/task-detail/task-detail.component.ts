import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';

// RxJS
import { Subscription } from 'rxjs';

// Self
	// Env
		import { environment } from 'src/environments/environment';		

	// Services
		import { HttpClientService } from 'src/app/services/httpService/http-client.service';
		import { TaskService } from 'src/app/services/task.service';
		import { CodeScannerService } from 'src/app/services/code-scanner.service';

	// Interfaces
		import { Task, TaskStatus } from 'src/app/interfaces/task.interface';

	// Enums
		import { Routes } from 'src/app/enums/routes.enum';

@Component({
	selector: 'app-task-detail',
	templateUrl: './task-detail.component.html',
	styleUrls: ['./task-detail.component.scss'],
})
export class TaskDetailComponent implements OnInit, OnDestroy {	
	private _subscriptions: Subscription[];

	private _task: Task;
	get task() { return this._task; }

	private _task_active: Task;
	get task_active() { return this._task_active; }	

	get task_status(): String {
		let status: String;
		switch(this.task.status) {
			case TaskStatus.ToDo:
				status = "Gepland";
				break;
			case TaskStatus.InProgress:
				status = "Actief";
				break;
			case TaskStatus.Paused:
				status = "Gepauzeerd";
				break;
			case TaskStatus.Done:
				status = "Voltooid";
				break;
		}
		return status;
	}

	constructor(
		private router: Router,
		private activatedRoute: ActivatedRoute,

		private taskService: TaskService,
		private codeScannerService: CodeScannerService,
		private httpClientService: HttpClientService
	) { }

	ngOnInit(): void {
		this._subscriptions = [
			this.activatedRoute.params.subscribe((params: Params) => {
				this._task = this.taskService.getTask(Number(params.id));
			}),
			this.taskService.task_active$.subscribe((task_active: Task) => {
				this._task_active = task_active;
			}),
		];
	}

	ngOnDestroy(): void {
		this._subscriptions.forEach((subscription: Subscription) => { subscription.unsubscribe(); });
	}

	public goBack($event: Event) {
		this.router.navigate([Routes.Tasks]);
	}

	public startScan() {
		this.codeScannerService.scannerStart();
	}

	public openInstructions() {
		const url = `${environment.dashboard.baseUrl + environment.dashboard.fileLocation}${this.task.zone.tree.instructionsUrl}`;
		this.httpClientService.open(url);
	}
}
