**Create Migration**
####
dotnet ef migrations add AddInitProject --project .\Notes.Persistence\Notes.Persistence.csproj --startup-project .\Notes.WebApi\No
tes.WebApi.csproj --context NotesDbContext

****
**Run migration**
####
dotnet ef database update AddInitProject --project .\Notes.Persistence\Notes.Persistence.csproj --startup-project .\Notes.WebApi\N
otes.WebApi.csproj --context NotesDbContext