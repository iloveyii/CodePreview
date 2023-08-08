package com.eShop.backend.model;

import com.fasterxml.jackson.annotation.JsonManagedReference;

import javax.persistence.CascadeType;
import javax.persistence.Entity;
import javax.persistence.ManyToOne;
import javax.persistence.OneToMany;
import java.sql.Timestamp;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;


@Entity
public class OrderEntity extends BaseEntity {
  @OneToMany(cascade = {CascadeType.ALL})
  @JsonManagedReference
  private List<OrderItemEntity> items;

  @ManyToOne
  private UserEntity customer;
  private OrderStatusEnum orderStatus;
  private Timestamp createdAt;
  private String customerFirstName;
  private String customerLastName;
  private String customerAddress;
  private String customerPhone;
  private float totalPrice;

  public OrderEntity() {
    this.items = new ArrayList<>();
    this.createdAt = new Timestamp(System.currentTimeMillis());
  }

  public List<OrderItemEntity> getItems() {
    return this.items;
  }

  public void setItems(List<OrderItemEntity> items) {
    this.items = items;
  }

  public UserEntity getCustomer() {
    return this.customer;
  }

  public void setCustomer(UserEntity customerId) {
    this.customer = customerId;
  }

  public OrderStatusEnum getOrderStatus() {
    return this.orderStatus;
  }

  public void setOrderStatus(OrderStatusEnum orderStatus) {
    this.orderStatus = orderStatus;
  }

  public Timestamp getCreatedAt() {
    return this.createdAt;
  }

  public void setCreatedAt(Timestamp createdAt) {
    this.createdAt = createdAt;
  }

  public String getCustomerFirstName() {
    return this.customerFirstName;
  }

  public void setCustomerFirstName(String customerFirstName) {
    this.customerFirstName = customerFirstName;
  }

  public String getCustomerLastName() {
    return this.customerLastName;
  }

  public void setCustomerLastName(String customerLastName) {
    this.customerLastName = customerLastName;
  }

  public String getCustomerAddress() {
    return this.customerAddress;
  }

  public void setCustomerAddress(String customerAddress) {
    this.customerAddress = customerAddress;
  }

  public String getCustomerPhone() {
    return this.customerPhone;
  }

  public void setCustomerPhone(String customerPhone) {
    this.customerPhone = customerPhone;
  }

  public float getTotalPrice() {
    return totalPrice = items.stream().map(item -> item.getItem().getPrice() * item.getQuantity()).reduce(0.0f, (subtotal, itemPrice) -> subtotal + itemPrice);
  }

  public void setTotalPrice(float totalPrice) {
    this.totalPrice = totalPrice;
  }

  @Override
  public String toString() {
    return "{" +
      " items='" + getItems() + "'" +
      ", customerId='" + getCustomer() + "'" +
      ", orderStatus='" + getOrderStatus() + "'" +
      ", createdAt='" + getCreatedAt() + "'" +
      ", customerFirstName='" + getCustomerFirstName() + "'" +
      ", customerLastName='" + getCustomerLastName() + "'" +
      ", customerAddress='" + getCustomerAddress() + "'" +
      ", customerPhone='" + getCustomerPhone() + "'" +
      "}";
  }


  @Override
  public boolean equals(Object o) {
    if (o == this)
      return true;
    if (!(o instanceof OrderEntity)) {
      return false;
    }
    OrderEntity orderEntity = (OrderEntity) o;
    return Objects.equals(items, orderEntity.items) && Objects.equals(customer, orderEntity.customer) && Objects.equals(orderStatus, orderEntity.orderStatus) && Objects.equals(createdAt, orderEntity.createdAt) && Objects.equals(customerFirstName, orderEntity.customerFirstName) && Objects.equals(customerLastName, orderEntity.customerLastName) && Objects.equals(customerAddress, orderEntity.customerAddress) && Objects.equals(customerPhone, orderEntity.customerPhone);
  }


}
