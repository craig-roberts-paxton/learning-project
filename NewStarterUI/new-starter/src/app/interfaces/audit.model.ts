export interface AuditRecord {
    doorName: string;
    userName: string;
    AuditDateTime: Date;
    AccessGranted: string;
}

export class AuditRecord implements AuditRecord {
    doorName: string = "";
    userName: string = "";
    AuditDateTime: Date = new Date();
    AccessGranted: string = "";
}