package com.eShop.backend.service;

import com.eShop.backend.model.ItemEntity;
import com.eShop.backend.repository.ItemRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.ArgumentCaptor;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.assertj.core.api.AssertionsForClassTypes.assertThat;
import static org.mockito.Mockito.when;
import static org.mockito.Mockito.verify;
import java.util.Optional;

@ExtendWith(MockitoExtension.class)
public class ItemServiceTest {

  @Mock
  private ItemRepository itemRepository;
  @Mock
  private OrderService orderService;
  private ItemService underTest;
  private ItemEntity item;

  @BeforeEach
  void setUp() {
    underTest = new ItemService(itemRepository, orderService);
    item = new ItemEntity();
    item.setName("item1");
    item.setCategory("category1");
    item.setDescription("description1");
    item.setImage("src/image1.png");
    item.setPrice((float) 11.1);
    item.setStock(11);
  }

  @Test
  void itShouldGetAllItems() {
    underTest.getItems();
    verify(itemRepository).findAll();
  }

  @Test
  void itShouldGetAnItem() {
    when(itemRepository.findById(item.getId())).thenReturn(Optional.of(item));

    assertEquals(item, underTest.getItem(item.getId()));
  }

  @Test
  void itShouldCreateAnItem() {
    underTest.createItem(item);

    ArgumentCaptor<ItemEntity> itemArgumentCaptor = ArgumentCaptor.forClass(ItemEntity.class);

    verify(itemRepository).save(itemArgumentCaptor.capture());

    ItemEntity capturedItem = itemArgumentCaptor.getValue();

    assertThat(capturedItem).isEqualTo(item);
  }

  @Test
  void itShouldUpdateAnItem() {
    ItemEntity item2 = new ItemEntity();
    item2.setName("item2");
    item2.setCategory("category2");
    item2.setDescription("description2");
    item2.setImage("src/image2.png");
    item2.setPrice((float) 22.2);
    item2.setStock(22);

    when(itemRepository.findById(item.getId())).thenReturn(Optional.of(item));

    underTest.updateItem(item.getId(), item2);

    ArgumentCaptor<ItemEntity> itemArgumentCaptor = ArgumentCaptor.forClass(ItemEntity.class);

    verify(itemRepository).save(itemArgumentCaptor.capture());

    ItemEntity capturedItem = itemArgumentCaptor.getValue();

    assertThat(capturedItem).isEqualTo(item2);
  }

  @Test
  void itShouldNotUpdateAnItemIfStockIsNegative() {
    ItemEntity item2 = new ItemEntity();
    item2.setName("item2");
    item2.setCategory("category2");
    item2.setDescription("description2");
    item2.setImage("src/image2.png");
    item2.setPrice((float) 22.2);
    item2.setStock(-10);

    when(itemRepository.findById(item.getId())).thenReturn(Optional.of(item));

    underTest.updateItem(item.getId(), item2);

    ItemEntity actualItem = underTest.getItem(item.getId());

    assertThat(actualItem).isNotEqualTo(item2);
  }

  @Test
  void itShouldRemoveAllItems() {
    underTest.deleteItems();
    verify(itemRepository).deleteAll();
  }

  @Test
  void itShouldRemoveAnItem() {
    underTest.deleteItem(item.getId());
    verify(itemRepository).deleteById(item.getId());
  }
}
