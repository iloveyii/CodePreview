import Mongo from "./base/Mongo";
import { Database } from "./base/Database";


type ProductT = {
    id?: string;
    name: string;
    make_id: string;
};

const COLLECTION = "products";

class Product extends Mongo {
    constructor(private product?: ProductT) {
        super(COLLECTION, product);
    }

    rules() {
        return {
            name: "required",
            make_id: "required"
        };
    }
}

export default Product;
