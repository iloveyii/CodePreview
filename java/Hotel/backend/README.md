JAVA Spring boot
================
Spring boot based web application

## Installations

# Installations
## Windows
- Download [IntelliJ](https://www.jetbrains.com/idea/download/download-thanks.html?platform=windows)
- Download [Docker Desktop](https://docs.docker.com/docker-for-windows/install/)
- Download [Git Desktop](https://git-scm.com/download/win)
    - You need to enable hyper-v/virtualization in your computer bios OR vmware player settings > hardware > processor > virtualization engine.
    - If memory problem, then infact decreasing it from 4GB to something lower will work
- Download [WSL2 update](https://docs.microsoft.com/en-us/windows/wsl/install-win10)


## Ubuntu

### Java
- Install update `sudo apt update`
- Install jdk `sudo apt install openjdk-11-jdk`
- Install jre `sudo apt install openjdk-11-jre`
- Some applications still don’t fully support the latest OpenJDK 11. For those, they can install the previous Java LTS which was version 8.
- `sudo apt install openjdk-8-jdk`
- If you have multiple versions of Java installed, simply use the commands below to set which one should be the default for your system.
- `sudo update-alternatives --config java`
- Some programs require that JAVA_HOME is configured on the system. You can set the default home by using the lines above in the config file.
- For Java 11, it displays /usr/lib/jvm/java-11-openjdk-amd64 and Java 8, it’s /usr/lib/jvm/java-8-openjdk-amd64.
- To set their homes, run the commands below to open the system environment file.
- `sudo nano /etc/environment`
- Then add a line for Java 11 as below:
- `JAVA_HOME="/usr/lib/jvm/java-11-openjdk-amd64"`
- Run the commands below to save your changes.
- `source /etc/environment`

### Maven
- Install maven `sudo apt update && sudo apt install maven`
 

### Spring
- Install plugin in Settings > Plugins and search Spring Assistant
- Create spring based app in IJ and add dependencies, web, devtools, actuator
- Create Controller at src main java with annotations like Controller, GetMapping etc
- Open src main java @SpringBootApplication, at the right of class click the green Run button ( CTRL + SHIFT + F10) 


## Configuration
- The `view` paths in templates/application.properties
```yaml
    spring.mvc.view.prefix: /WEB-INF/
    spring.mvc.view.suffix: .html
```

## Testing
- Click inside controller method and click generate from menu or alt+insert
- Add a test : 
    - Arrange: create controller object
    - Act : Run the test
    - Assert: Compare results


## Deploy
- Build jar `mvn clean install`
- Find jar file in target dir and run `java -jar file.jar`
- Run with pm2 `pm2 start 'java -jar file.jar' --name java-tomcat`


## Run with Docker
- Build `docker build -t hotel_backend_image .`
- Run `docker run -ti --rm -v /home/alex/projects/java/hotel_backend/:/java/app/ -p 8090:8090 --name hotel_backend_container hotel_backend_image bash`
- Make jar `mvn clean install`
- Run jar file `java -jar target/backend-0.0.1-SNAPSHOT.jar`

## Run with Docker Compose
- Build `docker-compose up --build`


## Shortcuts IntelliJ
- Insert dependency -> open pom file -> ALT + Insert


## Issues
- Could not initialize class org.jetbrains.jps.builders.JpsBuildBundle
- Solution: Update / reinstall IJ
