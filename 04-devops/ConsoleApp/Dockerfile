# before building this image, I should run
#     dotnet publish -c Release -o out

# the parts of this image identifier...

#    "registry"      "name/repository"    "tag"
#          v             v                 v
FROM mcr.microsoft.com/dotnet/core/runtime:2.2

# this copies out/ folder next to the Dockerfile ("outside Docker")
# to /app folder inside Docker image.
COPY out/ /app

# this sets bash as the command to run when container starts
# CMD [ "/bin/bash" ]
# this sets "dotnet /app/ConsoleApp.dll" to run when container starts.
CMD [ "dotnet", "/app/ConsoleApp.dll" ]
