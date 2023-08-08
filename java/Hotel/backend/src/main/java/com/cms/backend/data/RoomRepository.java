package com.cms.backend.data;

import com.cms.backend.models.Room;
import org.springframework.data.repository.CrudRepository;

public interface RoomRepository extends CrudRepository <Room, Integer> {
}
