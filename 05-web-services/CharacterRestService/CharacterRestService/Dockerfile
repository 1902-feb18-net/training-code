FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["CharacterRestService/CharacterRestService.csproj", "CharacterRestService/"]
RUN dotnet restore "CharacterRestService/CharacterRestService.csproj"
COPY . .
WORKDIR "/src/CharacterRestService"
RUN dotnet build "CharacterRestService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CharacterRestService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CharacterRestService.dll"]