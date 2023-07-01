// Angular
import { Injectable } from '@angular/core';

// RxJS
import { BehaviorSubject } from 'rxjs';

// Self
	// Env
		import { environment } from 'src/environments/environment';

	// Services
		import { ToastService } from './ionic/toast.service';
		import { HttpClientService } from './httpService/http-client.service';
		import { LoginService } from './login.service';

	// Enums
		import { ApiPath } from '../enums/api-path.enum';

	// Interfaces
		import { Task, TaskStatus } from '../interfaces/task.interface';

@Injectable({
	providedIn: 'root'
})
export class TaskService {
	private _tasks$ = new BehaviorSubject<Task[]>(null);
	get tasks$() { return this._tasks$; }
	get tasks() { return this._tasks$.value; }

	private _task_active$ = new BehaviorSubject<Task>(null);
	get task_active$() { return this._task_active$; }
	get task_active() { return this._task_active$.value; }

	private _currentPriority$ = new BehaviorSubject<number>(0);
	get currentPriority$() { return this._currentPriority$; }
	get currentPriority() { return this._currentPriority$.value; }

	constructor(
		private httpClientService: HttpClientService,
		private loginService: LoginService,
		private toastService: ToastService
	) {
	}

	// Public
		public getTasks(): void {
			this.loginService.getEmployee()
				.subscribe(
					(response: Task[]) => {
						const tasks: Task[] = [];

						this._resetActiveAndPriority();

						response.forEach((task: Task) => {
							const _task: Task = {
								id: task.id,
								dateCreated: new Date(task.dateCreated),
								dateEnd: new Date(task.dateEnd),
								datePlanned: new Date(task.datePlanned),
								dateStart: new Date(task.dateStart),
								description: task.description,
								duration: task.duration,
								name: task.name,
								priority: task.priority,
								status: task.status,
								zone: task.zone
							}
							
							if (_task.status === TaskStatus.InProgress || _task.status === TaskStatus.Paused) {
								this._task_active$.next(_task);
								this._currentPriority$.next(_task.priority);
							}
							else {
								this._updateCurrentPriority(_task);
							}

							tasks.push(_task);
						});

						this._tasks$.next(tasks);
					}
				);
		}

		public getTask(id: number): Task {
			return this._tasks$.value.find((task: Task) => {
				if (task.id === id) {
					return task;
				}
			});
		}

		// Status
			public taskCanUpdate(task: Task): boolean {
				const date_now = new Date();
				const date_today = new Date(date_now.getFullYear(), date_now.getMonth(), date_now.getDate());

				let canUpdate: boolean = true;
				let toast_text: string;

				const task_active = this.task_active;
				if (task_active && task_active.id !== task.id) {
					const datePlanned = task_active.datePlanned;
					toast_text = `Er is al een taak actief op ${datePlanned.getDate()}/${datePlanned.getMonth() + 1}/${datePlanned.getFullYear()}: ${task_active.name}`;
					canUpdate = false;
				}
				else if (task.datePlanned.getTime() !== date_today.getTime()) {
					toast_text = "Deze taak is niet gepland voor vandaag!";
					canUpdate = false;
				}
				else if (task.priority !== this.currentPriority) {
					toast_text = "Kies een taak met hogere prioriteit!";
					canUpdate = false;
				}

				if (!canUpdate) {
					this.toastService.presentToast(toast_text);
				}
				return canUpdate;
			}

			public taskStart(task: Task): void {
				let newStatus: TaskStatus;
				let path: string;
				switch (task.status) {
					case TaskStatus.ToDo:
						newStatus = TaskStatus.InProgress;
						path = ApiPath.TASK_START;
						break;
					case TaskStatus.InProgress:
						newStatus = TaskStatus.Done;
						path = ApiPath.TASK_STOP;
						break;
				}
				path += `/${task.id}`;
				this._updateTaskStatus(task, newStatus, path);
			}

			public taskPause(task: Task): void {
				let newStatus: TaskStatus;
				let path: string;
				switch (task.status) {
					case TaskStatus.Paused:
						newStatus = TaskStatus.InProgress;
						path = ApiPath.TASK_START;
						break;
					case TaskStatus.InProgress:
						newStatus = TaskStatus.Paused;
						path = ApiPath.TASK_PAUSE;
						break;
				}
				path += `/${task.id}`;
				this._updateTaskStatus(task, newStatus, path);
			}


	// Private
		private _updateTaskStatus(task_old: Task, status_new: TaskStatus, path: string): boolean {
			const tasks = this.tasks;

			if (!this.taskCanUpdate(task_old)) {
				return false;
			}

			const loadingMessage = this._getLoadingMessage(status_new);

			this.httpClientService.get(path, loadingMessage).subscribe((response) => {
				if (!environment.production) {
					console.log("updateTaskStatus", response)
				}

				const task_updated = tasks.find((task: Task) => {
					if (task.id === task_old.id) {
						task_old.status = response.status;
						return task_old;
					}
				});

				// Update active task & priorities
				if (task_updated.status === TaskStatus.Done) {
					this._resetActiveAndPriority();
					tasks.forEach((task: Task) => this._updateCurrentPriority(task));
				}
				else {
					this._task_active$.next(task_updated);
					this._currentPriority$.next(task_updated.priority);
				}

				// Update task list
				this._tasks$.next(tasks);

				return true;
			});

			return true;
		}	
		
		private _updateCurrentPriority(task: Task): void {
			const date_now = new Date();
			const date_today = new Date(date_now.getFullYear(), date_now.getMonth(), date_now.getDate());
			if (task.status === TaskStatus.InProgress || task.status === TaskStatus.Paused) {
				this.task_active$.next(task);
				this._currentPriority$.next(task.priority);
			}
			else if (task.status !== TaskStatus.Done && task.datePlanned.getTime() === date_today.getTime() ) {
				const currentPriority = this.currentPriority;

				if (currentPriority === null || currentPriority === 0) {
					this._currentPriority$.next(task.priority);
				}
				else {
					if (currentPriority > task.priority && task.priority !== 0) {
						this._currentPriority$.next(task.priority);
					}
				}
			}
		}

		private _getLoadingMessage(taskStatus: TaskStatus): string {
			let loadingMessage = "Taak ";
			switch (taskStatus) {
				case TaskStatus.ToDo:
					loadingMessage += "starten";
					break;
				case TaskStatus.InProgress:
					loadingMessage += "pauzeren";
					break;
				case TaskStatus.Paused:
					loadingMessage += "hervatten";
					break;
				case TaskStatus.Done:
					loadingMessage += "beëindigen";
					break;
			}

			return loadingMessage;
		}

		private _resetActiveAndPriority(): void {
			this._task_active$.next(null);
			this._currentPriority$.next(0);
		}
}
