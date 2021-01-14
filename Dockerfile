FROM mcr.microsoft.com/dotnet/runtime:3.1
WORKDIR /app
COPY publish ./
EXPOSE 80
ENTRYPOINT ["dotnet", "Steeltoe.Server.dll"]