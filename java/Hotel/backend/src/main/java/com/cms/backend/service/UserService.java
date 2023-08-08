package com.cms.backend.service;

import com.cms.backend.data.UserRepository;
import com.cms.backend.models.User;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class UserService {
    @Autowired
    private UserRepository userRepository;

    // Return all users
    public List<User> all() {
        return (List)userRepository.findAll();
    }

    public boolean create(User user) {
        userRepository.save(user);
        return  true;
    }

    public Optional<User> read(Integer id) {
        return userRepository.findById(id);
    }

    public boolean update(User user) {
        userRepository.save(user);
        return true;
    }

    public boolean delete(Integer id) {
        Optional<User> user =  userRepository.findById(id);

        if(user.isPresent()) {
            userRepository.deleteById(id);
            return true;
        }
        return  false;
    }

    public boolean find(User user) {
        List<User> u =  userRepository.findByEmail(user.getEmail());
        System.out.print("Find by email ::");
        System.out.println(u);
        return (u.size() > 0) && (u.get(0).getPassword().equals(user.getPassword()));
    }
}
