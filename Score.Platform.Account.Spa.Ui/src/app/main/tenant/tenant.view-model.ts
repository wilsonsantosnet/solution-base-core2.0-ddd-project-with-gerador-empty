export class Tenant {
    name : string;
    email : string;
    password : string;
    guidResetPassword : string;
    dateResetPassword : string;
    tenantId : number;
    programId : number;
    active : boolean;
    changePasswordNextLogin : boolean;
}