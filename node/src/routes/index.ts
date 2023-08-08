import express from "express";
import { getIndex } from "../controllers/index";

const router = express.Router();

router.route("/")
    .get(getIndex);

router.route("")
    .get(getIndex);

router.route("/index.html")
    .get(getIndex);

router.route("/*")
    .get(getIndex);

router.route("/receipt")
    .get(getIndex);

export default router;
