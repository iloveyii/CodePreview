import express from "express";
import { getOrder, getOrders, createOrder, updateOrder, deleteOrder, deleteOrderByStatus } from "../controllers/order";
import { ws_update } from "../middlewares/ws_update";


const router = express.Router();

router.route("/:id")
    .get(getOrder)
    .delete(ws_update, deleteOrder)
    .put(ws_update, updateOrder);

router.route("/:id/:status")
    .get(deleteOrderByStatus)
    .delete(ws_update, deleteOrderByStatus);

router.route("/")
    .get(getOrders)
    .post(ws_update, createOrder);

export default router;
