USE [ProgressTen]
GO

/****** Object:  Table [dbo].[Country]    Script Date: 11/03/2011 00:36:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Country](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Code] [varchar](2) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

truncate table Country

INSERT INTO Country (Code, Name) VALUES ( 'US', 'United States');
INSERT INTO Country (Code, Name) VALUES ( 'CA', 'Canada');
INSERT INTO Country (Code, Name) VALUES ( 'AF', 'Afghanistan');
INSERT INTO Country (Code, Name) VALUES ( 'AL', 'Albania');
INSERT INTO Country (Code, Name) VALUES ( 'DZ', 'Algeria');
INSERT INTO Country (Code, Name) VALUES ( 'DS', 'American Samoa');
INSERT INTO Country (Code, Name) VALUES ( 'AD', 'Andorra');
INSERT INTO Country (Code, Name) VALUES ( 'AO', 'Angola');
INSERT INTO Country (Code, Name) VALUES ( 'AI', 'Anguilla');
INSERT INTO Country (Code, Name) VALUES ( 'AQ', 'Antarctica');
INSERT INTO Country (Code, Name) VALUES ( 'AG', 'Antigua and/or Barbuda');
INSERT INTO Country (Code, Name) VALUES ( 'AR', 'Argentina');
INSERT INTO Country (Code, Name) VALUES ( 'AM', 'Armenia');
INSERT INTO Country (Code, Name) VALUES ( 'AW', 'Aruba');
INSERT INTO Country (Code, Name) VALUES ( 'AU', 'Australia');
INSERT INTO Country (Code, Name) VALUES ( 'AT', 'Austria');
INSERT INTO Country (Code, Name) VALUES ( 'AZ', 'Azerbaijan');
INSERT INTO Country (Code, Name) VALUES ( 'BS', 'Bahamas');
INSERT INTO Country (Code, Name) VALUES ( 'BH', 'Bahrain');
INSERT INTO Country (Code, Name) VALUES ( 'BD', 'Bangladesh');
INSERT INTO Country (Code, Name) VALUES ( 'BB', 'Barbados');
INSERT INTO Country (Code, Name) VALUES ( 'BY', 'Belarus');
INSERT INTO Country (Code, Name) VALUES ( 'BE', 'Belgium');
INSERT INTO Country (Code, Name) VALUES ( 'BZ', 'Belize');
INSERT INTO Country (Code, Name) VALUES ( 'BJ', 'Benin');
INSERT INTO Country (Code, Name) VALUES ( 'BM', 'Bermuda');
INSERT INTO Country (Code, Name) VALUES ( 'BT', 'Bhutan');
INSERT INTO Country (Code, Name) VALUES ( 'BO', 'Bolivia');
INSERT INTO Country (Code, Name) VALUES ( 'BA', 'Bosnia and Herzegovina');
INSERT INTO Country (Code, Name) VALUES ( 'BW', 'Botswana');
INSERT INTO Country (Code, Name) VALUES ( 'BV', 'Bouvet Island');
INSERT INTO Country (Code, Name) VALUES ( 'BR', 'Brazil');
INSERT INTO Country (Code, Name) VALUES ( 'IO', 'British lndian Ocean Territory');
INSERT INTO Country (Code, Name) VALUES ( 'BN', 'Brunei Darussalam');
INSERT INTO Country (Code, Name) VALUES ( 'BG', 'Bulgaria');
INSERT INTO Country (Code, Name) VALUES ( 'BF', 'Burkina Faso');
INSERT INTO Country (Code, Name) VALUES ( 'BI', 'Burundi');
INSERT INTO Country (Code, Name) VALUES ( 'KH', 'Cambodia');
INSERT INTO Country (Code, Name) VALUES ( 'CM', 'Cameroon');
INSERT INTO Country (Code, Name) VALUES ( 'CV', 'Cape Verde');
INSERT INTO Country (Code, Name) VALUES ( 'KY', 'Cayman Islands');
INSERT INTO Country (Code, Name) VALUES ( 'CF', 'Central African Republic');
INSERT INTO Country (Code, Name) VALUES ( 'TD', 'Chad');
INSERT INTO Country (Code, Name) VALUES ( 'CL', 'Chile');
INSERT INTO Country (Code, Name) VALUES ( 'CN', 'China');
INSERT INTO Country (Code, Name) VALUES ( 'CX', 'Christmas Island');
INSERT INTO Country (Code, Name) VALUES ( 'CC', 'Cocos (Keeling) Islands');
INSERT INTO Country (Code, Name) VALUES ( 'CO', 'Colombia');
INSERT INTO Country (Code, Name) VALUES ( 'KM', 'Comoros');
INSERT INTO Country (Code, Name) VALUES ( 'CG', 'Congo');
INSERT INTO Country (Code, Name) VALUES ( 'CK', 'Cook Islands');
INSERT INTO Country (Code, Name) VALUES ( 'CR', 'Costa Rica');
INSERT INTO Country (Code, Name) VALUES ( 'HR', 'Croatia (Hrvatska)');
INSERT INTO Country (Code, Name) VALUES ( 'CU', 'Cuba');
INSERT INTO Country (Code, Name) VALUES ( 'CY', 'Cyprus');
INSERT INTO Country (Code, Name) VALUES ( 'CZ', 'Czech Republic');
INSERT INTO Country (Code, Name) VALUES ( 'DK', 'Denmark');
INSERT INTO Country (Code, Name) VALUES ( 'DJ', 'Djibouti');
INSERT INTO Country (Code, Name) VALUES ( 'DM', 'Dominica');
INSERT INTO Country (Code, Name) VALUES ( 'DO', 'Dominican Republic');
INSERT INTO Country (Code, Name) VALUES ( 'TP', 'East Timor');
INSERT INTO Country (Code, Name) VALUES ( 'EC', 'Ecudaor');
INSERT INTO Country (Code, Name) VALUES ( 'EG', 'Egypt');
INSERT INTO Country (Code, Name) VALUES ( 'SV', 'El Salvador');
INSERT INTO Country (Code, Name) VALUES ( 'GQ', 'Equatorial Guinea');
INSERT INTO Country (Code, Name) VALUES ( 'ER', 'Eritrea');
INSERT INTO Country (Code, Name) VALUES ( 'EE', 'Estonia');
INSERT INTO Country (Code, Name) VALUES ( 'ET', 'Ethiopia');
INSERT INTO Country (Code, Name) VALUES ( 'FK', 'Falkland Islands (Malvinas)');
INSERT INTO Country (Code, Name) VALUES ( 'FO', 'Faroe Islands');
INSERT INTO Country (Code, Name) VALUES ( 'FJ', 'Fiji');
INSERT INTO Country (Code, Name) VALUES ( 'FI', 'Finland');
INSERT INTO Country (Code, Name) VALUES ( 'FR', 'France');
INSERT INTO Country (Code, Name) VALUES ( 'FX', 'France, Metropolitan');
INSERT INTO Country (Code, Name) VALUES ( 'GF', 'French Guiana');
INSERT INTO Country (Code, Name) VALUES ( 'PF', 'French Polynesia');
INSERT INTO Country (Code, Name) VALUES ( 'TF', 'French Southern Territories');
INSERT INTO Country (Code, Name) VALUES ( 'GA', 'Gabon');
INSERT INTO Country (Code, Name) VALUES ( 'GM', 'Gambia');
INSERT INTO Country (Code, Name) VALUES ( 'GE', 'Georgia');
INSERT INTO Country (Code, Name) VALUES ( 'DE', 'Germany');
INSERT INTO Country (Code, Name) VALUES ( 'GH', 'Ghana');
INSERT INTO Country (Code, Name) VALUES ( 'GI', 'Gibraltar');
INSERT INTO Country (Code, Name) VALUES ( 'GR', 'Greece');
INSERT INTO Country (Code, Name) VALUES ( 'GL', 'Greenland');
INSERT INTO Country (Code, Name) VALUES ( 'GD', 'Grenada');
INSERT INTO Country (Code, Name) VALUES ( 'GP', 'Guadeloupe');
INSERT INTO Country (Code, Name) VALUES ( 'GU', 'Guam');
INSERT INTO Country (Code, Name) VALUES ( 'GT', 'Guatemala');
INSERT INTO Country (Code, Name) VALUES ( 'GN', 'Guinea');
INSERT INTO Country (Code, Name) VALUES ( 'GW', 'Guinea-Bissau');
INSERT INTO Country (Code, Name) VALUES ( 'GY', 'Guyana');
INSERT INTO Country (Code, Name) VALUES ( 'HT', 'Haiti');
INSERT INTO Country (Code, Name) VALUES ( 'HM', 'Heard and Mc Donald Islands');
INSERT INTO Country (Code, Name) VALUES ( 'HN', 'Honduras');
INSERT INTO Country (Code, Name) VALUES ( 'HK', 'Hong Kong');
INSERT INTO Country (Code, Name) VALUES ( 'HU', 'Hungary');
INSERT INTO Country (Code, Name) VALUES ( 'IS', 'Iceland');
INSERT INTO Country (Code, Name) VALUES ( 'IN', 'India');
INSERT INTO Country (Code, Name) VALUES ( 'ID', 'Indonesia');
INSERT INTO Country (Code, Name) VALUES ( 'IR', 'Iran (Islamic Republic of)');
INSERT INTO Country (Code, Name) VALUES ( 'IQ', 'Iraq');
INSERT INTO Country (Code, Name) VALUES ( 'IE', 'Ireland');
INSERT INTO Country (Code, Name) VALUES ( 'IL', 'Israel');
INSERT INTO Country (Code, Name) VALUES ( 'IT', 'Italy');
INSERT INTO Country (Code, Name) VALUES ( 'CI', 'Ivory Coast');
INSERT INTO Country (Code, Name) VALUES ( 'JM', 'Jamaica');
INSERT INTO Country (Code, Name) VALUES ( 'JP', 'Japan');
INSERT INTO Country (Code, Name) VALUES ( 'JO', 'Jordan');
INSERT INTO Country (Code, Name) VALUES ( 'KZ', 'Kazakhstan');
INSERT INTO Country (Code, Name) VALUES ( 'KE', 'Kenya');
INSERT INTO Country (Code, Name) VALUES ( 'KI', 'Kiribati');
INSERT INTO Country (Code, Name) VALUES ( 'KP', 'Korea, Democratic People''s Republic of');
INSERT INTO Country (Code, Name) VALUES ( 'KR', 'Korea, Republic of');
INSERT INTO Country (Code, Name) VALUES ( 'KW', 'Kuwait');
INSERT INTO Country (Code, Name) VALUES ( 'KG', 'Kyrgyzstan');
INSERT INTO Country (Code, Name) VALUES ( 'LA', 'Lao People''s Democratic Republic');
INSERT INTO Country (Code, Name) VALUES ( 'LV', 'Latvia');
INSERT INTO Country (Code, Name) VALUES ( 'LB', 'Lebanon');
INSERT INTO Country (Code, Name) VALUES ( 'LS', 'Lesotho');
INSERT INTO Country (Code, Name) VALUES ( 'LR', 'Liberia');
INSERT INTO Country (Code, Name) VALUES ( 'LY', 'Libyan Arab Jamahiriya');
INSERT INTO Country (Code, Name) VALUES ( 'LI', 'Liechtenstein');
INSERT INTO Country (Code, Name) VALUES ( 'LT', 'Lithuania');
INSERT INTO Country (Code, Name) VALUES ( 'LU', 'Luxembourg');
INSERT INTO Country (Code, Name) VALUES ( 'MO', 'Macau');
INSERT INTO Country (Code, Name) VALUES ( 'MK', 'Macedonia');
INSERT INTO Country (Code, Name) VALUES ( 'MG', 'Madagascar');
INSERT INTO Country (Code, Name) VALUES ( 'MW', 'Malawi');
INSERT INTO Country (Code, Name) VALUES ( 'MY', 'Malaysia');
INSERT INTO Country (Code, Name) VALUES ( 'MV', 'Maldives');
INSERT INTO Country (Code, Name) VALUES ( 'ML', 'Mali');
INSERT INTO Country (Code, Name) VALUES ( 'MT', 'Malta');
INSERT INTO Country (Code, Name) VALUES ( 'MH', 'Marshall Islands');
INSERT INTO Country (Code, Name) VALUES ( 'MQ', 'Martinique');
INSERT INTO Country (Code, Name) VALUES ( 'MR', 'Mauritania');
INSERT INTO Country (Code, Name) VALUES ( 'MU', 'Mauritius');
INSERT INTO Country (Code, Name) VALUES ( 'TY', 'Mayotte');
INSERT INTO Country (Code, Name) VALUES ( 'MX', 'Mexico');
INSERT INTO Country (Code, Name) VALUES ( 'FM', 'Micronesia, Federated States of');
INSERT INTO Country (Code, Name) VALUES ( 'MD', 'Moldova, Republic of');
INSERT INTO Country (Code, Name) VALUES ( 'MC', 'Monaco');
INSERT INTO Country (Code, Name) VALUES ( 'MN', 'Mongolia');
INSERT INTO Country (Code, Name) VALUES ( 'MS', 'Montserrat');
INSERT INTO Country (Code, Name) VALUES ( 'MA', 'Morocco');
INSERT INTO Country (Code, Name) VALUES ( 'MZ', 'Mozambique');
INSERT INTO Country (Code, Name) VALUES ( 'MM', 'Myanmar');
INSERT INTO Country (Code, Name) VALUES ( 'NA', 'Namibia');
INSERT INTO Country (Code, Name) VALUES ( 'NR', 'Nauru');
INSERT INTO Country (Code, Name) VALUES ( 'NP', 'Nepal');
INSERT INTO Country (Code, Name) VALUES ( 'NL', 'Netherlands');
INSERT INTO Country (Code, Name) VALUES ( 'AN', 'Netherlands Antilles');
INSERT INTO Country (Code, Name) VALUES ( 'NC', 'New Caledonia');
INSERT INTO Country (Code, Name) VALUES ( 'NZ', 'New Zealand');
INSERT INTO Country (Code, Name) VALUES ( 'NI', 'Nicaragua');
INSERT INTO Country (Code, Name) VALUES ( 'NE', 'Niger');
INSERT INTO Country (Code, Name) VALUES ( 'NG', 'Nigeria');
INSERT INTO Country (Code, Name) VALUES ( 'NU', 'Niue');
INSERT INTO Country (Code, Name) VALUES ( 'NF', 'Norfork Island');
INSERT INTO Country (Code, Name) VALUES ( 'MP', 'Northern Mariana Islands');
INSERT INTO Country (Code, Name) VALUES ( 'NO', 'Norway');
INSERT INTO Country (Code, Name) VALUES ( 'OM', 'Oman');
INSERT INTO Country (Code, Name) VALUES ( 'PK', 'Pakistan');
INSERT INTO Country (Code, Name) VALUES ( 'PW', 'Palau');
INSERT INTO Country (Code, Name) VALUES ( 'PA', 'Panama');
INSERT INTO Country (Code, Name) VALUES ( 'PG', 'Papua New Guinea');
INSERT INTO Country (Code, Name) VALUES ( 'PY', 'Paraguay');
INSERT INTO Country (Code, Name) VALUES ( 'PE', 'Peru');
INSERT INTO Country (Code, Name) VALUES ( 'PH', 'Philippines');
INSERT INTO Country (Code, Name) VALUES ( 'PN', 'Pitcairn');
INSERT INTO Country (Code, Name) VALUES ( 'PL', 'Poland');
INSERT INTO Country (Code, Name) VALUES ( 'PT', 'Portugal');
INSERT INTO Country (Code, Name) VALUES ( 'PR', 'Puerto Rico');
INSERT INTO Country (Code, Name) VALUES ( 'QA', 'Qatar');
INSERT INTO Country (Code, Name) VALUES ( 'RE', 'Reunion');
INSERT INTO Country (Code, Name) VALUES ( 'RO', 'Romania');
INSERT INTO Country (Code, Name) VALUES ( 'RU', 'Russian Federation');
INSERT INTO Country (Code, Name) VALUES ( 'RW', 'Rwanda');
INSERT INTO Country (Code, Name) VALUES ( 'KN', 'Saint Kitts and Nevis');
INSERT INTO Country (Code, Name) VALUES ( 'LC', 'Saint Lucia');
INSERT INTO Country (Code, Name) VALUES ( 'VC', 'Saint Vincent and the Grenadines');
INSERT INTO Country (Code, Name) VALUES ( 'WS', 'Samoa');
INSERT INTO Country (Code, Name) VALUES ( 'SM', 'San Marino');
INSERT INTO Country (Code, Name) VALUES ( 'ST', 'Sao Tome and Principe');
INSERT INTO Country (Code, Name) VALUES ( 'SA', 'Saudi Arabia');
INSERT INTO Country (Code, Name) VALUES ( 'SN', 'Senegal');
INSERT INTO Country (Code, Name) VALUES ( 'SC', 'Seychelles');
INSERT INTO Country (Code, Name) VALUES ( 'SL', 'Sierra Leone');
INSERT INTO Country (Code, Name) VALUES ( 'SG', 'Singapore');
INSERT INTO Country (Code, Name) VALUES ( 'SK', 'Slovakia');
INSERT INTO Country (Code, Name) VALUES ( 'SI', 'Slovenia');
INSERT INTO Country (Code, Name) VALUES ( 'SB', 'Solomon Islands');
INSERT INTO Country (Code, Name) VALUES ( 'SO', 'Somalia');
INSERT INTO Country (Code, Name) VALUES ( 'ZA', 'South Africa');
INSERT INTO Country (Code, Name) VALUES ( 'GS', 'South Georgia South Sandwich Islands');
INSERT INTO Country (Code, Name) VALUES ( 'ES', 'Spain');
INSERT INTO Country (Code, Name) VALUES ( 'LK', 'Sri Lanka');
INSERT INTO Country (Code, Name) VALUES ( 'SH', 'St. Helena');
INSERT INTO Country (Code, Name) VALUES ( 'PM', 'St. Pierre and Miquelon');
INSERT INTO Country (Code, Name) VALUES ( 'SD', 'Sudan');
INSERT INTO Country (Code, Name) VALUES ( 'SR', 'Suriname');
INSERT INTO Country (Code, Name) VALUES ( 'SJ', 'Svalbarn and Jan Mayen Islands');
INSERT INTO Country (Code, Name) VALUES ( 'SZ', 'Swaziland');
INSERT INTO Country (Code, Name) VALUES ( 'SE', 'Sweden');
INSERT INTO Country (Code, Name) VALUES ( 'CH', 'Switzerland');
INSERT INTO Country (Code, Name) VALUES ( 'SY', 'Syrian Arab Republic');
INSERT INTO Country (Code, Name) VALUES ( 'TW', 'Taiwan');
INSERT INTO Country (Code, Name) VALUES ( 'TJ', 'Tajikistan');
INSERT INTO Country (Code, Name) VALUES ( 'TZ', 'Tanzania, United Republic of');
INSERT INTO Country (Code, Name) VALUES ( 'TH', 'Thailand');
INSERT INTO Country (Code, Name) VALUES ( 'TG', 'Togo');
INSERT INTO Country (Code, Name) VALUES ( 'TK', 'Tokelau');
INSERT INTO Country (Code, Name) VALUES ( 'TO', 'Tonga');
INSERT INTO Country (Code, Name) VALUES ( 'TT', 'Trinidad and Tobago');
INSERT INTO Country (Code, Name) VALUES ( 'TN', 'Tunisia');
INSERT INTO Country (Code, Name) VALUES ( 'TR', 'Turkey');
INSERT INTO Country (Code, Name) VALUES ( 'TM', 'Turkmenistan');
INSERT INTO Country (Code, Name) VALUES ( 'TC', 'Turks and Caicos Islands');
INSERT INTO Country (Code, Name) VALUES ( 'TV', 'Tuvalu');
INSERT INTO Country (Code, Name) VALUES ( 'UG', 'Uganda');
INSERT INTO Country (Code, Name) VALUES ( 'UA', 'Ukraine');
INSERT INTO Country (Code, Name) VALUES ( 'AE', 'United Arab Emirates');
INSERT INTO Country (Code, Name) VALUES ( 'GB', 'United Kingdom');
INSERT INTO Country (Code, Name) VALUES ( 'UM', 'United States minor outlying islands');
INSERT INTO Country (Code, Name) VALUES ( 'UY', 'Uruguay');
INSERT INTO Country (Code, Name) VALUES ( 'UZ', 'Uzbekistan');
INSERT INTO Country (Code, Name) VALUES ( 'VU', 'Vanuatu');
INSERT INTO Country (Code, Name) VALUES ( 'VA', 'Vatican City State');
INSERT INTO Country (Code, Name) VALUES ( 'VE', 'Venezuela');
INSERT INTO Country (Code, Name) VALUES ( 'VN', 'Vietnam');
INSERT INTO Country (Code, Name) VALUES ( 'VG', 'Virigan Islands (British)');
INSERT INTO Country (Code, Name) VALUES ( 'VI', 'Virgin Islands (U.S.)');
INSERT INTO Country (Code, Name) VALUES ( 'WF', 'Wallis and Futuna Islands');
INSERT INTO Country (Code, Name) VALUES ( 'EH', 'Western Sahara');
INSERT INTO Country (Code, Name) VALUES ( 'YE', 'Yemen');
INSERT INTO Country (Code, Name) VALUES ( 'YU', 'Yugoslavia');
INSERT INTO Country (Code, Name) VALUES ( 'ZR', 'Zaire');
INSERT INTO Country (Code, Name) VALUES ( 'ZM', 'Zambia');
INSERT INTO Country (Code, Name) VALUES ( 'ZW', 'Zimbabwe'); 