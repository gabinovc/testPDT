### Test para Iron Mountain

### Gabriel Valenzuela Castillo

Ejecutar el siguiente script para crear la tabla en sqlserver

~~~
USE [TEST_DB]
GO
/****** Object:  Table [dbo].[Contactos]    Script Date: 03/05/2021 03:48:17 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Contactos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FechaRegistro] [smalldatetime] NULL,
	[Nombre] [varchar](150) NOT NULL,
	[Direccion] [varchar](200) NULL,
	[Telefono] [varchar](15) NOT NULL,
	[Curp] [varchar](25) NOT NULL,
 CONSTRAINT [PK_Contactos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
~~~

El proyecto está hecho con VStudio2019, modificar en el archivo appsettings.json la propiedad ConnectionStrings a los 
parámetros con las credenciales del servidor sqlserver que se valla a utilizar ejemplo:

~~~
 "ConnectionStrings": {
    "TestPDBContext": "Data Source=DESKTOP-5GHF8UJ\\MSSQLSERVER2012;Initial Catalog=Test_DB;User ID=sa;Password=gabriel;MultipleActiveResultSets=true"
  }
}
~~~