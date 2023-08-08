package com.eShop.backend.mapper;

import com.eShop.backend.dto.OrderDto;
import com.eShop.backend.model.OrderEntity;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class OrderMapper {

  public List<OrderDto> toDto(List<OrderEntity> entities) {
    return entities.stream().map(this::toDto).collect(java.util.stream.Collectors.toList());
  }

  public OrderDto toDto(OrderEntity orderEntity) {
    var dto = new OrderDto();
    dto.setItems(orderEntity.getItems());
    dto.setCustomerId(orderEntity.getCustomer().getId());
    dto.setOrderStatus(orderEntity.getOrderStatus());
    dto.setCreatedAt(orderEntity.getCreatedAt());
    dto.setCustomerFirstName(orderEntity.getCustomerFirstName());
    dto.setCustomerLastName(orderEntity.getCustomerLastName());
    dto.setCustomerAddress(orderEntity.getCustomerAddress());
    dto.setCustomerPhone(orderEntity.getCustomerPhone());
    dto.setTotalPrice(orderEntity.getTotalPrice());
    return dto;
  }
}
