import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Invitation } from "../models/invitation";
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: "root"
})
export class InvitationService {
  private readonly BaseURI: string;

  constructor(private http: HttpClient) {
    this.BaseURI = environment.apiUrl + '/invitations';
  }

  getInvitations(): any {
    return this.http.get(this.BaseURI + "/all");
  }

  getUsersForInvitation(surveyId: string): any {
    return this.http.get(this.BaseURI + "/users/" + surveyId);
  }

  sendInvitations(invitations: Invitation[]) {
    return this.http.post(this.BaseURI, invitations);
  }

  getUserInvitations() {
    return this.http.get(this.BaseURI);
  }

  inviteGroup(id: string[], invitation: Invitation) {
    return this.http.post(this.BaseURI + "/group/" + id, invitation);
  }

  deleteInvitation(id: number) {
    return this.http.delete(this.BaseURI + "/" + id);
  }
}
