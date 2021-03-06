USE [master]
GO
/****** Object:  Database [Task_Api]    Script Date: 09-09-2021 09:52:02 ******/
CREATE DATABASE [Task_Api]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Task_Api', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Task_Api.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Task_Api_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Task_Api_log.ldf' , SIZE = 816KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Task_Api] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Task_Api].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Task_Api] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Task_Api] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Task_Api] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Task_Api] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Task_Api] SET ARITHABORT OFF 
GO
ALTER DATABASE [Task_Api] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Task_Api] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Task_Api] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Task_Api] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Task_Api] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Task_Api] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Task_Api] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Task_Api] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Task_Api] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Task_Api] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Task_Api] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Task_Api] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Task_Api] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Task_Api] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Task_Api] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Task_Api] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Task_Api] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Task_Api] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Task_Api] SET  MULTI_USER 
GO
ALTER DATABASE [Task_Api] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Task_Api] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Task_Api] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Task_Api] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Task_Api] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Task_Api]
GO
/****** Object:  Table [dbo].[tbl_Student]    Script Date: 09-09-2021 09:52:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Student](
	[StudentID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Age] [int] NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](10) NULL,
	[Address] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  StoredProcedure [dbo].[USP_Add_Update_Student]    Script Date: 09-09-2021 09:52:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[USP_Add_Update_Student] 
@studentID int ,
@FirstName nvarchar(50),
@LastName nvarchar(50) ,
@Age int  null,
@Email nvarchar(50)  null,
@Phone nvarchar(50)  null,
@Address nvarchar(50)  null
AS
BEGIN

if  (@studentID!=0 )
BEGIN
UPDATE tbl_Student
SET FirstName = @FirstName, LastName = @LastName, Age = @Age, Email = @Email,Phone=@Phone,[Address]=@Address
WHERE StudentID = @studentID
END
ELSE
BEGIN
INSERT INTO tbl_Student
VALUES (@FirstName,@LastName,@Age,@Email,@Phone,@Address);
END

END

GO
/****** Object:  StoredProcedure [dbo].[USP_DeleteStudent]    Script Date: 09-09-2021 09:52:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[USP_DeleteStudent] @studentID int
AS
BEGIN

delete FROM tbl_Student WHERE StudentID = @studentID

END

GO
/****** Object:  StoredProcedure [dbo].[USP_Get_Students]    Script Date: 09-09-2021 09:52:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_Get_Students] @studentID int
AS
BEGIN
if  (@studentID!=0)
BEGIN
SELECT * FROM tbl_Student WHERE StudentID = @studentID
END
ELSE
BEGIN
SELECT * FROM tbl_Student
END
END

GO
USE [master]
GO
ALTER DATABASE [Task_Api] SET  READ_WRITE 
GO
