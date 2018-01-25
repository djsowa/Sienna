FROM nginx:latest


#WORKDIR /amplify

RUN apt-get update
RUN apt-get upgrade -y
RUN apt-get install curl -y
RUN apt-get install gnupg2 -y
RUN apt-get install apache2-utils -y

COPY ./app/default.conf /etc/nginx/conf.d/default.conf

EXPOSE 8080

CMD echo "Starting nginx." && nginx -g 'daemon off;'