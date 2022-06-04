FROM node:latest AS build-env

RUN npm i -g serve

WORKDIR /app

COPY package.json .
RUN npm install

COPY . .
RUN ls -lh
RUN npm run build

ENTRYPOINT ["serve", "-s", "/app/build"]
