import Login from "../models/Login";
import User from "../models/User";
import Make from "../models/Make";
import Product from "../models/Product";
import Service from "../models/Service";
import Extra from "../models/Extra";
import Order from "../models/Order";
import Search from "../models/Search";
import Toast from "../models/Toast";
import Settings from "../models/Settings";


const models = {
    logins: new Login('logins'),
    users: new User('users'),
    makes: new Make('makes'),
    products: new Product('products'),
    services: new Service('services'),
    extras: new Extra('extras'),
    orders: new Order('orders'),
    searches: new Search('searches'),
    toasts: new Toast('toasts'),
    settings: new Settings('settings'),
};

export default models;
