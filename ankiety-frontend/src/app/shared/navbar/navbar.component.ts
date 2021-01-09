import {Component, Inject, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/dialogs/confirmation-dialog/confirmation-dialog.component';
import { AuthService } from "src/app/shared/auth.service";
import {ChangePasswordComponent} from "../../user/change-password/change-password.component";

@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
    constructor(
        private router: Router,
        public dialog: MatDialog,
        public authService: AuthService) {
    }

    ngOnInit(): void {
    }

    onLogout() {
        const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
            height: "140px",
            width: "500px",
            data: {"dialogText": "Czy na pewno chcesz się wylogować ?"}
        });

        dialogRef.afterClosed().subscribe((logout: boolean) => {
            if (logout) {
                localStorage.removeItem("token");
                localStorage.removeItem("user");
                this.router.navigate(["/login"]);
            }
        });
    }

    onChangePassword() {
        const dialogRef = this.dialog.open(ChangePasswordComponent, {
            maxHeight: "700px",
            width: "600px"
        });
    }

    getUserEmail() {
        return localStorage.getItem('user');
    }
}
