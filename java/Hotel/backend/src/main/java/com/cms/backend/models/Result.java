package com.cms.backend.models;

public class Result {
    public boolean status;
    public String message;

    public Result(boolean status, String message) {
        this.status = status;
        this.message = message;
    }
}
