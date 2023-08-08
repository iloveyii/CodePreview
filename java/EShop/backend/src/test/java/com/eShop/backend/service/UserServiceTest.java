package com.eShop.backend.service;

import com.eShop.backend.model.UserEntity;
import com.eShop.backend.repository.UserRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.ArgumentCaptor;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.assertj.core.api.AssertionsForClassTypes.assertThat;
import static org.mockito.Mockito.when;
import static org.mockito.Mockito.verify;
import java.util.Optional;

@ExtendWith(MockitoExtension.class)
public class UserServiceTest {

  @Mock
  private UserRepository userRepository;
  private UserService underTest;
  private UserEntity user;

  @BeforeEach
  void setUp() {
    underTest = new UserService(userRepository);

    user = new UserEntity();
    user.setEmail("mail1");
    user.setFirstName("fname1");
    user.setLastName("lname1");
    user.setPassword("password1");
    user.setAddress("address1");
    user.setPhone("phone1");
    user.setIsAdmin(false);
  }

  @Test
  void itShouldGetAllUsers() {
    underTest.getAllUsers();
    verify(userRepository).findAll();
  }

  @Test
  void itShouldCreateAUser() {
    underTest.saveUser(user);

    ArgumentCaptor<UserEntity> userArgumentCaptor = ArgumentCaptor.forClass(UserEntity.class);

    verify(userRepository).save(userArgumentCaptor.capture());

    UserEntity capturedUser = userArgumentCaptor.getValue();

    assertThat(capturedUser).isEqualTo(user);
  }

  @Test
  void itShouldRemoveAUser() {
    underTest.deleteUser(user.getId());

    verify(userRepository).deleteById(user.getId());
  }
}