
CREATE DATABASE Negocio


USE [Negocio]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 31/5/2023 15:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[idProducto] [int] IDENTITY(1,1) NOT NULL,
	[idTipo] [int] NULL,
	[detalle] [varchar](150) NOT NULL,
	[stock] [int] NULL,
	[stockNuevo] [int] NULL,
	[precio] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoProducto]    Script Date: 31/5/2023 15:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoProducto](
	[idTipo] [int] IDENTITY(1,1) NOT NULL,
	[Detalle] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idTipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta]    Script Date: 31/5/2023 15:42:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta](
	[idVenta] [int] IDENTITY(1,1) NOT NULL,
	[idProducto] [int] NULL,
	[unidades] [int] NULL,
	[precioUnidad] [int] NULL,
	[total] [int] NULL,
	[fechaVenta] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[idVenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Productos] ON 
GO
INSERT [dbo].[Productos] ([idProducto], [idTipo], [detalle], [stock], [stockNuevo], [precio]) VALUES (1, 2, N'Buzo 1', 2, 10, 5000)
GO
INSERT [dbo].[Productos] ([idProducto], [idTipo], [detalle], [stock], [stockNuevo], [precio]) VALUES (2, 1, N'Campera 1', 90, 90, 1500)
GO
INSERT [dbo].[Productos] ([idProducto], [idTipo], [detalle], [stock], [stockNuevo], [precio]) VALUES (3, 1, N'Campera 2', 200, 200, 1000)
GO
SET IDENTITY_INSERT [dbo].[Productos] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoProducto] ON 
GO
INSERT [dbo].[TipoProducto] ([idTipo], [Detalle]) VALUES (1, N'Camperas')
GO
INSERT [dbo].[TipoProducto] ([idTipo], [Detalle]) VALUES (2, N'Buzos')
GO
INSERT [dbo].[TipoProducto] ([idTipo], [Detalle]) VALUES (3, N'Remeras')
GO
INSERT [dbo].[TipoProducto] ([idTipo], [Detalle]) VALUES (4, N'Pantalones')
GO
INSERT [dbo].[TipoProducto] ([idTipo], [Detalle]) VALUES (5, N'Gorros')
GO
INSERT [dbo].[TipoProducto] ([idTipo], [Detalle]) VALUES (6, N'Guantes')
GO
SET IDENTITY_INSERT [dbo].[TipoProducto] OFF
GO
SET IDENTITY_INSERT [dbo].[Venta] ON 
GO
INSERT [dbo].[Venta] ([idVenta], [idProducto], [unidades], [precioUnidad], [total], [fechaVenta]) VALUES (2, 1, 10, 5000, 50000, CAST(N'2023-05-31' AS Date))
GO
INSERT [dbo].[Venta] ([idVenta], [idProducto], [unidades], [precioUnidad], [total], [fechaVenta]) VALUES (4, 2, 10, 1500, 15000, CAST(N'2023-05-31' AS Date))
GO
SET IDENTITY_INSERT [dbo].[Venta] OFF
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD  CONSTRAINT [FK_Tipo] FOREIGN KEY([idTipo])
REFERENCES [dbo].[TipoProducto] ([idTipo])
GO
ALTER TABLE [dbo].[Productos] CHECK CONSTRAINT [FK_Tipo]
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD  CONSTRAINT [FK_Productos] FOREIGN KEY([idProducto])
REFERENCES [dbo].[Productos] ([idProducto])
GO
ALTER TABLE [dbo].[Venta] CHECK CONSTRAINT [FK_Productos]
GO
