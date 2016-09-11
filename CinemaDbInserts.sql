USE [master]

IF DB_ID('CinemaDb') IS NOT NULL DROP DATABASE [CinemaDb];
IF @@ERROR = 3702 
   RAISERROR('Database cannot be dropped because there are still open connections.', 127, 127) WITH NOWAIT, LOG;

CREATE DATABASE [CinemaDb]
 CONTAINMENT = NONE

ALTER DATABASE [CinemaDb] MODIFY FILE
( NAME = N'CinemaDb' , SIZE = 512-MB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024-KB )
GO
ALTER DATABASE [CinemaDb] MODIFY FILE
( NAME = N'CinemaDb_log' , SIZE = 256-MB , MAXSIZE = UNLIMITED , FILEGROWTH = 10%)
GO

ALTER DATABASE [CinemaDb] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CinemaDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CinemaDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CinemaDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CinemaDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CinemaDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CinemaDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [CinemaDb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [CinemaDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CinemaDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CinemaDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CinemaDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CinemaDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CinemaDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CinemaDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CinemaDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CinemaDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CinemaDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CinemaDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CinemaDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CinemaDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CinemaDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CinemaDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [CinemaDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CinemaDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CinemaDb] SET  MULTI_USER 
GO
ALTER DATABASE [CinemaDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CinemaDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CinemaDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CinemaDb] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [CinemaDb] SET DELAYED_DURABILITY = DISABLED 
GO
USE [CinemaDb]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 11.09.2016 18:15:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Movies]    Script Date: 11.09.2016 18:15:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Movies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Reservations]    Script Date: 11.09.2016 18:15:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserEmail] [nvarchar](max) NULL,
	[SeanceId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Reservations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ReservationSpotMapping]    Script Date: 11.09.2016 18:15:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservationSpotMapping](
	[ReservationId] [int] NOT NULL,
	[SpotId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ReservationSpotMapping] PRIMARY KEY CLUSTERED 
(
	[ReservationId] ASC,
	[SpotId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 11.09.2016 18:15:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Rooms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Seances]    Script Date: 11.09.2016 18:15:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seances](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[MovieId] [int] NOT NULL,
	[RoomId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Seances] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Spots]    Script Date: 11.09.2016 18:15:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spots](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](max) NULL,
	[RoomId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Spots] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 11.09.2016 18:15:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserEmail] [nvarchar](max) NULL,
	[TransactionDate] [datetime] NULL,
	[Price] [float] NOT NULL,
	[ReservationId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Transactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Index [IX_SeanceId]    Script Date: 11.09.2016 18:15:14 ******/
CREATE NONCLUSTERED INDEX [IX_SeanceId] ON [dbo].[Reservations]
(
	[SeanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ReservationId]    Script Date: 11.09.2016 18:15:14 ******/
CREATE NONCLUSTERED INDEX [IX_ReservationId] ON [dbo].[ReservationSpotMapping]
(
	[ReservationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SpotId]    Script Date: 11.09.2016 18:15:14 ******/
CREATE NONCLUSTERED INDEX [IX_SpotId] ON [dbo].[ReservationSpotMapping]
(
	[SpotId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MovieId]    Script Date: 11.09.2016 18:15:14 ******/
CREATE NONCLUSTERED INDEX [IX_MovieId] ON [dbo].[Seances]
(
	[MovieId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoomId]    Script Date: 11.09.2016 18:15:14 ******/
CREATE NONCLUSTERED INDEX [IX_RoomId] ON [dbo].[Seances]
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoomId]    Script Date: 11.09.2016 18:15:14 ******/
CREATE NONCLUSTERED INDEX [IX_RoomId] ON [dbo].[Spots]
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ReservationId]    Script Date: 11.09.2016 18:15:14 ******/
CREATE NONCLUSTERED INDEX [IX_ReservationId] ON [dbo].[Transactions]
(
	[ReservationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Reservations]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Reservations_dbo.Seances_SeanceId] FOREIGN KEY([SeanceId])
REFERENCES [dbo].[Seances] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reservations] CHECK CONSTRAINT [FK_dbo.Reservations_dbo.Seances_SeanceId]
GO
ALTER TABLE [dbo].[ReservationSpotMapping]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ReservationSpotMapping_dbo.Reservations_ReservationId] FOREIGN KEY([ReservationId])
REFERENCES [dbo].[Reservations] ([Id])
GO
ALTER TABLE [dbo].[ReservationSpotMapping] CHECK CONSTRAINT [FK_dbo.ReservationSpotMapping_dbo.Reservations_ReservationId]
GO
ALTER TABLE [dbo].[ReservationSpotMapping]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ReservationSpotMapping_dbo.Spots_SpotId] FOREIGN KEY([SpotId])
REFERENCES [dbo].[Spots] ([Id])
GO
ALTER TABLE [dbo].[ReservationSpotMapping] CHECK CONSTRAINT [FK_dbo.ReservationSpotMapping_dbo.Spots_SpotId]
GO
ALTER TABLE [dbo].[Seances]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Seances_dbo.Movies_MovieId] FOREIGN KEY([MovieId])
REFERENCES [dbo].[Movies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Seances] CHECK CONSTRAINT [FK_dbo.Seances_dbo.Movies_MovieId]
GO
ALTER TABLE [dbo].[Seances]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Seances_dbo.Rooms_RoomId] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Seances] CHECK CONSTRAINT [FK_dbo.Seances_dbo.Rooms_RoomId]
GO
ALTER TABLE [dbo].[Spots]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Spots_dbo.Rooms_RoomId] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Spots] CHECK CONSTRAINT [FK_dbo.Spots_dbo.Rooms_RoomId]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Transactions_dbo.Reservations_ReservationId] FOREIGN KEY([ReservationId])
REFERENCES [dbo].[Reservations] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_dbo.Transactions_dbo.Reservations_ReservationId]
GO
USE [master]
GO
ALTER DATABASE [CinemaDb] SET  READ_WRITE 
GO


USE [CinemaDb]
GO

INSERT INTO [dbo].[Movies]([Title],[Description]) VALUES ('Kamienne piêœci','Dwukrotny zdobywca Oscara Robert De Niro („Wœciek³y byk”, „Last Vegas”, „Poradnik pozytywnego myœlenia”) w opowieœci o jednym z najs³ynniejszych bokserów wszech czasów. Roberto Duran wychowuje siê na ulicy, gdzie rz¹dzi prawo piêœci. Umiejêtnoœci, które zdobywa walcz¹c o przetrwanie, staj¹ siê jego przepustk¹ do lepszego œwiata. Na profesjonalnym ringu pojawia siê po raz pierwszy w wieku 16 lat i nied³ugo po tym zyskuje przydomek Manos de Piedra, czyli Kamienne piêœci.')
INSERT INTO [dbo].[Movies]([Title],[Description]) VALUES ('Smoleñsk','Antoni Krauze, twórca wielokrotnie nagrodzonego dramatu historycznego „Czarny Czwartek. Janek Wiœniewski pad³”, ods³ania kulisy tragedii, która wstrz¹snê³a ca³¹ Polsk¹. Poruszaj¹c¹ muzykê do filmu skomponowa³ Micha³ Lorenc, autor oprawy dŸwiêkowej takich produkcji, jak: „Poznañ 56”, „Historia Roja”, „Przedwioœnie” i „Czarny Czwartek. Janek Wiœniewski pad³”.')
INSERT INTO [dbo].[Movies]([Title],[Description]) VALUES ('Siedmiu wspania³ych','Armia bandytów  terroryzuje ma³¹ wioskê na pograniczu amerykañsko-meksykañskim. Zrozpaczeni farmerzy zwracaj¹ siê z proœb¹ o pomoc do grupy rewolwerowców. Siedmiu najemników postanawia stawiæ czo³o przewa¿aj¹cym si³om przeciwnika, choæ wydaj¹ siê nie mieæ szans. Czy najtwardsi rewolwerowcy bêd¹ w stanie uwolniæ wioskê spod bandyckich rz¹dów? Czy zapewni¹ mieszkañcom spokój i bezpieczeñstwo?')
GO
INSERT INTO [dbo].[Rooms]([Number]) VALUES('Sala 1')
INSERT INTO [dbo].[Rooms]([Number]) VALUES('Sala 2')
INSERT INTO [dbo].[Rooms]([Number]) VALUES('Sala 3')
INSERT INTO [dbo].[Rooms]([Number]) VALUES('Sala 4')

GO
INSERT INTO [dbo].[Seances]([Date],[MovieId],[RoomId]) VALUES ('2016-09-25-T18:00:00',1,1)
INSERT INTO [dbo].[Seances]([Date],[MovieId],[RoomId]) VALUES ('2016-09-25-T18:00:00',2,2)
INSERT INTO [dbo].[Seances]([Date],[MovieId],[RoomId]) VALUES ('2016-09-25-T18:00:00',3,3)
INSERT INTO [dbo].[Seances]([Date],[MovieId],[RoomId]) VALUES ('2016-09-26-T20:00:00',1,4)
INSERT INTO [dbo].[Seances]([Date],[MovieId],[RoomId]) VALUES ('2016-09-26-T20:00:00',2,1)
INSERT INTO [dbo].[Seances]([Date],[MovieId],[RoomId]) VALUES ('2016-09-26-T20:00:00',3,2)
GO

INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('1-A' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('2-A' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('3-A' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('4-A' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('5-A' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('6-A' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('7-A' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('8-A' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('9-A' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('1-B' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('2-B' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('3-B' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('4-B' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('5-B' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('6-B' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('7-B' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('8-B' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('9-B' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('1-C' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('2-C' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('3-C' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('4-C' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('5-C' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('6-C' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('7-C' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('8-C' ,1)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('9-C' ,1)


INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('1-A' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('2-A' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('3-A' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('4-A' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('5-A' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('6-A' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('7-A' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('8-A' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('9-A' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('1-B' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('2-B' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('3-B' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('4-B' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('5-B' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('6-B' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('7-B' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('8-B' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('9-B' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('1-C' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('2-C' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('3-C' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('4-C' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('5-C' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('6-C' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('7-C' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('8-C' ,2)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('9-C' ,2)


INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('1-A' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('2-A' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('3-A' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('4-A' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('5-A' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('6-A' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('7-A' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('8-A' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('9-A' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('1-B' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('2-B' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('3-B' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('4-B' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('5-B' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('6-B' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('7-B' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('8-B' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('9-B' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('1-C' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('2-C' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('3-C' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('4-C' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('5-C' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('6-C' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('7-C' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('8-C' ,3)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('9-C' ,3)


INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('1-A' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('2-A' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('3-A' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('4-A' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('5-A' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('6-A' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('7-A' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('8-A' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('9-A' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('1-B' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('2-B' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('3-B' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('4-B' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('5-B' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('6-B' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('7-B' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('8-B' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('9-B' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('1-C' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('2-C' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('3-C' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('4-C' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('5-C' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('6-C' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('7-C' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('8-C' ,4)
INSERT INTO [dbo].[Spots]([Number],[RoomId])VALUES ('9-C' ,4)
GO


/*
INSERT INTO [dbo].[Reservations]([UserEmail],[SeanceId]) VALUES  (<UserEmail, nvarchar(max),>,<SeanceId, int,>)
GO
INSERT INTO [dbo].[ReservationSpotMapping]([ReservationId],[SpotId]) VALUES (<ReservationId, int,>,<SpotId, int,>)
GO
*/





