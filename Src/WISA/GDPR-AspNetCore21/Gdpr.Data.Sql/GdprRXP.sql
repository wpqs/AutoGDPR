﻿CREATE TABLE [dbo].[GdprRXP]
(
    [Id] UNIQUEIDENTIFIER CONSTRAINT [DF_GdprRXP_Id] DEFAULT NEWID() NOT NULL PRIMARY KEY NONCLUSTERED, 
    [ProcFlags] INT CONSTRAINT [DF_GdprRXP_ProcFlags] DEFAULT 0 NOT NULL, 
    [FpdId] UNIQUEIDENTIFIER  CONSTRAINT [FK_GdprRXP_FpdId] FOREIGN KEY REFERENCES [dbo].[GdprFPD] (Id) ON DELETE CASCADE  NOT NULL,  
    [UrdId] UNIQUEIDENTIFIER CONSTRAINT [FK_GdprRXP_UrdId] FOREIGN KEY REFERENCES [dbo].[GdprURD] (Id) ON DELETE CASCADE  NOT NULL, 
)
GO;
CREATE UNIQUE NONCLUSTERED INDEX [IX_GdprRXP_FpdId] ON [dbo].[GdprRXP] ([FpdId])
GO;
CREATE UNIQUE NONCLUSTERED INDEX [IX_GdprRXP_UrdId] ON [dbo].[GdprRXP] ([UrdId])
GO;