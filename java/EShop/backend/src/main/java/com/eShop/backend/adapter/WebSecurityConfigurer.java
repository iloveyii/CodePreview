package com.eShop.backend.adapter;

import com.eShop.backend.entrypoint.MyBasicAuthenticationEntryPoint;
import com.eShop.backend.service.UserService;
import org.springframework.boot.autoconfigure.security.SecurityProperties;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.core.annotation.Order;
import org.springframework.security.config.annotation.method.configuration.EnableGlobalMethodSecurity;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;

@Configuration
@EnableWebSecurity
@EnableGlobalMethodSecurity(prePostEnabled = true)
@Order(SecurityProperties.BASIC_AUTH_ORDER - 10)
public class WebSecurityConfigurer extends WebSecurityConfigurerAdapter {

  private final MyBasicAuthenticationEntryPoint authenticationEntryPoint;

  public WebSecurityConfigurer(MyBasicAuthenticationEntryPoint authenticationEntryPoint, UserService userService) {
    this.authenticationEntryPoint = authenticationEntryPoint;
  }

  @Override
  protected void configure(HttpSecurity http) throws Exception {
    http
      .cors().and().csrf().disable()
      .authorizeRequests()
      .antMatchers("/api/public/**").permitAll()
      .antMatchers("/api/items/").permitAll()
      .antMatchers("/api/auth/signin").permitAll()
      .antMatchers("/api/users/add").permitAll()
      .anyRequest().authenticated()
      .and()
      .httpBasic()
      .authenticationEntryPoint(authenticationEntryPoint);
  }

  @Bean
  public PasswordEncoder passwordEncoder() {
    return new BCryptPasswordEncoder();
  }
}
