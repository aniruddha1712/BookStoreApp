--cart table--

create table Cart(
	CartId int identity(1,1) primary key,
	BookInCart int default 1,
	UserId int not null foreign key (UserId) references Users(UserId),
	BookId int not null foreign key (BookId) references Books(BookId)
)

--stored procedure for cart--
select * from Cart;
--Add to cart--
create proc spAddToCart(
	@BookId int,
	@BookInCart int,
	@UserId int
	)
as
begin
	if(not exists(select * from Cart where BookId=@BookId and UserId=@UserId))
	begin
		insert into Cart
		values(@BookInCart,@UserId,@BookId);
	end
end

--update cart--
create proc spUpdateCart(
	@CartId int,
	@BookInCart int
	)
as
begin
	update Cart set BookInCart=@BookInCart where CartId=@CartId;
end

--delete/remove from cart--
create proc spRemoveFromCart(
	@CartId int
	)
as
begin
	delete from Cart where CartId=@CartId;
end

--get all cart items--
create proc spGetAllCartItem(
	@UserId int
	)
as
begin
	select cart.CartId,cart.BookId,cart.BookInCart,cart.UserId,
		book.BookName,book.BookImage,book.Author,book.DiscountPrice,book.ActualPrice from Cart cart inner join Books book 
		on book.BookId=cart.BookId where cart.UserId = @UserId;
end