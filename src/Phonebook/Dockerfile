FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app

ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /source
COPY ["src/Phonebook/Phonebook.csproj", "source/Phonebook/"]
RUN dotnet restore "source/Phonebook/Phonebook.csproj"
COPY . .
WORKDIR "/source/src/Phonebook"
RUN dotnet build "Phonebook.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Phonebook.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Phonebook.dll"]
