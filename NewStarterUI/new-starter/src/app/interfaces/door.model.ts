export interface Door {
    doorId: number;
    doorName: string;
}

export class Door implements Door {
    doorId: number = 0;
    doorName: string = "";
}

