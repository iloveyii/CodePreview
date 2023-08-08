package com.eShop.backend.dto;

import com.fasterxml.jackson.annotation.JsonProperty;

import java.util.UUID;

public class AddItemToCartRequest {

  @JsonProperty("itemId")
  UUID itemId;

  @JsonProperty("orderId")
  UUID orderId;

  @JsonProperty("quantity")
  Integer quantity;

  @JsonProperty("customerId")
  UUID customerId;

  public UUID getItemId() {
    return itemId;
  }

  public void setItemId(UUID itemId) {
    this.itemId = itemId;
  }

  public UUID getOrderId() {
    return orderId;
  }

  public void setOrderId(UUID cartId) {
    this.orderId = cartId;
  }

  public Integer getQuantity() {
    return quantity;
  }

  public void setQuantity(Integer quantity) {
    this.quantity = quantity;
  }


  public UUID getCustomerId() {
    return customerId;
  }

  public void setCustomerId(UUID customerId) {
    this.customerId = customerId;
  }
}
