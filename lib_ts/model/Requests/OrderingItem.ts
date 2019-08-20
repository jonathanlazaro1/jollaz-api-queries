export default class OrderingItem {
  name: string;
  descending: boolean;

  constructor(name: string, descending = false) {
    this.name = name;
    this.descending = descending;
  }
}
