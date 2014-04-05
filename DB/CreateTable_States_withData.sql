USE [ProgressTen]
GO

/****** Object:  Table [dbo].[State]    Script Date: 11/02/2011 23:34:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[State](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NOT NULL,
	[Code] [varchar](2) NOT NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


INSERT INTO State (Name,Code) VALUES ('Alaska', 'AK')
INSERT INTO State (Name,Code) VALUES ('Arizona', 'AZ')
INSERT INTO State (Name,Code) VALUES ('Arkansas', 'AR')
INSERT INTO State (Name,Code) VALUES ('California', 'CA')
INSERT INTO State (Name,Code) VALUES ('Colorado', 'CO')
INSERT INTO State (Name,Code) VALUES ('Connecticut', 'CT')
INSERT INTO State (Name,Code) VALUES ('Delaware', 'DE')
INSERT INTO State (Name,Code) VALUES ('District of Columbia', 'DC')
INSERT INTO State (Name,Code) VALUES ('Florida', 'FL')
INSERT INTO State (Name,Code) VALUES ('Georgia', 'GA')
INSERT INTO State (Name,Code) VALUES ('Hawaii', 'HI')
INSERT INTO State (Name,Code) VALUES ('Idaho', 'ID')
INSERT INTO State (Name,Code) VALUES ('Illinois', 'IL')
INSERT INTO State (Name,Code) VALUES ('Indiana', 'IN')
INSERT INTO State (Name,Code) VALUES ('Iowa', 'IA')
INSERT INTO State (Name,Code) VALUES ('Kansas', 'KS')
INSERT INTO State (Name,Code) VALUES ('Kentucky', 'KY')
INSERT INTO State (Name,Code) VALUES ('Louisiana', 'LA')
INSERT INTO State (Name,Code) VALUES ('Maine', 'ME')
INSERT INTO State (Name,Code) VALUES ('Maryland', 'MD')
INSERT INTO State (Name,Code) VALUES ('Massachusetts', 'MA')
INSERT INTO State (Name,Code) VALUES ('Michigan', 'MI')
INSERT INTO State (Name,Code) VALUES ('Minnesota', 'MN')
INSERT INTO State (Name,Code) VALUES ('Mississippi', 'MS')
INSERT INTO State (Name,Code) VALUES ('Missouri', 'MO')
INSERT INTO State (Name,Code) VALUES ('Montana', 'MT')
INSERT INTO State (Name,Code) VALUES ('Nebraska', 'NE')
INSERT INTO State (Name,Code) VALUES ('Nevada', 'NV')
INSERT INTO State (Name,Code) VALUES ('New Hampshire', 'NH')
INSERT INTO State (Name,Code) VALUES ('New Jersey', 'NJ')
INSERT INTO State (Name,Code) VALUES ('New Mexico', 'NM')
INSERT INTO State (Name,Code) VALUES ('New York', 'NY')
INSERT INTO State (Name,Code) VALUES ('North Carolina', 'NC')
INSERT INTO State (Name,Code) VALUES ('North Dakota', 'ND')
INSERT INTO State (Name,Code) VALUES ('Ohio', 'OH')
INSERT INTO State (Name,Code) VALUES ('Oklahoma', 'OK')
INSERT INTO State (Name,Code) VALUES ('Oregon', 'OR')
INSERT INTO State (Name,Code) VALUES ('Pennsylvania', 'PA')
INSERT INTO State (Name,Code) VALUES ('Rhode Island', 'RI')
INSERT INTO State (Name,Code) VALUES ('South Carolina', 'SC')
INSERT INTO State (Name,Code) VALUES ('South Dakota', 'SD')
INSERT INTO State (Name,Code) VALUES ('Tennessee', 'TN')
INSERT INTO State (Name,Code) VALUES ('Texas', 'TX')
INSERT INTO State (Name,Code) VALUES ('Utah', 'UT')
INSERT INTO State (Name,Code) VALUES ('Vermont', 'VT')
INSERT INTO State (Name,Code) VALUES ('Virginia', 'VA')
INSERT INTO State (Name,Code) VALUES ('Washington', 'WA')
INSERT INTO State (Name,Code) VALUES ('West Virginia', 'WV')
INSERT INTO State (Name,Code) VALUES ('Wisconsin', 'WI')
INSERT INTO State (Name,Code) VALUES ('Wyoming', 'WY')
GO