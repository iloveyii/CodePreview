package com.cms.backend.models;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

@Getter
@Setter

@Entity
public class Booking {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;
    private String room_number;
    private Double price;
    private String name;
    private String phone;
    private String email;
    private String datetime;

    public String toString() {
        return String.format("id: %d, room_number: %s, price: %.2f, name: %s, phone: %s, email: %s, datetime: %s", id, room_number, price, name, phone, email, datetime);
    }

}
