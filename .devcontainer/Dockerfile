FROM ghcr.io/ahdcreative/xivita-daladocker:latest
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

# Crea un user non root
RUN adduser --disabled-password --gecos "" dev

# Installa tutte le dipendenze
RUN apk add --update openssh

# Scarica il repository dentro /workspaces
WORKDIR /workspaces 
RUN git clone git@github.com:DarkArtek/XIVITAStartupCommands.git