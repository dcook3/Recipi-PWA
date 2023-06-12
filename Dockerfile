FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY Foodi-Application-NET-7.0.csproj .
RUN dotnet restore Foodi-Application-NET-7.0.csproj
COPY . .
RUN dotnet build Foodi-Application-NET-7.0.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Foodi-Application-NET-7.0.csproj -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf
