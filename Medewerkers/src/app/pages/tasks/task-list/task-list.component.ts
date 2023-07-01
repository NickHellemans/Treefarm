// Angular
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';

// RxJS
import { Subscription } from 'rxjs';

// Self
	// Services
		import { LoginService } from 'src/app/services/login.service';
		import { TaskService } from 'src/app/services/task.service';
		import { MenuService } from 'src/app/services/ionic/menu.service';
		import { CodeScannerService } from 'src/app/services/code-scanner.service';

	// Enums
		import { Routes } from 'src/app/enums/routes.enum';

	// Interfaces
		import { Task } from 'src/app/interfaces/task.interface';
		import { Employee } from 'src/app/interfaces/employee.interface';
		import { ObjectFilter } from 'src/app/interfaces/object-filter.interface';

@Component({
	selector: 'app-task-list',
	templateUrl: './task-list.component.html',
	styleUrls: ['./task-list.component.scss'],
})
export class TaskListComponent implements OnInit, OnDestroy {
	private _subscriptions: Subscription[];

	private _tasks: Task[];
	get tasks() { return this._tasks; }

	private _task_active: Task;
	get task_active() { return this._task_active; }

	private _employee: Employee;
	get employee_fullName(): string {
		return `- ${this._employee.firstName} ${this._employee.lastName}`;
	}

	get menuId(): string { return this.menuService.menuId; }

	private _isScanActive: boolean;
	get isScanActive() { return this._isScanActive; }

	private _dateSelected: Date = new Date(new Date().setHours(0, 0, 0, 0)); // Current date without time
	get dateSelected() { return this._dateSelected; }
	set dateSelected(date: Date) { this._dateSelected = date; }

	get dateSelected_time(): number { return this._dateSelected.getTime(); }

	constructor(
		private router: Router,

		private loginService: LoginService,
		private taskService: TaskService,
		private menuService: MenuService,
		private codeScannerService: CodeScannerService
	) { }

	// Lifecycle hooks
	ngOnInit(): void {
		this.getTasks();
		this._subscriptions = [
			this.taskService.tasks$.subscribe((tasks: Task[]) => {
				this._tasks = tasks;
			}),
			this.taskService.task_active$.subscribe((task_active: Task) => {
				this._task_active = task_active;
			}),
			this.codeScannerService.isScanActive$.subscribe((isScanActive: boolean) => {
				this._isScanActive = isScanActive;
			}),
			this.loginService.employee$.subscribe((employee: Employee) => {
				this._employee = employee;
			})
		];
	}

	ngOnDestroy(): void {
		this._subscriptions.forEach((subscription: Subscription) => { subscription.unsubscribe(); });
	}

	ionViewDidEnter(): void {
		this.menuService.menuEnable();
	}

	ionViewDidLeave(): void {
		this.menuService.menuDisable();
	}
	// /Lifecycle hooks

	public getTasks() {
		this.taskService.getTasks();
	}

	public getTask(id: number) {
		this.router.navigate([Routes.Tasks, id]);
	}

	public getTaskFilter(priority: number): ObjectFilter[] {
		return [{'field': 'status', 'value': priority}, {'field': 'datePlanned', 'value': this.dateSelected_time}]
	}

	public ionRefresherHandler(event) {
		this.taskService.getTasks();
		event.target.complete();
	};
}
