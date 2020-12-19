import { Injectable } from "@angular/core";
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot
} from "@angular/router";
import { Router } from "@angular/router";
import { AuthService } from "../shared/auth.service";

@Injectable({
  providedIn: "root"
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router, private authService: AuthService) { }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (localStorage.getItem("token") != null) {
      return true;
    } else {
      this.router.navigate(["/login"]);
      return false;
    }
  }
}
