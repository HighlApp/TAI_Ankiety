import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Invitation } from "../models/invitation";
import { environment } from '../../environments/environment';
import {InvitationRequest} from "../models/invitationRequest";

@Injectable({
  providedIn: "root"
})
export class InvitationService {
  private readonly BaseURI: string;

  constructor(private http: HttpClient) {
    this.BaseURI = environment.apiUrl + '/invitations';
  }

  getInvitations(): any {
    return this.http.get(this.BaseURI);
  }

  getUsersForInvitation(surveyId: string): any {
    return this.http.get(this.BaseURI + "/users/" + surveyId);
  }

  sendInvitations(invitations: InvitationRequest[]) {
    const invReq = {
      invitationDetails: invitations[0],
      usersId: invitations.map(inv => inv.userId)
    };

    return this.http.post(this.BaseURI, invReq);
  }

  getUserInvitations() {
    return this.http.get(this.BaseURI + "/my");
  }

  deleteInvitation(id: number) {
    return this.http.delete(this.BaseURI + "/" + id);
  }
}
