package com.cms.backend.controllers;

import com.cms.backend.models.ResponseBooking;
import com.cms.backend.models.Result;
import com.cms.backend.models.Booking;
import com.cms.backend.models.User;
import com.cms.backend.service.BookingService;
import com.fasterxml.jackson.core.JsonProcessingException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@CrossOrigin(origins = "*", maxAge = 3600)
@RestController
public class BookingController {

    @Autowired
    BookingService bookingService;

    @PostMapping("/api/v1/bookings")
    public Result createBooking(@RequestBody Booking booking) throws JsonProcessingException {
        System.out.printf("Received booking: %s\n", booking);
        bookingService.create(booking);
        return new Result(true, "Booking saved");
    }

    @PutMapping("/api/v1/bookings/{id}")
    public Result updateBooking(@RequestBody Booking booking, @PathVariable  Integer id) throws JsonProcessingException {
        bookingService.update(booking);
        return new Result(true, "Booking updated");
    }

    @GetMapping("/api/v1/bookings")
    public ResponseBooking getBookings() throws JsonProcessingException {
        return new ResponseBooking(true, bookingService.all());
    }

    @DeleteMapping("/api/v1/bookings/{id}")
    public Result deleteBooking(@PathVariable Integer id) throws JsonProcessingException {
        boolean status = bookingService.delete(id);
        Result result = new Result(status, "Booking deleted with id " + id );
        return result;
    }
}
