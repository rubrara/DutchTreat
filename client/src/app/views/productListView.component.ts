import { Component, OnDestroy, OnInit } from "@angular/core";
import { Store } from "../services/store.service";

@Component({
  selector: "product-list",
  templateUrl: "productListView.component.html",
  styleUrls: ["productListView.component.css"],
})
export default class ProductListView implements OnInit, OnDestroy {
  constructor(public store: Store) {}

  ngOnInit() {
    this.store.loadProducts().subscribe(() => {});
  }

  ngOnDestroy() {}
}
