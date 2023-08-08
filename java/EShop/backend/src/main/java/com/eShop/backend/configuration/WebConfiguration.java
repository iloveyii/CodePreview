package com.eShop.backend.configuration;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.servlet.config.annotation.CorsRegistry;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurer;

@Configuration
public class WebConfiguration implements WebMvcConfigurer {
  private static final Logger logger = LoggerFactory.getLogger(WebConfiguration.class);

  @Override
  public void addCorsMappings(CorsRegistry registry) {
    logger.info("Setting up CORS policy...");

    registry.addMapping("/**")
      .allowedMethods("*")
      .allowCredentials(true)
      .allowedOrigins("http://localhost:4200", "http://localhost:8080");
    //.allowCredentials(true)
    //.allowedOriginPatterns("localhost:4200", "localhost:8080");
  }
}
