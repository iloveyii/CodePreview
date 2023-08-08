// ----------------------------------
// Package Import
// ----------------------------------
import express from "express";
import cors from "cors";
import compression from "compression";
import bodyParser from "body-parser";
import expressLayouts from "express-ejs-layouts";
import morgan from "morgan";
import helmet from "helmet";
import path from "path";

const fileUpload = require("express-fileupload");

// ----------------------------------
// Middleware Import
// ----------------------------------
import { errorHandler } from "./middlewares/error_handler";
import { notFound } from "./middlewares/not_found";

// ----------------------------------
// Routes Import
// ----------------------------------
import login from "./routes/login";
import index from "./routes/index";
import user from "./routes/user";
import make from "./routes/make";
import product from "./routes/product";
import service from "./routes/service";
import extra from "./routes/extra";
import order from "./routes/order";
import setting from "./routes/setting";
import backup from "./routes/backup";

// ----------------------------------
// Connect to DB
// ----------------------------------
const dialect = "mongodb"; // process.env.DB_DIALECT || "mongodb";

// ----------------------------------
// Express configuration
// ----------------------------------
const app: any = express();
// app.use(function (req: any, res: any, next: any) {
//   res.setHeader(
//     "Content-Security-Policy",
//     "script-src '*' 'unsafe-inline' http://localhost:6600"
//   );
//   return next();
// });
app.use(express.json({ limit: "50mb" }));
app.use(cors());
app.use(compression());
app.use(express.urlencoded({ limit: "50mb", extended: true }));
app.use(express.static(path.join(__dirname, "../../", "admin", "build")));
app.use(bodyParser.urlencoded({ limit: "50mb", extended: true }));
app.use(bodyParser.json({ limit: "50mb" }));
app.use(cors({ origin: "*", optionsSuccessStatus: 200 }));
app.use(
  fileUpload({
    createParentPath: true,
    limits: { fileSize: 50 * 1024 * 1024 },
  })
);

// ----------------------------------
// Security - header
// ----------------------------------
app.use(helmet());

// ----------------------------------
// Logging
// ----------------------------------
app.use(morgan("common"));

// ----------------------------------
// EJS Layouts
// ----------------------------------
app.use(expressLayouts);
app.set("views", path.resolve(__dirname, "views", "build"));
console.log("views", path.resolve(__dirname, "views", "build"));
app.set("view engine", "ejs");

// ----------------------------------
// API Routes
// ----------------------------------
app.use("/api/v1/users", user);
app.use("/api/v1/logins", login);
app.use("/api/v1/users", user);

app.use("/api/v1/makes", make);
app.use("/api/v1/products", product);
app.use("/api/v1/services", service);
app.use("/api/v1/extras", extra);
app.use("/api/v1/orders", order);
app.use("/api/v1/settings", setting);
app.use("/api/v1/backups", backup);

app.use("/",  index);
app.use("/*",  index);

// ----------------------------------
// Not found - 404
// ----------------------------------
app.use(notFound);

// ----------------------------------
// Error handling
// ----------------------------------
app.use(errorHandler);

// ----------------------------------
// Export app
// ----------------------------------
export default app;
