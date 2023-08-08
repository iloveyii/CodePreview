package com.eShop.backend.mapper;

import com.eShop.backend.dto.UserDto;
import com.eShop.backend.model.UserEntity;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class UserMapper {

  public List<UserDto> toDto(List<UserEntity> entities) {
    return entities.stream().map(this::toDto).collect(java.util.stream.Collectors.toList());
  }

  public UserDto toDto(UserEntity UserDto) {
    var dto = new UserDto();
    dto.setUserMail(UserDto.getEmail());
    dto.setFirstName(UserDto.getFirstName());
    dto.setLastName(UserDto.getLastName());
    dto.setPassword(UserDto.getPassword());
    dto.setAddress(UserDto.getAddress());
    dto.setPhone(UserDto.getPhone());
    dto.setIsAdmin(UserDto.getIsAdmin());
    return dto;
  }
}
