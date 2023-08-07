FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY Foodi-Application-NET-7.0.csproj .
RUN dotnet restore Recipi-PWA.csproj
COPY . .
RUN dotnet build Recipi-PWA.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Foodi-Application-NET-7.0.csproj -c Release -o /app/publish

FROM nginx:alpine AS final
EXPOSE 8080
WORKDIR /usr/shared/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf
