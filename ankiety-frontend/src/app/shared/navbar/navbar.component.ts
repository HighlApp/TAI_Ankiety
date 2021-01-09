import {Component, Inject, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/dialogs/confirmation-dialog/confirmation-dialog.component';
import { AuthService } from "src/app/shared/auth.service";


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
                this.router.navigate(["/login"]);
            }
        });
    }

    onChangePassword() {
        console.log('onChangePassword() called!');
    }
}
