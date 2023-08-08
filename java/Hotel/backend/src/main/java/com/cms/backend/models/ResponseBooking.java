package com.cms.backend.models;

import java.util.List;

public class ResponseBooking {

    public boolean success;
    public List data;

    public ResponseBooking(boolean status, Iterable<Booking> data) {
        this.success = status;
        this.data = (List) data;
    }
}
