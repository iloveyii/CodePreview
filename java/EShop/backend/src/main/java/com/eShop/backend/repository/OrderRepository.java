package com.eShop.backend.repository;

import com.eShop.backend.model.OrderEntity;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.util.Optional;
import java.util.List;
import java.util.UUID;

@Repository
public interface OrderRepository extends JpaRepository<OrderEntity, UUID> {
  public Optional<OrderEntity> findById(UUID id);
  
  @Query("SELECT e FROM #{#entityName} e WHERE e.orderStatus=0")
  public Optional<List<OrderEntity>> findAllInCart();
}
