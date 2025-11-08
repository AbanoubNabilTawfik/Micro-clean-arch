import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CoreRoutingModule } from './core-routing.module';
import { NavbarComponent } from './components/navbar/navbar.component';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { NotAuthenticatedComponent } from './pages/not-authenticated/not-authenticated.component';
import { ServerErrorComponent } from './pages/server-error/server-error.component';
import { HeaderComponent } from './components/header/header.component';
import {NgxSpinnerModule} from 'ngx-spinner';
import {
  BreadcrumbComponent,
  BreadcrumbItemDirective,
  BreadcrumbService
} from 'xng-breadcrumb';
@NgModule({
  declarations: [
    NavbarComponent,
    NotFoundComponent,
    NotAuthenticatedComponent,
    ServerErrorComponent,
    HeaderComponent
  ],
  imports: [
    CommonModule,
    CoreRoutingModule,
    CollapseModule.forRoot(),
    BsDropdownModule.forRoot(),
    BreadcrumbComponent,
    BreadcrumbItemDirective,
    NgxSpinnerModule
  ],
  exports:[
    NavbarComponent,
     NotFoundComponent,
     NotAuthenticatedComponent,
     ServerErrorComponent,
     HeaderComponent,
     NgxSpinnerModule
  ],
  providers:[
    BreadcrumbService
  ]
})
export class CoreModule { }
