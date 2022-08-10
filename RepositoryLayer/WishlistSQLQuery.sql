--Wishlist--

--table for wishlist--
create table Wishlist(
	WishlistId int identity (1,1) primary key,
	UserId int not null foreign key (UserId) references Users(UserId),
	BookId int not null foreign key (BookId) references Books(BookId)
	)

--procedure for wishlist--
select * from Wishlist;
--add to wishlist--
create proc spAddToWishlist(
	@BookId int,
	@UserId int
	)
as
begin
	if(not exists(select * from Wishlist where BookId=@BookId and UserId=@UserId))
	begin
		insert into Wishlist
		values(@UserId,@BookId);
	end
end

--remove from wishlist--
create proc spRemoveFromWishlist(
	@WishlistId int
	)
as
begin
	delete from Wishlist where WishlistId = @WishlistId;
end

--get wishlist item--
create proc spGetAllWishlistItem(
	@UserId int
	)
as
begin
	select wish.WishlistId,wish.BookId,wish.UserId,
		book.BookName,book.BookImage,book.Author,book.DiscountPrice,book.ActualPrice		
		from WishList wish inner join Books book
		on wish.BookId = book.BookId
		where wish.UserId = @UserId;
end