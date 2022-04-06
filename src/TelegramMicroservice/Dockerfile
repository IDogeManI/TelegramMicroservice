

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/TelegramMicroservice/TelegramMicroservice.csproj", "src/TelegramMicroservice/"]
RUN dotnet restore "src\TelegramMicroservice\TelegramMicroservice.csproj"
COPY . .
WORKDIR "/src/src/TelegramMicroservice"
RUN dotnet build "TelegramMicroservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TelegramMicroservice.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TelegramMicroservice.dll"]
