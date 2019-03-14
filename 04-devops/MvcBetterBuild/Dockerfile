# multi-stage build - FROM command can name a stage.

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build

WORKDIR /app

COPY . ./

# build & publish to /app/out
RUN dotnet publish -c Release -o out

# docker allows us to use some layers just temporarily
# and then copy certain files out of them onto a new base image.
# this means the resulting final image will be smaller.

# asp.net core runtime image. (much smaller than sdk!)
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime

WORKDIR /app

# copy from a previous stage, instead of from outside Docker
COPY --from=build /app/out ./

# there is technical difference between CMD and ENTRYPOINT

ENTRYPOINT [ "dotnet", "MvcBetterBuild.dll" ]

# best of both worlds from multi-stage build:
# repeatable build environment, plus small image size
