import Mongo from "./base/Mongo";
import { Database } from "./base/Database";


type BackupT = {
    id?: string;
    p_id: number;
    message: string;
    dated: string;
    success: boolean;
    download_url: string;
    start: boolean;
};


const COLLECTION = "backups";

class Backup extends Mongo {
    constructor(private product?: BackupT) {
        super(COLLECTION, product);
    }

    rules() {
        return {
            message: "required"
        };
    }
}

export default Backup;
