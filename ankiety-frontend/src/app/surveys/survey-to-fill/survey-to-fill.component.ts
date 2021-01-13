import { Component, OnInit } from "@angular/core";
import { Invitation } from "src/app/models/invitation";
import { InvitationService } from "src/app/shared/invitation.service";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: 'app-survey-to-fill',
  templateUrl: './survey-to-fill.component.html',
  styleUrls: ['./survey-to-fill.component.css']
})
export class SurveyToFillComponent implements OnInit {
  invitations: Invitation[];
  isLoading: boolean = true;

  constructor(
      private invitationService: InvitationService,
      private router: Router,
      private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.invitationService
        .getUserInvitations()
        .subscribe((res: any) => {
          (this.invitations = res);
          this.isLoading = false;
          if (this.invitations.length > 0) {
            this.setTimer();
          }
        });
  }

  onInvitationClick(invitation: any) {
    this.router.navigate(["user/surveys/fill/", invitation.invitationId]);
  }

  setTimer() {
    setInterval(
        () => {
          this.invitations.forEach(invitation => {
            if (invitation.expirationDate != undefined) {
              var now = new Date().getTime();
              var distance = new Date(invitation.expirationDate).getTime() - now;

              if (distance < 0) {
                this.invitations = this.invitations.filter(x => x != invitation);
              }
            }
          });
        },
        1000
    );
  }
}
