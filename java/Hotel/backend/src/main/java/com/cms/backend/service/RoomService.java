package com.cms.backend.service;

import com.cms.backend.data.RoomRepository;
import com.cms.backend.models.Room;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class RoomService {
    @Autowired
    private RoomRepository roomRepository;

    // Return all rooms
    public List<Room> all() {
        return (List)roomRepository.findAll();
    }

    public boolean create(Room room) {
        roomRepository.save(room);
        return  true;
    }

    public Optional<Room> read(Integer id) {
        return roomRepository.findById(id);
    }

    public boolean update(Room room) {
        roomRepository.save(room);
        return true;
    }

    public boolean delete(Integer id) {
        Optional<Room> room =  roomRepository.findById(id);

        if(room.isPresent()) {
            roomRepository.deleteById(id);
            return true;
        }
        return  false;
    }
}
