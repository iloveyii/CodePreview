FROM maven
WORKDIR /backend
COPY . .
RUN mvn clean install

ADD ./wait-for-it.sh /wait-for-it.sh
RUN chmod +x /wait-for-it.sh

CMD ./wait-for-it.sh -h $DATABASE_DOCKER_HOST -p $DATABASE_DOCKER_PORT  -- mvn spring-boot:run
