--book table--
create table Books(
	BookId int identity(1,1) primary key,
	BookName varchar(200) not null,
	Author varchar(200) not null,
	BookImage varchar(max) not null,
	BookDetail varchar(max) not null,
	DiscountPrice float not null,
	ActualPrice float not null,
	BookQuantity int not null,
	Rating float,
	RatingCount int 
)

--stored procedure for Books--
--add books--

create proc spAddbook(
	@BookName varchar(200),
	@Author varchar(200),
	@BookImage varchar(max),
	@BookDetail varchar(max),
	@DiscountPrice float,
	@ActualPrice float,
	@BookQuantity int,
	@Rating float,
	@RatingCount int
	)
as
begin
	insert into Books
	values(@BookName,@Author,@BookImage,@BookDetail,@DiscountPrice,@ActualPrice,@BookQuantity,@Rating,@RatingCount);
end

--update book--
create proc spUpdateBook(
	@BookId int,
	@BookName varchar(200),
	@Author varchar(200),
	@BookImage varchar(max),
	@BookDetail varchar(max),
	@DiscountPrice float,
	@ActualPrice float,
	@BookQuantity int,
	@Rating float,
	@RatingCount int
	)
as 
begin
	update Books set 
	BookName= @BookName,
	Author= @Author,
	BookImage = @BookImage,
	BookDetail= @BookDetail,
	DiscountPrice = @DiscountPrice,
	ActualPrice = @ActualPrice ,
	BookQuantity = @BookQuantity,
	Rating = @Rating,
	RatingCount= @RatingCount
	where BookId = @BookId;
end
		
--Delete book--
create proc spDeleteBook(
	@BookId int
	)
as
begin
	delete from Books where BookId=@BookId;
end	

--get all books--
create proc spGetAllBooks
as
begin
	select * from Books;
end

--get book by id--
create proc spGetBookById(
	@BookId int
	)
as
begin
	select * from Books where BookId=@BookId;
end	