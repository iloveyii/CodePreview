import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {UserRegistrationComponent} from './user-registration/user-registration.component';
const routes: Routes = [
  {
    path: '',
    component: UserRegistrationComponent,
  },
  {
    path: '**',
    redirectTo: '/hello',
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
