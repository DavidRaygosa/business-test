FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out

#RUNTIME
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /src
COPY --from=build /src/out ./

ENV Env Production
ENV EncyptionPswd b14ca5898a4e4146bbce2ea2315a1906
ENV DefaultRoleId 2

EXPOSE 80
ENTRYPOINT [ "dotnet" , "BusinessTest.Application.dll" ]