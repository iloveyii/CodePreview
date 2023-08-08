package com.cms.backend.models;

import java.util.List;

public class ResponseCustomer {

    public boolean success;
    public List data;

    public ResponseCustomer(boolean status, Iterable<Customer> data) {
        this.success = status;
        this.data = (List) data;
    }
}
