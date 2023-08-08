package com.cms.backend.controllers;


import com.cms.backend.models.*;
import com.cms.backend.service.UserService;
import com.fasterxml.jackson.core.JsonProcessingException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@CrossOrigin(origins = "*", maxAge = 3600)
@RestController
public class UserController {

    @Autowired
    UserService userService;

    @PostMapping("/api/v1/logins")
    public Result loginUser(@RequestBody User user) throws JsonProcessingException {
        boolean found = userService.find(user);
        System.out.printf("Found ::: %s\n", found);
        return new Result(found, "User saved");
    }

    @PostMapping("/api/v1/users")
    public Result createUser(@RequestBody User user) throws JsonProcessingException {
        userService.create(user);
        return new Result(true, "User saved");
    }

    @PutMapping("/api/v1/users/{id}")
    public Result updateUser(@RequestBody User user, @PathVariable  Integer id) throws JsonProcessingException {
        userService.update(user);
        return new Result(true, "User saved");
    }

    @GetMapping("/api/v1/users")
    public ResponseUser getUsers() throws JsonProcessingException {
        return new ResponseUser(true, userService.all());
    }

    @DeleteMapping("/api/v1/users/{id}")
    public Result deleteUser(@PathVariable  Integer id) throws JsonProcessingException {
        boolean status = userService.delete(id);
        Result result = new Result(status, "User deleted with id " + id );
        return result;
    }
}
