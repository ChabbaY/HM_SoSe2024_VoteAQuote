#build
FROM node:18-alpine as build
WORKDIR /usr/local/app
COPY ./ /usr/local/app/
RUN npm install
RUN npm run build --prod

#run
FROM node:18-alpine
WORKDIR /usr/app
COPY --from=build /usr/local/app/dist/angular ./
CMD node server/server.mjs
EXPOSE 4000