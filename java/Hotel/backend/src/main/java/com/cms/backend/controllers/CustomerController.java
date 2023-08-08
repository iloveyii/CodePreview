package com.cms.backend.controllers;


import com.cms.backend.models.ResponseCustomer;
import com.cms.backend.models.Result;
import com.cms.backend.models.Customer;
import com.cms.backend.service.CustomerService;
import com.fasterxml.jackson.core.JsonProcessingException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@CrossOrigin(origins = "*", maxAge = 3600)
@RestController
public class CustomerController {

    @Autowired
    CustomerService customerService;

    @PostMapping("/api/v1/customers")
    public Result createCustomer(@RequestBody Customer customer) throws JsonProcessingException {
        System.out.print("Received Customer");
        System.out.println(customer);
        customerService.create(customer);
        return new Result(true, "Customer saved");
    }

    @PutMapping("/api/v1/customers/{id}")
    public Result updateCustomer(@RequestBody Customer customer, @PathVariable  Integer id) throws JsonProcessingException {
        customerService.update(customer);
        return new Result(true, "Customer saved");
    }

    @GetMapping("/api/v1/customers")
    public ResponseCustomer getCustomers() throws JsonProcessingException {
        return new ResponseCustomer(true, customerService.all());
    }

    @DeleteMapping("/api/v1/customers/{id}")
    public Result deleteCustomer(@PathVariable  Integer id) throws JsonProcessingException {
        boolean status = customerService.delete(id);
        Result result = new Result(status, "Customer deleted with id " + id );
        return result;
    }
}
