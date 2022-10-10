FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
ENV SRC_DIR=/usr/src/MainSite
COPY src $SRC_DIR
RUN dotnet restore $SRC_DIR \
    --runtime linux-x64
RUN dotnet publish $SRC_DIR/Client \
    --runtime linux-x64 \
    --framework net6.0 \
    --self-contained \
    --configuration Release \
    --output /build

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /build .
EXPOSE 80
ENTRYPOINT ["dotnet", "Steeltoe.Client.dll"]
