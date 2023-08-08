#!/bin/bash
echo "Stop container"
echo "This is deprecated please use docker compose"
docker stop blazorapp 
echo "Pull new changes from master branch"
git pull origin master 
echo "Build image"
docker build -t blazorapp .
echo "Run image "
docker run --name blazorapp -d -p 5005:3000 --rm blazorapp

