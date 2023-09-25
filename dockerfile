FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /app

# Copy the project file and restore dependencies
COPY ValidateUserMS.csproj .
RUN dotnet restore

# Copy the application code and build
COPY . .
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app
EXPOSE 8080
# Copy the published application from the build stage
COPY --from=build /app/out .

# Specify the entry point for the application
ENTRYPOINT ["dotnet", "ValidateUserMS.dll"]