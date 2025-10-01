in root directory of project i.e. EShopping :
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
run above command once docker desktop is running, It will spin up all the containers in daemon mode (-d)
After the command suceed, in container we will see catalogservice container will be up and running in docker desktop


lsof -i :9000
docker stop $(docker ps -aq)
docker rm $(docker ps -aq)
docker network prune
TO BUILD : docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --build
TO RUN/START : docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d  


Runnning application locally : port : 5284 (API/Properties/launchSettings.json)
- Make sure mongodb container is running
- Run in debug mode in IDE
http://localhost:5284/swagger/index.html 

Running application in container :
-Application will run on port "9000:8080" default (Docker-compose file)
-Container will run on 9000 and it will redirect to 8080 port on which application will be running inside the container.