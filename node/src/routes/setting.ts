import express from "express";
import { getSetting, updateSetting, deleteSetting } from "../controllers/setting";


const router = express.Router();

router.route("/:id")
    .get(getSetting)
    .delete(deleteSetting)
    .put(updateSetting);

router.route("/")
    .get(getSetting);

export default router;
