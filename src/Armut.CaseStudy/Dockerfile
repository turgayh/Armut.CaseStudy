#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Armut.CaseStudy/Armut.CaseStudy.csproj", "src/Armut.CaseStudy/"]
COPY ["src/Armut.CaseStudy.Model/Armut.CaseStudy.Model.csproj", "src/Armut.CaseStudy.Model/"]
COPY ["src/Armut.CaseStudy.Operation/Armut.CaseStudy.Operation.csproj", "src/Armut.CaseStudy.Operation/"]
RUN dotnet restore "src/Armut.CaseStudy/Armut.CaseStudy.csproj"
COPY . .
WORKDIR "/src/src/Armut.CaseStudy"
RUN dotnet build "Armut.CaseStudy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Armut.CaseStudy.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Armut.CaseStudy.dll"] 