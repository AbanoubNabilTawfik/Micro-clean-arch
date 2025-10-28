FROM nginx

COPY APIGateway/nginx/nginx.local.conf /etc/nginx/nginx.conf
COPY APIGateway/nginx/id-local.crt /etc/ssl/certs/id-local.eshopping.com.crt
COPY APIGateway/nginx/id-local.key /etc/ssl/private/id-local.eshopping.com.key