FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RazorStore/RazorStore.csproj", "RazorStore/"]
COPY ["RazorStoe.Model/RazorStore.Model.csproj", "RazorStoe.Model/"]
COPY ["RazorStore.Services/RazorStore.Services.csproj", "RazorStore.Services/"]
RUN dotnet restore "RazorStore/RazorStore.csproj"
COPY . .
WORKDIR "/src/RazorStore"
RUN dotnet build "RazorStore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RazorStore.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RazorStore.dll"]
