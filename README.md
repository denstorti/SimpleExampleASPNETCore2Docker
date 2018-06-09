# ASPNET Core 2 (Docker) + MSSQL Linux (Docker)
Simple Example ASPNET Core 2 Web API  with SQL Server, both running on Docker.

## Pre-requisites
* Docker installed

## Running
1. After cloning, in the main folder execute:
docker-compose build
2. Then:
docker-compose up
3. Test it:
Call one of the APIs: http://localhost:9090/api/v1/public/products

### Problems
If you have problems running "docker-compose up" because there is already volumes, networks or containers with the same name, you can solve this by running "docker system prune". 
Be aware that several artifacts will be dropped, use only with nothing else created on Docker matters.

## API structure (simple examples)
See swagger docs:
http://localhost:9090/swagger/v1/public/

curl -X GET "http://localhost:9090/api/v1/public/products" -H "accept: application/json"

curl -X GET "http://localhost:9090/api/v1/public/categories" -H "accept: application/json"

curl -X POST "http://localhost:9090/api/v1/public/categories" -H "accept: application/json" -H "Content-Type: application/json-patch+json" -d "{ \"title\": \"NEW CATEGORY\", \"description\": \"new category description\"}"

curl -X POST "http://localhost:9090/api/v1/public/products" -H "accept: application/json" -H "Content-Type: application/json-patch+json" -d "{ \"title\": \"NEW PRODUCT\", \"price\": 20, \"categoryId\": 1}"
