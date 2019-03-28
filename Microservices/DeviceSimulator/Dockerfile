FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app

#Copy csproj and restore as distinct layers
COPY DHLM.Vehicle.Simulator/*.csproj ./
#COPY DHLM.Vehicle.Simulator/settings.json /app
#COPY DHLM.Vehicle.Simulator/VehicleData.json /app

RUN dotnet restore

EXPOSE 5000:80
ENV planet=earth
ENV DeviceName = "aStrangeDevice"
ENV ConnString = "HostName=naseemtestinghub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=S6/P3koWnUTbSrenNOKWa0kE/9HFnE+d2EnrnWO0dPx="

#Copy everything else and build
COPY . ./
RUN dotnet publish DHLM.Vehicle.Simulator/DHLM.Vehicle.Simulator.csproj -c Release -o /app/out -f netcoreapp2.1

#Build runtime image
FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
#COPY DHLM.Vehicle.Simulator/settings.json .
#COPY DHLM.Vehicle.Simulator/vehicledata.json .
ENTRYPOINT ["dotnet", "DHLM.Vehicle.Simulator.dll"]
