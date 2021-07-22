FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
ENV SRC_DIR=/usr/src/MainSite
COPY src $SRC_DIR
RUN dotnet restore $SRC_DIR \
    --runtime linux-x64
RUN dotnet publish $SRC_DIR/Server \
    --runtime linux-x64 \
    --framework net5.0 \
    --self-contained \
    --configuration Release \
    --output /build

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /build .
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT=Staging
ENTRYPOINT ["dotnet", "Steeltoe.Client.dll"]
