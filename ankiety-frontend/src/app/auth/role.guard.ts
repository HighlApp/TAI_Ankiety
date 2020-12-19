import { Injectable } from "@angular/core";
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot
} from "@angular/router";
import { Router } from "@angular/router";
import { AuthService } from "../shared/auth.service";
import { UserDetails } from '../models/userDetails';

@Injectable({
  providedIn: "root"
})
export class RoleGuard implements CanActivate {

  constructor(private router: Router, private authService: AuthService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const currentUser = this.authService.user;

    if (currentUser) {
      if (route.data.roles && route.data.roles.indexOf(currentUser.role) === -1) {
        this.router.navigate(["/"]);
        return false;
      }
      return true;
    }

    if (localStorage.getItem("token") != null) {
      this.authService.getUserProfile().subscribe((res: UserDetails) => {
        this.authService.user = res;
      });

      if (route.data.roles && route.data.roles.indexOf(currentUser.role) === -1) {
        this.router.navigate(["/"]);
        return false;
      }
      return true;
    }

    this.router.navigate(["/login"]);
    localStorage.clear();
    return false;
  }
}
