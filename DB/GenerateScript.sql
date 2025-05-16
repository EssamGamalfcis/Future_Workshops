 
CREATE TABLE "Projects" (
    "Id" UUID PRIMARY KEY,
    "Name" TEXT NOT NULL,
    "CreatedDate" TIMESTAMP NOT NULL
);
 
CREATE TABLE "TaskItems" (
    "Id" UUID PRIMARY KEY,
    "Title" TEXT NOT NULL,
    "Description" TEXT NOT NULL,
    "DueDate" TIMESTAMP NOT NULL,
    "Status" INTEGER NOT NULL,  
    "ProjectId" UUID NOT NULL,
    CONSTRAINT "FK_TaskItems_Projects_ProjectId"
        FOREIGN KEY ("ProjectId")
        REFERENCES "Projects"("Id")
        ON DELETE CASCADE
);
 
