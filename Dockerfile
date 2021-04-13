FROM mcr.microsoft.com/dotnet/runtime:3.1
WORKDIR /app
COPY publish ./
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT=Staging
ENTRYPOINT ["dotnet", "Steeltoe.Server.dll"]
