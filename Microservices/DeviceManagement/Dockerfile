FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY DHLM.DeviceManagement.API/*.csproj ./

RUN dotnet restore

EXPOSE 5000:80
ENV planet=earth
ENV DeviceName = "aStrangeDevice"
ENV ConnString = "HostName=naseemtestinghub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=S6/P3koWnUTbSrenNOKWa0kE/9HFnE+d2EnrnWO0dPx="

# Copy everything else and build
COPY . ./
RUN dotnet publish DHLM.DeviceManagement.API/DHLM.DeviceManagement.API.csproj -c Release -o /app/out

# Build runtime image
FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "DHLM.DeviceManagement.API.dll"]