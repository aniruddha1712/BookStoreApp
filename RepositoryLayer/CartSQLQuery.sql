--cart table--

create table Cart(
	CartId int identity(1,1) primary key,
	BookInCart int default 1,
	UserId int not null foreign key (UserId) references Users(UserId),
	BookId int not null foreign key (BookId) references Books(BookId)
)