build:
	docker-compose --env-file ./.env build

start:
	docker-compose --env-file ./.env up -d

stop:
	docker-compose --env-file ./.env stop

down:
	docker-compose --env-file ./.env down

all: build start
clean: down
