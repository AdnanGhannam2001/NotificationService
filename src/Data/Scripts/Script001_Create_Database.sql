CREATE TABLE IF NOT EXISTS "Notifications" (
    "Id" VARCHAR(255) NOT NULL,
    "UserId" VARCHAR(255) NOT NULL,
    "IsRead" BOOLEAN DEFAULT FALSE,
    "Content" VARCHAR(1000) NOT NULL, 
    "Link" VARCHAR(250) NOT NULL, 
    "CreatedAtUtc" TIMESTAMPTZ,
    "UpdatedAtUtc" TIMESTAMPTZ,
    PRIMARY KEY ("Id"));