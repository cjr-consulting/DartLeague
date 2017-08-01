#!/bin/bash

rm .env
echo DARTLEAGUE_HOST_NAME=$(get_octopusvariable "DARTLEAGUE_HOST_NAME") >> .env
echo LETSENCRYPT_HOST=$(get_octopusvariable "LETSENCRYPT_HOST") >> .env
echo LETSENCRYPT_EMAIL=$(get_octopusvariable "LETSENCRYPT_EMAIL") >> .env
echo MYSQL_PASSWORD=$(get_octopusvariable "MYSQL_PASSWORD") >> .env
echo MYSQL_DATA_PATH=$(get_octopusvariable "MYSQL_DATA_PATH") >> .env
echo FILE_STORAGE_PATH=$(get_octopusvariable "FILE_STORAGE_PATH") >> .env
echo SSL_CERT_PATH=$(get_octopusvariable "SSL_CERT_PATH") >> .env

docker-compose -f docker-compose.deploy.yml up -d