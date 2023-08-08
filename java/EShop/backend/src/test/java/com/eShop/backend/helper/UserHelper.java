package com.eShop.backend.helper;

import com.eShop.backend.model.UserEntity;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;

import java.util.UUID;

public class UserHelper {
  static PasswordEncoder passwordEncoder = new BCryptPasswordEncoder();

  public static UserEntity getMockUser() {
    UserEntity user = new UserEntity();
    user.setId(UUID.randomUUID());
    user.setEmail("user@mail.com");
    user.setPassword(passwordEncoder.encode("password"));
    user.setLastName("Lastname");
    user.setFirstName("Firstname");
    user.setAddress("Address 1");
    user.setPhone("123456789");
    return user;
  }
}
