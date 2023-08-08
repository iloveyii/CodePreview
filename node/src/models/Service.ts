import Mongo from "./base/Mongo";
import { Database } from "./base/Database";


type ServiceT = {
    id?: string;
    name: string;
    price: string;
    qty: number
};

const COLLECTION = "services";

class Service extends Mongo {
    constructor(private service?: ServiceT) {
        super(COLLECTION, service);
    }

    rules() {
        return {
            name: "required",
            price: "required"
        };
    }
}

export default Service;
