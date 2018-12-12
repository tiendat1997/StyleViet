CREATE TABLE [dbo].[SalonService](
	[SalonId] [int] NOT NULL,
	[ServiceId] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [float] NOT NULL,
	[Disabled] [bit] NOT NULL,
 CONSTRAINT [PK_SalonService] PRIMARY KEY CLUSTERED 
(
	[SalonId] ASC,
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[SalonService]  WITH CHECK ADD  CONSTRAINT [FK_SalonService_Salon_SalonId] FOREIGN KEY([SalonId])
REFERENCES [dbo].[Salon] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SalonService] CHECK CONSTRAINT [FK_SalonService_Salon_SalonId]
GO
ALTER TABLE [dbo].[SalonService]  WITH CHECK ADD  CONSTRAINT [FK_SalonService_Service_ServiceId] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Service] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SalonService] CHECK CONSTRAINT [FK_SalonService_Service_ServiceId]
GO
/****** Object:  Index [IX_SalonService_ServiceId]    Script Date: 12/12/2018 9:56:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_SalonService_ServiceId] ON [dbo].[SalonService]
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]