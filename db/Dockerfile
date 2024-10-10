COPY dunder-mifflin-infinity-db-schema.sql ./

RUN docker run -i /bin/sh postgres

RUN docker exec ./dunder-mifflin-infinity-db-schema.sql