package com.eShop.backend.controller;

import com.eShop.backend.dto.UpdateItemResponse;
import com.eShop.backend.model.ItemEntity;
import com.eShop.backend.service.ItemService;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.UUID;

@RestController
@RequestMapping("/api/items")
public class ItemController {
  private final ItemService itemService;

  public ItemController(ItemService itemService) {
    this.itemService = itemService;
  }

  @GetMapping
  public List<ItemEntity> readItems() {
    return itemService.getItems();
  }

  @GetMapping(path = "/{itemId}")
  public ItemEntity readItem(@PathVariable(value = "itemId") UUID id) {
    return itemService.getItem(id);
  }

  @PostMapping
  @PreAuthorize("hasRole('ADMIN')")
  public ItemEntity createItem(@RequestBody ItemEntity item) {
    return itemService.createItem(item);
  }

  @PutMapping(path = "/{itemId}")
  @PreAuthorize("hasRole('ADMIN')")
  public ResponseEntity<UpdateItemResponse> updateItem(@PathVariable(value = "itemId") UUID id,
                               @RequestBody ItemEntity item) {
    return itemService.updateItem(id, item);
  }

  @DeleteMapping
  @PreAuthorize("hasRole('ADMIN')")
  public void deleteItems() {
    itemService.deleteItems();
  }

  @DeleteMapping(path = "/{itemId}")
  @PreAuthorize("hasRole('ADMIN')")
  public void deleteItem(@PathVariable(value = "itemId") UUID id) {
    itemService.deleteItem(id);
  }
}
