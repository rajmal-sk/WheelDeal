# Use the official .NET SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
# Set the working directory in the build environment
WORKDIR /app 
# Expose port 80 for the application
EXPOSE 80

# copy all .csproj files and restore as distinct layers. Use of the same COPY command
# for every dockerfile in the project to take advantage of docker caching. 
COPY WheelDeal.sln WheelDeal.sln
COPY src/AuctionService/AuctionService.csproj src/AuctionService/AuctionService.csproj
COPY src/SearchService/SearchService.csproj src/SearchService/SearchService.csproj
COPY src/GatewayService/GatewayService.csproj src/GatewayService/GatewayService.csproj
COPY src/Contracts/Contracts.csproj src/Contracts/Contracts.csproj
COPY src/IdentityService/IdentityService.csproj src/IdentityService/IdentityService.csproj
COPY src/BiddingService/BiddingService.csproj src/BiddingService/BiddingService.csproj
COPY src/NotificationService/NotificationService.csproj src/NotificationService/NotificationService.csproj

# Restore package dependencies for the entire solution
RUN dotnet restore WheelDeal.sln

# Copy the source code of the necessary projects to the working directory
COPY src/IdentityService src/IdentityService

# Set the working directory to the AuctionService project
WORKDIR /app/src/IdentityService

# Publish the AuctionService project in Release configuration to the output directory
RUN dotnet publish -c Release -o /app/src/out 

# Use the official .NET runtime image as the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0
# Set the working directory in the runtime environment
WORKDIR /app
# Copy the published output from the build environment to the runtime environment
COPY --from=build /app/src/out .
# Set the entry point for the application
ENTRYPOINT [ "dotnet", "IdentityService.dll" ]