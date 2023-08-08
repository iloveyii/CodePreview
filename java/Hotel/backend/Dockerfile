FROM openjdk:8
RUN apt update
RUN apt install maven -y
WORKDIR /java/app
ADD . .
RUN mvn install
CMD ["java", "-jar", "target/backend-0.0.1-SNAPSHOT.jar"]