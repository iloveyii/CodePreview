package com.cms.backend.data;

import com.cms.backend.models.Booking;
import org.springframework.data.repository.CrudRepository;

public interface BookingRepository extends CrudRepository <Booking, Integer> {
}
