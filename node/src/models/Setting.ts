import Mongo from "./base/Mongo";
import { Database } from "./base/Database";
import Condition from "./base/Condition";
import fs from "fs";
import path from "path";

type SettingT = {
    id?: string;
    token: Object;
    opening_hours: Object;
    tables: Object;
    page_setup: Object;
};

const COLLECTION = "settings";

class Setting extends Mongo {
    constructor(private service?: SettingT) {
        super(COLLECTION, service);
    }

    rules() {
        return {
            token: "required",
            opening_hours: "required",
            tables: "required",
            page_setup: "required"
        };
    }

    async create(): Promise<any> {
        const condition = new Condition({where: {}});
        await this.deleteMany(condition);
        await super.create();
        let css = await this.generate_css("token");
        css += await this.generate_css("opinion");
        await this.save_css_file(css);
        return this;
    }

    async save_css_file(css: string): Promise<any> {
        const file_path = path.resolve( __dirname, "../../../admin/public/css/print.css");
        fs.writeFileSync(file_path, css);
        return this;
    }

    async generate_css(id: string): Promise<any> {
        const {dimensions, margins} = this.data.page_setup[id];
        const class_suffix = id === "token" ? "receipt" : "opinion";
        const css = `
            @media print {
                .print-${class_suffix} {
                    width: ${dimensions.width}mm;
                    height: ${dimensions.height}mm;
                    margin: ${margins.top}mm ${margins.left}mm ${margins.bottom}mm ${margins.left}mm;
                }
            }
        `;
        return css;
    }
}

export default Setting;
