package com.eShop.backend.controller;

import com.eShop.backend.dto.*;
import com.eShop.backend.mapper.OrderMapper;
import com.eShop.backend.model.OrderEntity;
import com.eShop.backend.service.OrderService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Optional;
import java.util.UUID;

@RestController
@RequestMapping("/api/order")
public class OrderController {
  private static final Logger LOGGER = LoggerFactory.getLogger(OrderService.class);
  private final OrderService orderService;
  private final OrderMapper orderMapper;

  @Autowired
  public OrderController(OrderService orderService, OrderMapper orderMapper) {
    this.orderService = orderService;
    this.orderMapper = orderMapper;
  }

  @GetMapping
  public List<OrderEntity> readOrders() {
    return orderService.getOrders();
  }

  @PostMapping("add")
  public String createOrder(@RequestBody OrderEntity orderEntity) {
    orderService.createOrder(orderEntity);
    return "Order Created!";
  }

  @PutMapping(path = "status/{orderId}")
  public OrderEntity orderStatus(@PathVariable(value = "orderId") UUID id, @RequestBody OrderEntity orderEntity) {
    LOGGER.info("Updated order status to: ({}) for order id: ({})", orderEntity.getOrderStatus(), id);
    return orderService.updateOrderStatus(id, orderEntity.getOrderStatus());
  }

  @GetMapping("cart")
  public Optional<OrderDto> getCart(@RequestParam("userId") UUID userId) {
    Optional<OrderEntity> orderEntity = orderService.getCartForUser(userId);
    return orderEntity.map(orderMapper::toDto);
  }

  @PostMapping("cart")
  public AddItemToCartResponse addItemToCart(@RequestBody AddItemToCartRequest addItemToCartRequest) {
    return orderService.addItemToCart(addItemToCartRequest);
  }

  @DeleteMapping(path = "cart/{orderItemId}")
  public void deleteItem(@PathVariable(value = "orderItemId") UUID orderItemId) {
    orderService.removeItemFromCart(orderItemId);
  }

  @RequestMapping("count")
  public OrderItemsCountResponse orderItemsCount(OrderItemsCountRequest itemsCountDto) {
    return orderService.getOrderItemsCount(itemsCountDto);
  }

}
