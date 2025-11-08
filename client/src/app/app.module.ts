import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from '../core/core.module';
import { HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { StoreModule } from '../store/store.module';
import { HomeModule } from '../home/home.module';
import { errorInterceptor } from '../core/interceptors/error.interceptor';
import { loadingInterceptor } from '../core/interceptors/loading.interceptor';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    CoreModule,
    HttpClientModule,
    StoreModule,
    HomeModule
  ],
  providers: [
    provideHttpClient(withInterceptors([errorInterceptor,loadingInterceptor]))
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
