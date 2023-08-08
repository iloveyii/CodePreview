package com.eShop.backend.service;

import com.eShop.backend.dto.SignInRequest;
import com.eShop.backend.dto.SignInResponse;
import com.eShop.backend.helper.UserHelper;
import com.eShop.backend.model.UserEntity;
import com.eShop.backend.repository.UserRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;

import java.util.Optional;

import static org.assertj.core.api.AssertionsForClassTypes.assertThat;
import static org.mockito.Mockito.when;

@ExtendWith(MockitoExtension.class)
public class AuthenticationServiceTest {
  @Mock
  private UserRepository userRepository;

  private PasswordEncoder passwordEncoder = new BCryptPasswordEncoder();

  private AuthenticationService authenticationService;

  @BeforeEach
  void setUp() {
    authenticationService = new AuthenticationService(userRepository);
  }


  @Test
  void itShouldReturnNotSuccessful() {
    // arrange
    when(userRepository.findByEmail("user@mail.com")).thenReturn(Optional.empty());
    SignInRequest signInRequest = new SignInRequest();
    signInRequest.setUsername("user@mail.com");

    // act
    SignInResponse response = authenticationService.signIn(signInRequest);

    // assert
    assertThat(response).isNotNull();
    assertThat(response.isSuccess()).isFalse();
    assertThat(response.getMessage()).isNotNull();
  }

  @Test
  void itShouldReturnSuccessfulWhenPasswordMatches() {
    // arrange
    UserEntity userEntity = UserHelper.getMockUser();
    String password = "somePassword124";
    userEntity.setPassword(passwordEncoder.encode(password));
    when(userRepository.findByEmail(userEntity.getEmail())).thenReturn(Optional.of(userEntity));

    SignInRequest signInRequest = new SignInRequest();
    signInRequest.setUsername(userEntity.getEmail());
    signInRequest.setPassword(password);

    // act
    SignInResponse response = authenticationService.signIn(signInRequest);

    // assert
    assertThat(response).isNotNull();
    assertThat(response.isSuccess()).isTrue();
    assertThat(response.getToken()).isNotNull();
    assertThat(response.getUsername()).isNotNull();
    assertThat(response.getUserId()).isNotNull();
  }

  @Test
  void itShouldReturnNotSuccessfulWhenPasswordDoesNotMatche() {
    // arrange
    UserEntity userEntity = UserHelper.getMockUser();
    String password = "somePassword124";
    userEntity.setPassword(passwordEncoder.encode(password));
    when(userRepository.findByEmail(userEntity.getEmail())).thenReturn(Optional.of(userEntity));

    SignInRequest signInRequest = new SignInRequest();
    signInRequest.setUsername(userEntity.getEmail());
    signInRequest.setPassword("someOtherPassword");

    // act
    SignInResponse response = authenticationService.signIn(signInRequest);

    // assert
    assertThat(response).isNotNull();
    assertThat(response.isSuccess()).isFalse();
    assertThat(response.getToken()).isNull();
    assertThat(response.getUsername()).isNotNull();
    assertThat(response.getUserId()).isNull();
  }
}
