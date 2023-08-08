package com.eShop.backend.service;

import com.eShop.backend.dto.SignInRequest;
import com.eShop.backend.dto.SignInResponse;
import com.eShop.backend.helper.AuthHelper;
import com.eShop.backend.model.UserEntity;
import com.eShop.backend.repository.UserRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.Optional;

@Service
public class AuthenticationService {
  private static final Logger LOGGER = LoggerFactory.getLogger(AuthenticationService.class);

  private final UserRepository userRepository;
  private final PasswordEncoder passwordEncoder;

  public AuthenticationService(UserRepository userRepository) {
    this.userRepository = userRepository;
    this.passwordEncoder = new BCryptPasswordEncoder();
  }

  private String getBasicAuthToken(String username, String password) {
    LOGGER.debug("Generating basic auth token for user: {}", username);
    return AuthHelper.getBasicAuthToken(username, password);
  }

  public SignInResponse signIn(SignInRequest request) {
    LOGGER.info("Sign in request: {}", request);
    SignInResponse response = new SignInResponse();
    response.setUsername(request.getUsername());

    Optional<UserEntity> user = userRepository.findByEmail(request.getUsername());
    if (user.isEmpty()) {
      LOGGER.info("User ({}) not found", request.getUsername());
      response.setSuccess(false);
      response.setMessage("User not found");
      return response;
    }
    if (!passwordEncoder.matches(request.getPassword(), user.get().getPassword())) {
      LOGGER.info("Password ({}) does not match", request.getPassword());
      response.setSuccess(false);
      response.setMessage("Password does not match");
      return response;
    }

    LOGGER.info("User ({}) password matches", request.getUsername());
    response.setUserId(user.get().getId());
    response.setSuccess(true);
    response.setMessage("Password matches");
    response.setToken(getBasicAuthToken(request.getUsername(), request.getPassword()));
    response.setAdmin(user.get().isIsAdmin());
    response.setUsername(request.getUsername());

    return response;
  }

}
