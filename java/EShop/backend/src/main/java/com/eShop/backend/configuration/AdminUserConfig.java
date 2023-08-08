package com.eShop.backend.configuration;

import com.eShop.backend.model.UserEntity;
import com.eShop.backend.repository.UserRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.event.ContextRefreshedEvent;
import org.springframework.context.event.EventListener;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;

import java.util.Optional;
import java.util.UUID;

@Configuration
public class AdminUserConfig {
  private static final Logger LOGGER = LoggerFactory.getLogger(AdminUserConfig.class);

  private final UserRepository userRepository;
  private final PasswordEncoder passwordEncoder;

  public AdminUserConfig(UserRepository userRepository) {
    this.userRepository = userRepository;
    this.passwordEncoder = new BCryptPasswordEncoder();
  }

  @EventListener
  public void seedAdminUser(ContextRefreshedEvent event) {
    LOGGER.info("Making sure that there is an admin user");
    Optional<UserEntity> user = userRepository.findByEmail("admin@mail.com");
    if (user.isEmpty()) {
      LOGGER.info("Creating admin user");
      UserEntity adminUser = new UserEntity();
      // hardcoded admin user id - could be removed once we have a proper user management
      adminUser.setId(UUID.fromString("f5ceb222-a689-4217-b6d5-683f39e019c1"));
      adminUser.setEmail("admin@mail.com");
      adminUser.setPassword(passwordEncoder.encode("admin123456"));
      adminUser.setIsAdmin(true);
      adminUser.setFirstName("Admin");
      adminUser.setLastName("User");
      userRepository.save(adminUser);
    } else {
      LOGGER.info("Admin user already exists");
    }
  }
}
