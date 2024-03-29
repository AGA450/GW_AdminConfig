USE [Gateway]
GO
/****** Object:  StoredProcedure [dbo].[usp_GW_GetLaneDevices]    Script Date: 24-01-2024 10:40:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GW_GetLaneDevices]
(	-- Add the parameters for the stored procedure here
	@DeviceId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT  [DeviceId]
      ,[LaneId]
      ,[Type]
      ,[SubType]
      ,[Brand]
      ,[Model]
      ,[IPAddress]
      ,[Port]
      ,[ExtraField1]
      ,[ExtraField2]
      ,[LastUpdatedBy]
      ,[LastUpdateTime]
  FROM [Gateway].[dbo].[gw_LaneDevices]
  WHERE DeviceId=@DeviceId
END
--exec usp_GW_GetLaneDevices '01'
GO
/****** Object:  StoredProcedure [dbo].[usp_GW_GetPathSettings]    Script Date: 24-01-2024 10:40:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GW_GetPathSettings]
 (
 @Category NVARCHAR(20),
 @Name NVARCHAR(50)
 )
	---- Add the parameters for the stored procedure here
	
AS
BEGIN
	SET NOCOUNT ON;
   SELECT [Category]
		  ,[Name]
		  ,[Value]
		  ,[ExtraField1]
		  ,[ExtraField2]
		  ,[LastUpdatedBy]
		  ,[LastUpdateTime]
  FROM [Gateway].[dbo].[gw_Settings]
  WHERE Category=@Category AND Name=@Name
END

GO
/****** Object:  StoredProcedure [dbo].[usp_GW_InsertConfigSettings]    Script Date: 24-01-2024 10:40:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GW_InsertConfigSettings]
 (
 @Category NVARCHAR(20),
 @Name NVARCHAR(50),
 @Value NVARCHAR(250),
 @ExtraField1 NVARCHAR(100),
 @ExtraField2 NVARCHAR(100),
 @LastUpdatedBy NVARCHAR(20)
 )
	---- Add the parameters for the stored procedure here
	
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [Gateway].[dbo].[gw_Settings](Category,Name,Value,ExtraField1,ExtraField2,LastUpdatedBy,LastUpdateTime)
	VALUES(@Category,@Name,@Value,@ExtraField1,@ExtraField2,@LastUpdatedBy,GETDATE())
   
END

GO
/****** Object:  StoredProcedure [dbo].[usp_GW_LaneDevices]    Script Date: 24-01-2024 10:40:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GW_LaneDevices]
 
	---- Add the parameters for the stored procedure here
AS
BEGIN
	SET NOCOUNT ON;
   SELECT  [DeviceId]
      ,[LaneId]
      ,[Type]
      ,[SubType]
      ,[Brand]
      ,[Model]
      ,[IPAddress]
      ,[Port]
      ,[ExtraField1]
      ,[ExtraField2]
      ,[LastUpdatedBy]
      ,[LastUpdateTime]
  FROM [Gateway].[dbo].[gw_LaneDevices]
END

GO
/****** Object:  StoredProcedure [dbo].[usp_GW_PathSettings]    Script Date: 24-01-2024 10:40:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[usp_GW_PathSettings]
 
	---- Add the parameters for the stored procedure here
	--<@Param1, sysname, @p1> <Datatype_For_Param1, , int> = <Default_Value_For_Param1, , 0>, 
	--<@Param2, sysname, @p2> <Datatype_For_Param2, , int> = <Default_Value_For_Param2, , 0>
AS
BEGIN
	SET NOCOUNT ON;
   SELECT [Category]
		  ,[Name]
		  ,[Value]
		  ,[ExtraField1]
		  ,[ExtraField2]
		  ,[LastUpdatedBy]
		  ,[LastUpdateTime]
  FROM [Gateway].[dbo].[gw_Settings]
END

GO
/****** Object:  StoredProcedure [dbo].[usp_GW_UpdateConfigSettings]    Script Date: 24-01-2024 10:40:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GW_UpdateConfigSettings]
 (
 @Category NVARCHAR(20),
 @Name NVARCHAR(50),
 @Value NVARCHAR(250),
 @ExtraField1 NVARCHAR(100),
 @ExtraField2 NVARCHAR(100),
 @LastUpdatedBy NVARCHAR(20)
 )
	---- Add the parameters for the stored procedure here
	
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE  [Gateway].[dbo].[gw_Settings]
	SET Value=@Value,ExtraField1=@ExtraField1,ExtraField2=@ExtraField2
	,LastUpdatedBy=@LastUpdatedBy,LastUpdateTime=GETDATE()
	WHERE Category=@Category AND Name=@Name
END

GO
