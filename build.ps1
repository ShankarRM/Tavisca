dotnet restore
dotnet build

dotnet run --project .\Tavisca.MerchantsOfTheGalaxy\Tavisca.MerchantsOfTheGalaxy.csproj
dotnet test .\Tavisca.MerchantsOfTheGalaxy.Test -v n --no-build --no-restore

