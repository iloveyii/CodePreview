package com.eShop.backend.dto;

public class AddItemToCartResponse {

  Boolean requestStatus;
  String message;

  public void setRequestStatus(Boolean requestStatus) {
    this.requestStatus = requestStatus;
  }

  public Boolean getRequestStatus() {
    return requestStatus;
  }

  public String getMessage() {
    return message;
  }

  public void setMessage(String message) {
    this.message = message;
  }
}
