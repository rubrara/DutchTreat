import { Component } from "@angular/core";
import { Store } from "../services/store.service";
import { Router } from "@angular/router";
import { LoginRequest } from "../shared/LoginResults";

@Component({
  selector: "login-page",
  templateUrl: "loginPage.component.html",
  styleUrls: ["loginPage.component.css"],
})
export class LoginPage {
  constructor(public store: Store, private router: Router) {}

  public creds: LoginRequest = {
    username: "",
    password: "",
  };

  public errorMessage = "";

  onLogin() {
    this.store.login(this.creds).subscribe(
      () => {
        // Success
        if (this.store.order.items.length > 0) {
          this.router.navigate(["checkout"]);
        } else {
          this.router.navigate([""]);
        }
      },
      (error) => {
        this.errorMessage = "Failed to login";
      }
    );
  }
}
