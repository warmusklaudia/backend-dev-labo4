FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 3000

#ENV ASPNETCORE_URLS=http://+:5212

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["Backend_Labo_01_Cars.csproj", "./"]
RUN dotnet restore "Backend_Labo_01_Cars.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Backend_Labo_01_Cars.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Backend_Labo_01_Cars.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Backend_Labo_01_Cars.dll"]
