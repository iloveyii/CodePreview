import dotenv from "dotenv";
import socket from "socket.io";

import app from "./src/app";

const session = require("express-session");

import { Database } from "./src/models/base/Database";

// ----------------------------------
// Environment setup
// ----------------------------------
dotenv.config({path: ".env"});
const {
    REACT_APP_serverIp: SERVER = "",
    REACT_APP_PORT: PORT = 5500,
    SESS_NAME = "sid",
    SESS_SECRET = "top-secret",
    SESS_LIFETIME = 1000 * 60 * 60 * 2, // 2 hrs
    DB_NAME = "receipt",
    DB_HOST = "receipt_mongo",  
    DB_USER = "",
    DB_PASS = ""
} = process.env;

app.use(
    session({
        name: SESS_NAME,
        resave: false,
        saveUninitialized: false,
        secret: SESS_SECRET,
        cookie: {
            maxAge: SESS_LIFETIME,
            sameSite: "none",
            secure: false,
        }
    }));

// ----------------------------------
// Connect to DB
// ----------------------------------
const db = new Database(DB_HOST, DB_NAME, DB_USER, DB_PASS);
db.connect();
console.log("DB DB_NAME, DB_USER, DB_PASS: ", DB_NAME, DB_USER, DB_PASS);


// ----------------------------------
// Express server
// ----------------------------------
const server = app.listen(PORT, () => {
    console.log(
        "  SERVER is running on %s:%d in %s mode",
        SERVER,
        PORT,
        process.env.NODE_ENV ? process.env.NODE_ENV : "prod",
    );
    console.log("  To change these config(server and port), edit .env file\n");
    console.log("  Press CTRL-C to stop\n");
});

// ----------------------------------
// Socket IO
// ----------------------------------
export const io = socket(server);
io.on("connection", (client : any) => {
    console.log("Made socket connection with id ", client.id);
});

export default server;
