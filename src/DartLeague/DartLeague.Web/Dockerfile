FROM microsoft/dotnet:2.1-aspnetcore-runtime
ARG source
WORKDIR /app
EXPOSE 80
COPY ${source:-obj/Docker/publish} .
RUN mkdir /storage
VOLUME ["/storage"]
ENTRYPOINT ["dotnet", "DartLeague.Web.dll"]
