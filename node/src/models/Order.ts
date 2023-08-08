import Mongo from "./base/Mongo";
import { Database } from "./base/Database";
import { ConditionI } from "../interfaces";

type ServiceT = {
  id?: string;
  name: string;
};

type OrderT = {
  id?: string;
  service_number: string;
  name: string;
  telephone: string;
  code: string;
  fix: {
    list: ServiceT[];
  };
  technical_info: string;
  customer_picked: false;
  date_picked: string;
  time_picked: string;
  info: string;
  date: string;
  time: string;
  screen_cover: string;
  express: string;
  price: string;
  payment_method: string;
};

const COLLECTION = "orders";

class Order extends Mongo {
  constructor(private order?: OrderT) {
    super(COLLECTION, order);
  }

  rules() {
    return {
      service_number: "required",
      name: "required",
      telephone: "required",
      price: "required",
    };
  }

  async read(condition?: ConditionI) {
    const db = await this.database.db();
    const collection = await db.collection(COLLECTION);
    const model = await collection
      .find(condition?.where)
      .sort({ _id: -1, date_picked: 1, time_picked: 1 });
    const arr = await model.toArray();
    if (arr.length > 0) {
      this.setResponse(true, arr);
    } else {
      this.setResponse(false, [
        "No record found with condition " + JSON.stringify(condition?.where),
      ]);
    }
    return this;
  }
}

export default Order;
