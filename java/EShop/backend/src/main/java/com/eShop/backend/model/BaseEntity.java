package com.eShop.backend.model;


import javax.persistence.*;
import java.util.UUID;

@Entity
@Inheritance(strategy = InheritanceType.TABLE_PER_CLASS)
public abstract class BaseEntity {

  @Id
  @Column(length = 16, updatable = true, nullable = false)
  protected UUID id = UUID.randomUUID();

  public UUID getId() {
    return id;
  }

  public void setId(UUID id) {
    this.id = id;
  }

  @Override
  public boolean equals(Object o) {
    if (this == o) return true;
    if (!(o instanceof BaseEntity)) return false;

    BaseEntity that = (BaseEntity) o;

    return getId() != null ? getId().equals(that.getId()) : that.getId() == null;
  }
}
