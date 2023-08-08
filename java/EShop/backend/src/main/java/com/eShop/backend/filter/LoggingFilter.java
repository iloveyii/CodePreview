package com.eShop.backend.filter;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.core.annotation.Order;
import org.springframework.stereotype.Component;

import javax.servlet.*;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

@Component
@Order(Integer.MIN_VALUE + 50)
public class LoggingFilter implements Filter {

  private static final Logger logger = LoggerFactory.getLogger(LoggingFilter.class);

  @Override
  public void init(FilterConfig filterConfig) {
    logger.debug("Setting up logging filter...");
  }

  @Override
  public void doFilter(ServletRequest servletRequest, ServletResponse servletResponse, FilterChain filterChain) throws IOException, ServletException {

    HttpServletRequest req = (HttpServletRequest) servletRequest;

    logger.debug("{} {}", req.getMethod(), req.getRequestURI());

    filterChain.doFilter(servletRequest, servletResponse);

    HttpServletResponse res = (HttpServletResponse) servletResponse;

    logger.info("{} {} - {} ", req.getMethod(), req.getRequestURI(), res.getStatus());
  }

  @Override
  public void destroy() {
    logger.debug("Destroying logging filter.");
  }
}
