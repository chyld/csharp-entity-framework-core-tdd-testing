rm db/app.db
rm -rf lib/Migrations
dotnet ef migrations add Initial --project lib
dotnet ef database update --project lib
sqlite3 db/app.db ".schema" > sql/schema.sql
# sqlite3 db/app.db ".read sql/inserts.sql"

