BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "IssueReport" (
	"id"	guid NOT NULL,
	"created_on"	DATETIME NOT NULL,
	"synced_on"	DATETIME,
	"title"	TEXT NOT NULL,
	"issue_type"	VARCHAR(20) NOT NULL,
	"frequency"	VARCHAR(20) NOT NULL,
	"person_identity"	VARCHAR(100),
	"description"	TEXT,
	"steps_to_produce"	TEXT,
	"machine"	VARCHAR(50) NOT NULL,
	"machine_user"	VARCHAR(50) NOT NULL,
	"client_id"	guid,
	"last_update_on"	DATETIME,
	"other_data"	TEXT,
	"error_msg"	TEXT,
	"error_dump"	TEXT,
	PRIMARY KEY("id")
);
CREATE TABLE IF NOT EXISTS "Log" (
	"id"	GUID NOT NULL UNIQUE,
	"message"	TEXT NOT NULL,
	"log_time"	DATETIME NOT NULL,
	"image_id"	GUID UNIQUE,
	"dump"	TEXT,
	"synced_on"	DATETIME,
	"log_type_alias"	INT NOT NULL,
	"log_type"	VARCHAR(100) NOT NULL,
	"other_data"	TEXT,
	"client_id"	guid,
	"machine"	VARCHAR(50) NOT NULL,
	"machine_user"	VARCHAR(50) NOT NULL,
	"last_update_on"	DATETIME,
	PRIMARY KEY("id"),
	FOREIGN KEY("image_id") REFERENCES "ImgContent"("id")
);
CREATE TABLE IF NOT EXISTS "ImgContent" (
	"id"	GUID NOT NULL UNIQUE,
	"image"	IMAGE NOT NULL,
	"datetime"	DATETIME NOT NULL,
	"client_id"	guid,
	"machine"	varchar(50) NOT NULL,
	"machine_user"	varchar(50) NOT NULL,
	"synced_on"	DATETIME,
	"last_update_on"	DATETIME
);
CREATE INDEX IF NOT EXISTS "Indexes" ON "Log" (
	"id",
	"image_id",
	"synced_on",
	"log_type_alias"	ASC
);
COMMIT;
