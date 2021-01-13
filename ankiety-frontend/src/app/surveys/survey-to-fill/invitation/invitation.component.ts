import { Component, OnInit, Input } from "@angular/core";
import { Router } from "@angular/router";

@Component({
  selector: 'app-invitation',
  templateUrl: './invitation.component.html',
  styleUrls: ['./invitation.component.css']
})
export class InvitationComponent implements OnInit {
  @Input() invitation: any;
  sendDate: any;
  expirationDate: any;

  constructor(private router: Router) { }

  ngOnInit() {
    this.sendDate = this.transformDate(this.invitation.sendDate);
    if (this.invitation.expirationDate)
      this.expirationDate = this.transformDate(this.invitation.expirationDate);
  }

  onInvitationClick() {
    this.router.navigate(["user/surveys/fill/", this.invitation.invitationId]);
  }

  transformDate(invitationDate: any) {
    var date = new Date(invitationDate);

    let dd = date.getUTCDate().toString();
    let mm = date.getMonth().toString();
    let yy = date.getFullYear().toString();
    let hh = date.getHours().toString();
    let min = date.getMinutes().toString();

    var text = new Date(Number(yy), Number(mm), Number(dd), Number(hh), Number(min)).toLocaleString().replace(',', ' ').replace('.', '/').replace('.', '/');
    return text.slice(0, text.length - 3).padStart(17, '0');
  }
}