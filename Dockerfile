FROM mcr.microsoft.com/dotnet/sdk:3.1-alpine AS build
ENV SRC_DIR=/usr/src/MainSite
COPY src $SRC_DIR
RUN dotnet restore $SRC_DIR \
    --runtime linux-x64
RUN dotnet publish $SRC_DIR/Server \
    --runtime linux-x64 \
    --framework netcoreapp3.1 \
    --self-contained \
    --configuration Release \
    --output /build

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /build .
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT=Staging
ENTRYPOINT ["dotnet", "Steeltoe.Server.dll"]
