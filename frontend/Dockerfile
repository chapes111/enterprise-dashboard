# Build Stage
FROM node:20-alpine AS build
WORKDIR /app
COPY package*.json ./
RUN npm ci
COPY . .
RUN npm run build

# Runtime Stage
FROM node:20-alpine AS final
WORKDIR /app
COPY --from=build /app/package*.json ./
COPY --from=build /app/build ./build
COPY --from=build /app/node_modules ./node_modules
EXPOSE 5173
ENV PORT=5173 HOST=0.0.0.0
CMD ["node", "build"]
