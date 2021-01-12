import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { UserDetails } from '../models/userDetails';
import { environment } from '../../environments/environment';
@Injectable({
  providedIn: "root"
})
export class AuthService {
  user: UserDetails;

  constructor(private http: HttpClient) { }

  getUserProfile() {
    return this.http.get(environment.apiUrl + "/UserProfile");
  }
}
