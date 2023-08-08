package com.cms.backend.service;

import com.cms.backend.data.BookingRepository;
import com.cms.backend.models.Booking;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class BookingService {
    @Autowired
    private BookingRepository bookingRepository;

    // Return all bookings
    public List<Booking> all() {
        return (List)bookingRepository.findAll();
    }

    public boolean create(Booking booking) {
        bookingRepository.save(booking);
        return  true;
    }

    public Optional<Booking> read(Integer id) {
        return bookingRepository.findById(id);
    }

    public boolean update(Booking booking) {
        bookingRepository.save(booking);
        return true;
    }

    public boolean delete(Integer id) {
        Optional<Booking> booking =  bookingRepository.findById(id);

        if(booking.isPresent()) {
            bookingRepository.deleteById(id);
            return true;
        }
        return  false;
    }
}
