export class OrderItem {
  id: any;
  quantity: any;
  unitPrice: any;
  productId: any;
  productCategory: any;
  productSize: any;
  productTitle: any;
  productArtist: any;
  productArtId: any;
}
export class Order {
  orderId: any;
  orderDate: Date = new Date();
  orderNumber: string = Math.random().toString(36).substring(0, 5);
  items: OrderItem[] = [];

  get subtotal(): number {
    const res = this.items.reduce((tot, val) => {
      return tot + val.unitPrice * val.quantity;
    }, 0);
    return res;
  }
}
