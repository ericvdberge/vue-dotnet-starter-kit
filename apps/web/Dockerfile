FROM node:20-alpine AS build

WORKDIR /app

# Enable Corepack + pnpm
RUN corepack enable && corepack prepare pnpm@latest --activate

# Copy only lock + manifest first (cache optimization)
COPY package.json pnpm-lock.yaml ./

# Install deps
RUN pnpm install --frozen-lockfile

# Copy rest of source
COPY . .

# Build
RUN pnpm build


FROM nginx:alpine

RUN rm /etc/nginx/conf.d/default.conf
COPY nginx.conf /etc/nginx/conf.d/default.conf

COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]