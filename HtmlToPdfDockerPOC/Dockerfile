
# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["HtmlToPdfDockerPOC.csproj", "./"]
RUN dotnet restore "./HtmlToPdfDockerPOC.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "HtmlToPdfDockerPOC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HtmlToPdfDockerPOC.csproj" -c Release -o /app/publish

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN icacls "wwwroot" /grant Everyone:F /t
RUN icacls "appsettings.json" /grant Everyone:W

ENTRYPOINT ["dotnet", "HtmlToPdfDockerPOC.dll"]
