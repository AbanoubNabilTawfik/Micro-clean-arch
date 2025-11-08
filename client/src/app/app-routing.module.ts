import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from '../home/home.component';
import { NotFoundComponent } from '../core/pages/not-found/not-found.component';
import { NotAuthenticatedComponent } from '../core/pages/not-authenticated/not-authenticated.component';
import { ServerErrorComponent } from '../core/pages/server-error/server-error.component';
import { skip } from 'rxjs';
import { SigninRedirectCallbackComponent } from '../account/signin-redirect-callback/signin-redirect-callback.component';
import { SignoutRedirectCallbackComponent } from '../account/signout-redirect-callback/signout-redirect-callback.component';
import { AuthGuard } from '../core/guards/auth.guard';

const routes: Routes = 
[
 {path:'', component:HomeComponent,data:{breadcrumb:'Home'}},
 {path:'store',loadChildren:()=>import('../store/store.module').then(m=>m.StoreModule),data:{breadcrumb:'Store'}},
 {path:'basket',loadChildren:()=>import('../basket/basket.module').then(m=>m.BasketModule),data:{breadcrumb:'Basket'}},
 {path:'account',loadChildren:()=>import('../account/account.module').then(m=>m.AccountModule),data:{breadcrumb:{skip:true}}},
 {path:'checkout', canActivate:[AuthGuard],loadChildren:()=>import('../checkout/checkout.module').then(m=>m.CheckoutModule),data:{breadcrumb:'Checkout'}},
 {path:'signin-callback',component:SigninRedirectCallbackComponent},
 {path:'signout-callback',component:SignoutRedirectCallbackComponent},
 {path:'not-found' ,component:NotFoundComponent},
 {path:'not-authenticated' ,component:NotAuthenticatedComponent},
 {path:'server-error' ,component:ServerErrorComponent},
 {path:'**' ,redirectTo:'' ,pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
