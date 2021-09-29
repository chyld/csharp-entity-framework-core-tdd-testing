CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS "Students" (
    "Name" TEXT NOT NULL CONSTRAINT "PK_Students" PRIMARY KEY,
    "Color" INTEGER NOT NULL
);
