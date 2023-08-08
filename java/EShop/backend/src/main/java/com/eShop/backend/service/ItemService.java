package com.eShop.backend.service;

import com.eShop.backend.dto.UpdateItemResponse;
import com.eShop.backend.model.ItemEntity;
import com.eShop.backend.repository.ItemRepository;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.UUID;

@Service
public class ItemService {
  private final ItemRepository itemRepository;
  private final OrderService orderService;

  public ItemService(ItemRepository itemRepository, OrderService orderService) {
    this.itemRepository = itemRepository;
    this.orderService = orderService;
  }

  public List<ItemEntity> getItems() {
    return itemRepository.findAll();
  }

  public ItemEntity getItem(UUID itemId) {
    return itemRepository.findById(itemId).get();
  }

  public ItemEntity createItem(ItemEntity item) {
    return itemRepository.save(item);
  }

  public ResponseEntity<UpdateItemResponse> updateItem(UUID itemId, ItemEntity item) {
    ItemEntity it = itemRepository.findById(itemId).get();
    UpdateItemResponse response = new UpdateItemResponse();

    it.setName(item.getName());
    it.setPrice(item.getPrice());
    it.setCategory(item.getCategory());
    it.setDescription(item.getDescription());
    it.setImage(item.getImage());

    if (item.getStock() >= 0) {
      it.setStock(item.getStock());
      itemRepository.save(it);

      response.setStatusCode(200);
      response.setMessage("Item stock updated successfully!");
      return ResponseEntity.ok(response);
    }

    response.setStatusCode(400);
    response.setMessage("Item stock update failed!");
    return ResponseEntity.badRequest().body(response);
  }

  public void deleteItems() {
    itemRepository.deleteAll();
  }

  public void deleteItem(UUID itemId) {
    itemRepository.deleteById(itemId);
    orderService.removeOrderItemFromInCartOrders(itemId);
  }
}
