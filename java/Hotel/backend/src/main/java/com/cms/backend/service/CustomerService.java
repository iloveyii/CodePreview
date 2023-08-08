package com.cms.backend.service;

import com.cms.backend.data.CustomerRepository;
import com.cms.backend.models.Customer;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class CustomerService {
    @Autowired
    private CustomerRepository customerRepository;

    // Return all customers
    public List<Customer> all() {
        return (List)customerRepository.findAll();
    }

    public boolean create(Customer customer) {
        customerRepository.save(customer);
        return  true;
    }

    public Optional<Customer> read(Integer id) {
        return customerRepository.findById(id);
    }

    public boolean update(Customer customer) {
        customerRepository.save(customer);
        return true;
    }

    public boolean delete(Integer id) {
        Optional<Customer> customer =  customerRepository.findById(id);

        if(customer.isPresent()) {
            customerRepository.deleteById(id);
            return true;
        }
        return  false;
    }
}
