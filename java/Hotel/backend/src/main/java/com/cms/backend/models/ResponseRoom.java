package com.cms.backend.models;

import java.util.List;

public class ResponseRoom {

    public boolean success;
    public List data;

    public ResponseRoom(boolean status, Iterable<Room> data) {
        this.success = status;
        this.data = (List) data;
    }
}
