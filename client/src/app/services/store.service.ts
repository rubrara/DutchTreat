import { Product } from "./../shared/Product";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, map } from "rxjs";
import { Order, OrderItem } from "../shared/Order";
import { LoginRequest, LoginResults } from "../shared/LoginResults";

@Injectable()
export class Store {
  constructor(private http: HttpClient) {}

  public products: Product[] = [];
  public order: Order = new Order();
  public token = "";
  public expiration = new Date();

  loadProducts(): Observable<void> {
    return this.http.get<[]>("/api/products").pipe(
      map((data) => {
        this.products = data;
        return;
      })
    );
  }

  get loginRequired(): boolean {
    return this.token.length === 0 || this.expiration > new Date();
  }

  addToOrder(product: Product) {
    let newItem: OrderItem;

    newItem = this.order.items.find((i) => i.productId == product.id)!;

    if (newItem != null) {
      newItem.quantity++;
    } else {
      newItem = new OrderItem();

      newItem.productId = product.id;
      newItem.productTitle = product.title;
      newItem.productArtId = product.artId;
      newItem.productArtist = product.artist;
      newItem.productCategory = product.category;
      newItem.productSize = product.size;
      newItem.unitPrice = product.price;
      newItem.quantity = 1;

      this.order.items.push(newItem);
    }
  }

  login(creds: LoginRequest) {
    return this.http.post<LoginResults>("/account/createToken", creds).pipe(
      map((data) => {
        this.token = data.token;
        this.expiration = data.expiration;
      })
    );
  }
  checkout() {
    const headers = new HttpHeaders().set(
      "Authorization",
      `Bearer ${this.token}`
    );

    return this.http
      .post("/api/orders", this.order, {
        headers: headers,
      })
      .pipe(
        map(() => {
          this.order = new Order();
        })
      );
  }
}
