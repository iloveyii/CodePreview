package com.cms.backend.controllers;


import com.cms.backend.data.RoomRepository;
import com.cms.backend.models.ResponseRoom;
import com.cms.backend.models.Result;
import com.cms.backend.models.Room;
import com.cms.backend.service.RoomService;
import com.fasterxml.jackson.core.JsonProcessingException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.Optional;

@CrossOrigin(origins = "*", maxAge = 3600)
@RestController
public class RoomController {

    @Autowired
    RoomService roomService;

    @PostMapping("/api/v1/rooms")
    public Result createRoom(@RequestBody Room room) throws JsonProcessingException {
        roomService.create(room);
        return new Result(true, "Room saved");
    }

    @GetMapping("/api/v1/rooms")
    public ResponseRoom getRooms() throws JsonProcessingException {
        return new ResponseRoom(true, roomService.all());
    }

    @DeleteMapping("/api/v1/rooms/{id}")
    public Result deleteRoom(@PathVariable  Integer id) throws JsonProcessingException {
        boolean status = roomService.delete(id);
        Result result = new Result(status, "Room deleted with id " + id );
        return result;
    }
}
