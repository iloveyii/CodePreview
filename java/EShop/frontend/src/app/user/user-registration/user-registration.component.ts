import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MatSnackBar} from "@angular/material/snack-bar";
import {Router} from "@angular/router";
import {Subscription} from "rxjs";
import {UserService} from 'src/app/core/_services/user.service';
import {UserDto} from 'src/app/shared/_models/dtos/user-dto';
import {AuthService} from "../../core/_services/auth.service";

/**
 TODO: Fix rerouting back to homepage and fix hashing of password
 */

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.css']
})
export class UserRegistrationComponent implements OnInit, OnDestroy {

  user!: UserDto;
  form!: FormGroup;
  flag: boolean = true;

  private readonly subscriptions: Subscription = new Subscription();

  constructor(private fb: FormBuilder,
              private userService: UserService,
              private authService: AuthService,
              private router: Router,
              private matSnackBar: MatSnackBar) {
    this.user = new UserDto();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      email: [null, [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
      firstName: [null],
      password: [null, [Validators.required, Validators.minLength(6)]],
    });
  }

  registration(): void {
    this.user.email = this.form.controls['email'].value;
    this.user.password = this.form.controls['password'].value;
    this.user.isAdmin = false;
    this.subscriptions.add(
      this.userService.save(this.user).subscribe(result => {
        if (this.user.email && this.user.password) {
          this.subscriptions.add(this.authService.signIn({
            username: this.user.email,
            password: this.user.password
          })
            .subscribe(result => {
              this.authService.setLocalStorage(result);
              this.matSnackBar.open('Registration successful', '', {
                duration: 2000,
              });
              this.goToHome();
            }));
        }
      })
    );
  }

  public goToHome() {
    this.router.navigate(['/']);
  }

}
