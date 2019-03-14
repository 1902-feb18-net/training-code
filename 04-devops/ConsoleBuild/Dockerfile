FROM mcr.microsoft.com/dotnet/core/sdk:2.2

# "cd" into directory
WORKDIR /app/src

# copy whole dir containing Dcckerfile....
#  ... into current dir inside the image (/app/src, right now)
COPY ./ .

RUN dotnet build -c Release

CMD [ "dotnet", "run" ]
