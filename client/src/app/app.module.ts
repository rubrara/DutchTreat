import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { AppComponent } from "./app.component";
import ProductListView from "./views/productListView.component";
import { Store } from "./services/store.service";
import { HttpClientModule } from "@angular/common/http";
import { CartView } from "./views/cartView.component";
import { ShopPage } from "./pages/shopPage.component";
import router from "./router";
import { CheckoutPage } from "./pages/checkout.component";
import { LoginPage } from "./pages/loginPage.component";
import { AuthActivator } from "./services/authActivator.service";
import { FormsModule } from "@angular/forms";

@NgModule({
  declarations: [
    AppComponent,
    ProductListView,
    CartView,
    ShopPage,
    CheckoutPage,
    LoginPage,
  ],
  imports: [BrowserModule, HttpClientModule, FormsModule, router],
  providers: [Store, AuthActivator],
  bootstrap: [AppComponent],
})
export class AppModule {}
