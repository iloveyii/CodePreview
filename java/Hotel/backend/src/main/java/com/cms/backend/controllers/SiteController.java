package com.cms.backend.controllers;

import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.json.simple.parser.ParseException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.*;

import java.io.*;
import java.util.Arrays;
import java.util.List;

@CrossOrigin(origins = "*", maxAge = 3600)
@Controller
public class SiteController {

    @Value("${server.web}")
    private String serverWeb;

    @Value("${server.admin}")
    private String serverAdmin;

    private String siteLanguage = "sv";

    Logger logger = LogManager.getLogger(SiteController.class);
    private String[] langs = {"sv", "en", "ar"};


    private String render(String host, String lang, Model model) {
        List<String> listLangs = Arrays.asList(langs);
        siteLanguage = listLangs.contains(lang) ? lang : "sv";

        JSONObject langContent = readTransFiles(siteLanguage);
        model.addAttribute("lang", langContent);

        if((host + "").contains(((String)serverWeb) + "")) {
            return "index/index";
        } else {
            return "admin/index";
        }
    }

    @RequestMapping({ "/{lang}"})
    public String localizedIndex(@RequestHeader String host,  @PathVariable(value="lang") String lang, Model model) {
        return render(host, lang, model);
    }

    @RequestMapping({"/",  "/inquiries"})
    public String index(@RequestHeader String host,  Model model) {
        return render(host, siteLanguage, model);
    }

    private JSONObject readTransFiles(String lang) {
        JSONParser parser = new JSONParser();

        try {
            InputStream in = getClass().getResourceAsStream("/trans.json");
            BufferedReader reader = new BufferedReader(new InputStreamReader(in));
            String strCurrentLine;
            String jsonContent = "";

            while ((strCurrentLine = reader.readLine()) != null) {
                jsonContent = jsonContent + (strCurrentLine);
            }
            // System.out.println(jsonContent);

            Object oJson = parser.parse( jsonContent );
            JSONObject jsonObject = (JSONObject) oJson;
            JSONObject langContent = (JSONObject) jsonObject.get(lang);
            logger.info(langContent);
            return  langContent;
        }
        catch(FileNotFoundException e){e.printStackTrace();}
        catch(IOException e){e.printStackTrace();}
        catch(ParseException e){e.printStackTrace();}
        catch(Exception e){e.printStackTrace();}

        return null;
    }
}
