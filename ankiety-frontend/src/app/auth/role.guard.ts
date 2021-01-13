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

  parseJwt (token: string) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
      return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
  };

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    var userToken = localStorage.getItem('token');

    if (userToken) {
      if (route.data.roles && route.data.roles.indexOf(this.parseJwt(userToken).role) === -1) {
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
