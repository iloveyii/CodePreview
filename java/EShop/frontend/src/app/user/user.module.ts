import {CommonModule} from '@angular/common';
import {NgModule} from '@angular/core';
import {CoreModule} from "../core/core.module";
import {UserRoutingModule} from './user-routing.module';
import {UserRegistrationComponent} from './user-registration/user-registration.component';

@NgModule({
  declarations: [
    UserRegistrationComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    CoreModule
  ],
  exports:[
    UserRegistrationComponent
  ]
})
export class UserModule { }
