CREATE TABLE [dbo].Peliculas
(
	[PeliculaId] INT Identity NOT NULL PRIMARY KEY,
	[Titulo] VARCHAR (50) NOT NULL,
	[Sinopsis] VARCHAR (MAX) NOT NULL,
	[Director] VARCHAR (30) NOT NULL,
	[Genero] VARCHAR (10) NOT NULL,
	[Calificacion] INT NOT NULL,
	[Poster] Image NOT NULL
)
