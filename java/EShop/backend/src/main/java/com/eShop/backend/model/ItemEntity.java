package com.eShop.backend.model;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Lob;

import java.util.Objects;

@Entity
public class ItemEntity extends BaseEntity {
  private String name;
  private float price;
  private String category;

  @Lob
  @Column(length = 100000)
  private String description;
  private int stock;
  private String image;
  private boolean deleted = Boolean.FALSE;

  public String getName() {
    return this.name;
  }

  public void setName(String name) {
    this.name = name;
  }

  public float getPrice() {
    return this.price;
  }

  public void setPrice(float price) {
    this.price = price;
  }

  public String getCategory() {
    return this.category;
  }

  public void setCategory(String category) {
    this.category = category;
  }

  public String getDescription() {
    return this.description;
  }

  public void setDescription(String description) {
    this.description = description;
  }

  public int getStock() {
    return this.stock;
  }

  public void setStock(int stock) {
    this.stock = stock;
  }

  public String getImage() {
    return this.image;
  }

  public void setImage(String image) {
    this.image = image;
  }

  public boolean getDeleted() {
    return this.deleted;
  }

  public void setDeleted(boolean deleted) {
    this.deleted = deleted;
  }

  @Override
  public String toString() {
    return "{" +
        " name='" + getName() + "'" +
        ", price='" + getPrice() + "'" +
        ", category='" + getCategory() + "'" +
        ", description='" + getDescription() + "'" +
        ", stock='" + getStock() + "'" +
        ", image='" + getImage() + "'" +
        ", deleted=" + getDeleted() + " " +
        "}";
  }

  @Override
  public boolean equals(Object o) {
    if (o == this)
      return true;
    if (!(o instanceof ItemEntity)) {
      return false;
    }
    ItemEntity itemEntity = (ItemEntity) o;
    return Objects.equals(name, itemEntity.name) && price == itemEntity.price
        && Objects.equals(category, itemEntity.category) && Objects.equals(description, itemEntity.description)
        && stock == itemEntity.stock && Objects.equals(image, itemEntity.image)
        && deleted == itemEntity.deleted;
  }

}
