CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS "Tags" (
    "Name" TEXT NOT NULL CONSTRAINT "PK_Tags" PRIMARY KEY,
    "Color" INTEGER NOT NULL
);
CREATE TABLE IF NOT EXISTS "Todos" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Todos" PRIMARY KEY AUTOINCREMENT,
    "Status" INTEGER NOT NULL,
    "Priority" INTEGER NOT NULL,
    "Title" TEXT NULL,
    "Due" TEXT NOT NULL
);
CREATE TABLE sqlite_sequence(name,seq);
CREATE TABLE IF NOT EXISTS "Comments" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Comments" PRIMARY KEY AUTOINCREMENT,
    "Text" TEXT NULL,
    "TodoId" INTEGER NULL,
    CONSTRAINT "FK_Comments_Todos_TodoId" FOREIGN KEY ("TodoId") REFERENCES "Todos" ("Id") ON DELETE RESTRICT
);
CREATE TABLE IF NOT EXISTS "TagTodo" (
    "TagsName" TEXT NOT NULL,
    "TodosId" INTEGER NOT NULL,
    CONSTRAINT "PK_TagTodo" PRIMARY KEY ("TagsName", "TodosId"),
    CONSTRAINT "FK_TagTodo_Tags_TagsName" FOREIGN KEY ("TagsName") REFERENCES "Tags" ("Name") ON DELETE CASCADE,
    CONSTRAINT "FK_TagTodo_Todos_TodosId" FOREIGN KEY ("TodosId") REFERENCES "Todos" ("Id") ON DELETE CASCADE
);
CREATE INDEX "IX_Comments_TodoId" ON "Comments" ("TodoId");
CREATE INDEX "IX_TagTodo_TodosId" ON "TagTodo" ("TodosId");
