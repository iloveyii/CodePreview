import express from "express";
import { getExtra, getExtras, createExtra, updateExtra, deleteExtra } from "../controllers/extra";


const router = express.Router();

router.route("/:id")
    .get(getExtra)
    .delete(deleteExtra)
    .put(updateExtra);

router.route("/")
    .get(getExtras)
    .post(createExtra);

export default router;
