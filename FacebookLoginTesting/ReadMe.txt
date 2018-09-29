---------------------------For database--------------------

CREATE TABLE UserProfile (
    UserId nvarchar(128) primary key not null,
    Name varchar(128),
    Email varchar(200),
    BloodType varchar(10),
	Age varchar(10),
	PhoneNumber varchar(200),
	DateOfDonation datetime,
	Location varchar(200),
	ImagePath  varchar(max),
);


CREATE TABLE Post (
    PostId int primary key identity(1,1) not null,
	UserId nvarchar(128) foreign key REFERENCES UserProfile(UserId),
    BloodType varchar(10),
	Age varchar(10),
	Location varchar(200),
	Illness varchar(200),
	PhoneNumber varchar(200),
);


-----------------------------------------------------------------


-------------------Web project--------------------------------

just go and change the server name with your sql server name
