import { Component, OnInit } from "@angular/core";
import { AuthService } from "./shared/auth.service";
import { UserDetails } from "./models/userDetails";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = "System ankietowy";

  constructor(private authService: AuthService) { }

  ngOnInit() {
    if (localStorage.getItem("token") != null) {
      this.authService.getUserProfile().subscribe((res: UserDetails) => {
        this.authService.user = res;
      });
    }
  }
}
