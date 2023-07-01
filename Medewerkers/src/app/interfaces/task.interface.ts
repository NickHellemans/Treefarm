import { Zone } from "./zone.interface";

export interface Task {
	dateCreated?: Date,
	dateStart?: Date,
	dateEnd?: Date,
	datePlanned: Date,
	description: string,
	duration?: number,
	id: number,
	name: string,
	priority: number,
	status: TaskStatus
	zone: Zone,
}

export enum TaskStatus {
	ToDo,
	InProgress,
	Paused,
	Done
}
