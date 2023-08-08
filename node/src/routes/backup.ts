import express from "express";
import { getBackup, createBackup, updateBackup } from "../controllers/backup";

const router = express.Router();

router.route("/:id")
    .get(getBackup)
    .put(updateBackup);

router.route("/")
    .post(createBackup);

export default router;
