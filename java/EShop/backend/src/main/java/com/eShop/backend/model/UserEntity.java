package com.eShop.backend.model;

import javax.persistence.Entity;

@Entity
public class UserEntity extends BaseEntity {
  private String email;
  private String firstName;
  private String lastName;
  private String password;
  private String address;
  private String phone;
  private boolean isAdmin;

  // Getters and Setters
  public String getEmail() {
    return this.email;
  }

  public void setEmail(String userMail) {
    this.email = userMail;
  }

  public String getFirstName() {
    return this.firstName;
  }

  public void setFirstName(String firstName) {
    this.firstName = firstName;
  }

  public String getLastName() {
    return this.lastName;
  }

  public void setLastName(String lastName) {
    this.lastName = lastName;
  }

  public String getPassword() {
    return this.password;
  }

  public void setPassword(String hashedPassword) {
    this.password = hashedPassword;
  }

  public String getAddress() {
    return this.address;
  }

  public void setAddress(String address) {
    this.address = address;
  }

  public String getPhone() {
    return this.phone;
  }

  public void setPhone(String phone) {
    this.phone = phone;
  }

  public boolean isIsAdmin() {
    return this.isAdmin;
  }

  public boolean getIsAdmin() {
    return this.isAdmin;
  }

  public void setIsAdmin(boolean isAdmin) {
    this.isAdmin = isAdmin;
  }


  // toString
  @Override
  public String toString() {
    return "{" +
      " userMail='" + getEmail() + "'" +
      ", firstName='" + getFirstName() + "'" +
      ", lastName='" + getLastName() + "'" +
      ", hashedPassword='" + getPassword() + "'" +
      ", address='" + getAddress() + "'" +
      ", phone='" + getPhone() + "'" +
      ", isAdmin='" + isIsAdmin() + "'" +
      "}";
  }

}
