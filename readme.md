# Aspnetcore HelloWorld using Docker

Learn how to build a Docker image and run a container using that image. Requires Docker for Windows with Kubernetes, Hyper-V and git

## Getting Started

Clone the helloworld repo

open Powershell.exe 

mkdir c:\docker\helloworld

cd c:\docker\helloworld

git clone https://github.com/jmccor99/HelloWorld.git

cd helloworld

List the current Docker images and containers

docker images

docker ps -a

### Building the helloworld image and starting a container using the image

docker build . --tag helloworld:latest

## Look at what's happened

Take a look at the new images

docker images

We can see a number of images have been created. 

Two base images: mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine and mcr.microsoft.com/dotnet/core/sdk:3.1-alpine

The <none> image which is make up of the build image layers

The helloworld:latest image which is made up of the runtime image layers

Use docker history imageid to view the layers in each image

The build image layers are thrown away during docker build and only the runtime image layers are used to create the helloworld:latest image. This is a key part of the docker multi phase build process. It allows us to reduce the size of the runtime image by not including the image layers that we do not need.

## Run a new container using the helloworld:latest image

We are going to create and start a new container using the helloworld:latest image. 

We will publish the container port 5001 to the host

We will detach from the container and run it in the background

docker run -d -p 5001:5001 helloworld:latest

## Look inside the helloworld:latest container

Find the running container instance of helloworld:latest

docker ps -a --filter ancestor=helloworld:latest

docker exec {containerid} ls

## Browse to the helloworld web site running inside the helloworld:latest container

Open a browser on the host

http://localhost:5001/

## Stop the helloworld:latest container

docker stop {containerid}

## Remove the helloworld:latest container

docker rm {containerid}

## Remove the helloworld:latest image

docker rmi helloworld:latest

## Remove the base images

docker rmi mcr.microsoft.com/dotnet/core/sdk:3.1-alpine

docker rmi mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine

## Setup Docker hub account

Sign up to https://hub.docker.com/

Replace jmccor99 with your docker hub user name and password

docker login -u jmccor99
enter password:

## Building the helloworld image and starting a container using docker compose

Edit the docker-compose.yml file and replace jmccor99 with your docker hub user name

Build the helloworld:latest image and push to docker hub. Create and run a container using docker hub

docker-compose up --build -d

docker-compose push

docker ps -a

docker-compose down

docker-compose up -d

docker-compose down

## Tidy up

docker stop $(docker ps -aq)

docker rm $(docker ps -aq)

docker rmi --force $(docker images -aq)

## Use Docker stack to deploy a pod of helloworld:latest image containers to kubernetes using docker stack file

Edit the docker-compose.yml file and replace jmccor99 with your docker hub user name

open the docker for windows settings and go to the kubernetes tab. Enable deploy docker stacks to kubernetes by default.

docker swarm init

docker stack deploy --orchestrator=kubernetes -c stack.yml helloworld

docker stack ls

docker stack ps helloworld

docker stack services helloworld 

View the kubernetes configuration

kubectl get nodes

kubectl get services

kubectl get pods

kubectl get rs

## Have some fun

kubectl delete pods

kubectl get pods

kubectl delete -n default deployment helloworld

kubectl get pods

kubectl delete svc helloworld

kubectl delete svc helloworld-published

docker stack rm helloworld

