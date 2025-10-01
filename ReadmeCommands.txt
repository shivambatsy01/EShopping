in root directory of project i.e. EShopping :
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
run above command once docker desktop is running, It will spin up all the containers in daemon mode (-d)
After the command suceed, in container we will see catalogservice container will be up and running in docker desktop


lsof -i :9001
docker stop $(docker ps -aq)
docker rm $(docker ps -aq)
docker network prune
TO BUILD : docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --build
TO RUN/START : docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d  


Runnning application locally :
- Make sure mongodb container is running
- Run in debug mode in IDE
http://localhost:5284/swagger/index.html 