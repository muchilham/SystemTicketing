USE [master]
GO
/****** Object:  Database [TicketingDB]    Script Date: 2/14/2016 9:02:55 PM ******/
CREATE DATABASE [TicketingDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TicketingDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\TicketingDB.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TicketingDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\TicketingDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TicketingDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TicketingDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TicketingDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TicketingDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TicketingDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TicketingDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TicketingDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TicketingDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TicketingDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TicketingDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TicketingDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TicketingDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TicketingDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TicketingDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TicketingDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TicketingDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TicketingDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TicketingDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TicketingDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TicketingDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TicketingDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TicketingDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TicketingDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TicketingDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TicketingDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TicketingDB] SET  MULTI_USER 
GO
ALTER DATABASE [TicketingDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TicketingDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TicketingDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TicketingDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [TicketingDB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [TicketingDB]
GO
/****** Object:  Table [dbo].[tbl_Admin]    Script Date: 2/14/2016 9:02:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Admin](
	[username] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[email] [varchar](255) NOT NULL,
	[phonenumber] [varchar](15) NOT NULL,
 CONSTRAINT [PK_tbl_Admin] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Booking]    Script Date: 2/14/2016 9:02:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Booking](
	[id_booking] [int] IDENTITY(1,1) NOT NULL,
	[id_customer] [int] NOT NULL,
	[id_schedule] [int] NOT NULL,
	[booking_date] [datetime] NOT NULL,
	[status] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tbl_Booking] PRIMARY KEY CLUSTERED 
(
	[id_booking] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Customer]    Script Date: 2/14/2016 9:02:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Customer](
	[id_customer] [int] NOT NULL,
	[firstname] [varchar](100) NULL,
	[lastname] [varchar](100) NULL,
	[address] [text] NULL,
	[town] [text] NULL,
	[country] [text] NULL,
	[postcode] [text] NULL,
	[email] [varchar](255) NULL,
 CONSTRAINT [PK_tbl_Customer] PRIMARY KEY CLUSTERED 
(
	[id_customer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_HistoryLogin]    Script Date: 2/14/2016 9:02:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_HistoryLogin](
	[id_history] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[login_date] [datetime] NOT NULL,
 CONSTRAINT [PK_tbl_HistoryLogin] PRIMARY KEY CLUSTERED 
(
	[id_history] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Payment]    Script Date: 2/14/2016 9:02:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Payment](
	[id_payment] [int] IDENTITY(1,1) NOT NULL,
	[id_booking] [int] NOT NULL,
	[total_payment] [float] NOT NULL,
	[paid] [float] NOT NULL,
 CONSTRAINT [PK_tbl_Payment] PRIMARY KEY CLUSTERED 
(
	[id_payment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Route]    Script Date: 2/14/2016 9:02:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Route](
	[id_route] [int] IDENTITY(1,1) NOT NULL,
	[id_departure] [int] NOT NULL,
	[id_arrival] [int] NOT NULL,
 CONSTRAINT [PK_tbl_Route] PRIMARY KEY CLUSTERED 
(
	[id_route] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Schedule]    Script Date: 2/14/2016 9:02:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Schedule](
	[id_schedule] [int] IDENTITY(1,1) NOT NULL,
	[id_train] [int] NOT NULL,
	[id_route] [int] NOT NULL,
	[id_ticket] [int] NOT NULL,
	[harga] [float] NOT NULL,
	[departure_date] [datetime] NOT NULL,
 CONSTRAINT [PK_tbl_Schedule] PRIMARY KEY CLUSTERED 
(
	[id_schedule] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Station]    Script Date: 2/14/2016 9:02:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Station](
	[id_station] [int] NOT NULL,
	[station_name] [varchar](100) NOT NULL,
	[station_location] [varchar](100) NOT NULL,
 CONSTRAINT [PK_tbl_Station] PRIMARY KEY CLUSTERED 
(
	[id_station] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Ticket]    Script Date: 2/14/2016 9:02:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Ticket](
	[id_ticket] [int] NOT NULL,
	[class_ticket] [varchar](255) NOT NULL,
 CONSTRAINT [PK_tbl_Ticket_1] PRIMARY KEY CLUSTERED 
(
	[id_ticket] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_TicketType]    Script Date: 2/14/2016 9:02:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_TicketType](
	[id_type] [int] IDENTITY(1,1) NOT NULL,
	[id_ticket] [int] NOT NULL,
	[harga_ticket] [float] NOT NULL,
 CONSTRAINT [PK_tbl_TicketType] PRIMARY KEY CLUSTERED 
(
	[id_type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Train]    Script Date: 2/14/2016 9:02:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Train](
	[id_train] [int] NOT NULL,
	[train_name] [varchar](100) NOT NULL,
	[train_seat] [int] NOT NULL,
 CONSTRAINT [PK_tbl_Train] PRIMARY KEY CLUSTERED 
(
	[id_train] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[tbl_Admin] ([username], [password], [email], [phonenumber]) VALUES (N'admin', N'admin', N'muh.ilham0606@gmail.com', N'081291665400')
SET IDENTITY_INSERT [dbo].[tbl_Booking] ON 

INSERT [dbo].[tbl_Booking] ([id_booking], [id_customer], [id_schedule], [booking_date], [status]) VALUES (1, 6127, 2, CAST(N'2016-02-14 20:59:18.703' AS DateTime), N'PAID')
SET IDENTITY_INSERT [dbo].[tbl_Booking] OFF
INSERT [dbo].[tbl_Customer] ([id_customer], [firstname], [lastname], [address], [town], [country], [postcode], [email]) VALUES (6127, N'Muchammad', N'Ilham', N'Jalan Penggilingan Baru', N'Jakarta', N'Jakarta Selatan', N'13550', N'muh.ilham0606@gmail.com')
SET IDENTITY_INSERT [dbo].[tbl_Payment] ON 

INSERT [dbo].[tbl_Payment] ([id_payment], [id_booking], [total_payment], [paid]) VALUES (1, 1, 80000, 100000)
SET IDENTITY_INSERT [dbo].[tbl_Payment] OFF
SET IDENTITY_INSERT [dbo].[tbl_Route] ON 

INSERT [dbo].[tbl_Route] ([id_route], [id_departure], [id_arrival]) VALUES (1, 1804, 1982)
INSERT [dbo].[tbl_Route] ([id_route], [id_departure], [id_arrival]) VALUES (2, 1804, 51237)
INSERT [dbo].[tbl_Route] ([id_route], [id_departure], [id_arrival]) VALUES (3, 1982, 51237)
SET IDENTITY_INSERT [dbo].[tbl_Route] OFF
SET IDENTITY_INSERT [dbo].[tbl_Schedule] ON 

INSERT [dbo].[tbl_Schedule] ([id_schedule], [id_train], [id_route], [id_ticket], [harga], [departure_date]) VALUES (2, 15502, 2, 1, 80000, CAST(N'2016-02-15 20:57:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[tbl_Schedule] OFF
INSERT [dbo].[tbl_Station] ([id_station], [station_name], [station_location]) VALUES (1804, N'Stasiun Kota', N'Jakarta')
INSERT [dbo].[tbl_Station] ([id_station], [station_name], [station_location]) VALUES (1982, N'Gede Bage', N'Bandung')
INSERT [dbo].[tbl_Station] ([id_station], [station_name], [station_location]) VALUES (51237, N'Lempuyangan', N'Kebumen')
INSERT [dbo].[tbl_Ticket] ([id_ticket], [class_ticket]) VALUES (1, N'Ekonomi')
INSERT [dbo].[tbl_Ticket] ([id_ticket], [class_ticket]) VALUES (2, N'Express')
SET IDENTITY_INSERT [dbo].[tbl_TicketType] ON 

INSERT [dbo].[tbl_TicketType] ([id_type], [id_ticket], [harga_ticket]) VALUES (1, 1, 80000)
INSERT [dbo].[tbl_TicketType] ([id_type], [id_ticket], [harga_ticket]) VALUES (2, 1, 100000)
INSERT [dbo].[tbl_TicketType] ([id_type], [id_ticket], [harga_ticket]) VALUES (3, 1, 120000)
INSERT [dbo].[tbl_TicketType] ([id_type], [id_ticket], [harga_ticket]) VALUES (4, 2, 200000)
INSERT [dbo].[tbl_TicketType] ([id_type], [id_ticket], [harga_ticket]) VALUES (5, 2, 240000)
INSERT [dbo].[tbl_TicketType] ([id_type], [id_ticket], [harga_ticket]) VALUES (6, 2, 300000)
SET IDENTITY_INSERT [dbo].[tbl_TicketType] OFF
INSERT [dbo].[tbl_Train] ([id_train], [train_name], [train_seat]) VALUES (15502, N'GAJAH MADA', 1000)
ALTER TABLE [dbo].[tbl_Booking]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Booking_tbl_Customer] FOREIGN KEY([id_customer])
REFERENCES [dbo].[tbl_Customer] ([id_customer])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_Booking] CHECK CONSTRAINT [FK_tbl_Booking_tbl_Customer]
GO
ALTER TABLE [dbo].[tbl_Booking]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Booking_tbl_Schedule] FOREIGN KEY([id_schedule])
REFERENCES [dbo].[tbl_Schedule] ([id_schedule])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_Booking] CHECK CONSTRAINT [FK_tbl_Booking_tbl_Schedule]
GO
ALTER TABLE [dbo].[tbl_HistoryLogin]  WITH CHECK ADD  CONSTRAINT [FK_tbl_HistoryLogin_tbl_Admin] FOREIGN KEY([username])
REFERENCES [dbo].[tbl_Admin] ([username])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[tbl_HistoryLogin] CHECK CONSTRAINT [FK_tbl_HistoryLogin_tbl_Admin]
GO
ALTER TABLE [dbo].[tbl_Payment]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Payment_tbl_Booking] FOREIGN KEY([id_booking])
REFERENCES [dbo].[tbl_Booking] ([id_booking])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_Payment] CHECK CONSTRAINT [FK_tbl_Payment_tbl_Booking]
GO
ALTER TABLE [dbo].[tbl_Route]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Route_tbl_Station] FOREIGN KEY([id_departure])
REFERENCES [dbo].[tbl_Station] ([id_station])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_Route] CHECK CONSTRAINT [FK_tbl_Route_tbl_Station]
GO
ALTER TABLE [dbo].[tbl_Route]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Route_tbl_Station1] FOREIGN KEY([id_arrival])
REFERENCES [dbo].[tbl_Station] ([id_station])
GO
ALTER TABLE [dbo].[tbl_Route] CHECK CONSTRAINT [FK_tbl_Route_tbl_Station1]
GO
ALTER TABLE [dbo].[tbl_Schedule]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Schedule_tbl_Route] FOREIGN KEY([id_route])
REFERENCES [dbo].[tbl_Route] ([id_route])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_Schedule] CHECK CONSTRAINT [FK_tbl_Schedule_tbl_Route]
GO
ALTER TABLE [dbo].[tbl_Schedule]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Schedule_tbl_Ticket] FOREIGN KEY([id_ticket])
REFERENCES [dbo].[tbl_Ticket] ([id_ticket])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_Schedule] CHECK CONSTRAINT [FK_tbl_Schedule_tbl_Ticket]
GO
ALTER TABLE [dbo].[tbl_Schedule]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Schedule_tbl_Train] FOREIGN KEY([id_train])
REFERENCES [dbo].[tbl_Train] ([id_train])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_Schedule] CHECK CONSTRAINT [FK_tbl_Schedule_tbl_Train]
GO
ALTER TABLE [dbo].[tbl_TicketType]  WITH CHECK ADD  CONSTRAINT [FK_tbl_TicketType_tbl_Ticket] FOREIGN KEY([id_ticket])
REFERENCES [dbo].[tbl_Ticket] ([id_ticket])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbl_TicketType] CHECK CONSTRAINT [FK_tbl_TicketType_tbl_Ticket]
GO
USE [master]
GO
ALTER DATABASE [TicketingDB] SET  READ_WRITE 
GO
