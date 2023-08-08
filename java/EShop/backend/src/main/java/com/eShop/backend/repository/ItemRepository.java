package com.eShop.backend.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import com.eShop.backend.model.ItemEntity;

import java.util.List;
import java.util.Optional;
import java.util.UUID;

public interface ItemRepository extends JpaRepository<ItemEntity, UUID> {
  @Override
  @Query("SELECT e FROM #{#entityName} e WHERE e.deleted=false")
  public List<ItemEntity> findAll();

  @Override
  @Query("SELECT e FROM #{#entityName} e WHERE e.deleted=false AND e.id=?1")
  public Optional<ItemEntity> findById(UUID id);

  @Override
  @Query("UPDATE #{#entityName} e SET e.deleted=true WHERE e.id=?1")
  @Modifying
  public void deleteById(UUID id);
}
