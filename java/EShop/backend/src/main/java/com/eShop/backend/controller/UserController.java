package com.eShop.backend.controller;

import com.eShop.backend.model.UserEntity;
import com.eShop.backend.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

/**
 * Todo:
 * Authorization for DeleteRequest
 * GetRequest should only return name and lastname?
 */

@RestController
@RequestMapping(path = "/api/users")
public class UserController {

  private final UserService userService;

  @Autowired
  public UserController(UserService userService) {
    this.userService = userService;
  }

  @GetMapping(path = "all")
  public Iterable<UserEntity> getAllUsers() {
    return this.userService.getAllUsers();
  }

  @PostMapping(path = "add")
  public void createUser(@RequestBody UserEntity user) {
    this.userService.saveUser(user);
  }

  @DeleteMapping(value = "delete/{id}")
  public ResponseEntity<UUID> deleteUser(@PathVariable UUID id) {
    this.userService.deleteUser(id);
    return new ResponseEntity<>(id, HttpStatus.OK);
  }
}
