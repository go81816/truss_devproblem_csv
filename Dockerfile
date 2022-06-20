#FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.csproj .
#RUN dotnet restore -r linux-musl-x64
RUN dotnet restore -r linux-x64

# copy and publish app and libraries
COPY . .
#RUN dotnet publish -c release -o /app -r linux-musl-x64 --self-contained false --no-restore
RUN dotnet publish -c release -o /app -r linux-x64 --self-contained false --no-restore

# final stage/image
#FROM mcr.microsoft.com/dotnet/runtime:6.0-alpine-amd64
FROM mcr.microsoft.com/dotnet/runtime:6.0-focal-amd64
WORKDIR /app
COPY --from=build /app .

# See: https://github.com/dotnet/announcements/issues/20
# Uncomment to enable globalization APIs (or delete)
# ENV \
#     DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
#     LC_ALL=en_US.UTF-8 \
#     LANG=en_US.UTF-8
# RUN apk add --no-cache icu-libs

ENTRYPOINT ["./normalizer"]