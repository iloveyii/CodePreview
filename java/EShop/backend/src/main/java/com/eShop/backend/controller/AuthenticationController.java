package com.eShop.backend.controller;

import com.eShop.backend.dto.SignInRequest;
import com.eShop.backend.dto.SignInResponse;
import com.eShop.backend.service.AuthenticationService;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(path = "/api/auth")
public class AuthenticationController {

  private final AuthenticationService authenticationService;

  public AuthenticationController(AuthenticationService authenticationService) {
    this.authenticationService = authenticationService;
  }

  @PostMapping(path = "signin")
  public SignInResponse login(@RequestBody SignInRequest request) {
    return authenticationService.signIn(request);
  }
}
