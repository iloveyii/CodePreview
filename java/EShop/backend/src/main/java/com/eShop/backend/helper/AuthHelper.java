package com.eShop.backend.helper;

public class AuthHelper {
  public static String getBasicAuthToken(String username, String password) {
    String auth = username + ":" + password;
    return "Basic " + javax.xml.bind.DatatypeConverter.printBase64Binary(auth.getBytes());
  }
}
