# use sdk image, instead of runtime image.
# contains full dotnet command we expect. larger base image.
FROM mcr.microsoft.com/dotnet/core/sdk:2.2

# "cd" into directory (creating if necessary)
WORKDIR /app/src

# copy whole dir containing Dockerfile....
#  ... into current dir inside the image (/app/src, right now)
COPY ./ .

RUN dotnet build -c Release

CMD [ "dotnet", "run" ]

# build happens inside docker - maximum repeatability
# for build as well as for runtime.
