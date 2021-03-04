FROM mysql:5.7.22
EXPOSE 3306
COPY ./RestAspeNet5/db/migrations/ /home/database/
COPY ./RestAspeNet5/db/dataset/ /home/database/
COPY ./RestAspeNet5/ci/initi_Database.sh/ /docker-entrypoint-initdb.d/initi_Database.sh