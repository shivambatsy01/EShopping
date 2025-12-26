in root directory of project i.e. EShopping :
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
run above command once docker desktop is running, It will spin up all the containers in daemon mode (-d)
After the command suceed, in container we will see catalogservice container will be up and running in docker desktop


lsof -i :9000
docker stop $(docker ps -aq)
docker rm $(docker ps -aq)
docker network prune
TO BUILD : docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --build
TO RUN/START : docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d start
TO STOP : docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d stop or "docker-compose down"


Runnning application locally : port : 5284 (API/Properties/launchSettings.json)
- Make sure mongodb container is running
- Run in debug mode in IDE
http://localhost:5284/swagger/index.html 

Running application in container :
-Application will run on port "9000:8080" default (Docker-compose file)
-Container will run on 9000 and it will redirect to 8080 port on which application will be running inside the container.


------------------------------------------------------------------------------------------------------
Access container application : 
Portainer : http://localhost:9090/#!/wizard (Internally running on 9000, check using "docker logs <ContaineName>" command)
portainer username : admin,  password : admin1234567
Portainer is a lightweight, open-source management UI for container orchestration platforms like Docker, Docker Swarm, and Kubernetes

CatalogAPI : http://localhost:9000/swagger/index.html (Internally running on 8080 of it's container)
BasketAPI : http://localhost:9001/swagger/index.html (Internally running on 8080 of it's container)

To Open UI for grpc service (DiscountAPI) running in docker, use grpc client (grpcUI)
- configure reflections in program.cs
- brew install grpcui (in Mac)
- run command :   grpcui -plaintext localhost:9002, this will open a webUI in browser

pgadmin : used to access docker container based postgres db server (used for discountdb) via web browser
http://localhost:5050/login?next=/
usename and password are in docker-compose-override.yml
PGADMIN_DEFAULT_EMAIL=admin@eShop.net
PGADMIN_DEFAULT_PASSWORD=admin1234