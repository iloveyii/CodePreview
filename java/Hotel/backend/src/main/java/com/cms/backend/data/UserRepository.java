package com.cms.backend.data;

import com.cms.backend.models.User;
import org.springframework.data.repository.CrudRepository;

import java.util.List;

public interface UserRepository extends CrudRepository <User, Integer> {
    List<User> findByEmail(String email);
}
