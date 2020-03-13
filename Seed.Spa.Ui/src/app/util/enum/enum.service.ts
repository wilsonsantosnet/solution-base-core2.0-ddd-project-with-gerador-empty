import { Injectable, EventEmitter } from '@angular/core'

export class EEnumService {


  public static _eRole: ERole;

  public static getRole() {

    if (!this._eRole)
      this._eRole = new ERole();

    return this._eRole;
  }

};



export class ERole {

  public ADMIN: string;

  constructor() {
    this.ADMIN = "ADMIN";
  }
};


