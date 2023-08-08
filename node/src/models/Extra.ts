import Mongo from "./base/Mongo";


type ExtraT = {
    id?: string;
    name: string;
    price: string;
};

const COLLECTION = "extras";

class Extra extends Mongo {
    constructor(private extra?: ExtraT) {
        super(COLLECTION, extra);
    }

    rules() {
        return {
            name: "required",
            price: "required"
        };
    }
}

export default Extra;
