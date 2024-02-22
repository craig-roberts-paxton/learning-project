export interface User {
    userId: number;
    userName: string;
    firstName: string;
    lastName: string;
    pinCode: string | undefined;
}

export class User implements User {
    userId: number = 0;
    userName: string = "";
    firstName: string = "";
    lastName: string = "";
    pinCode: string | undefined = "";
}

