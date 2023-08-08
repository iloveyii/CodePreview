package com.cms.backend.models;

import java.util.List;

public class ResponseUser {

    public boolean success;
    public List data;

    public ResponseUser(boolean status, Iterable<User> data) {
        this.success = status;
        this.data = (List) data;
    }
}
