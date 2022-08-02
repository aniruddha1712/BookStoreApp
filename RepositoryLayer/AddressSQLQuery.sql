--Address--
--Table for Address type--

create table AddressType(
	TypeId int identity(1,1) primary key,
	AddType varchar(200)
	)

--adding types--
insert into AddressType values('Home');
insert into AddressType values('Work');
insert into AddressType values('Other');

select * from Address;

--table for Address info--

create table Address(
	AddressId int identity(1,1) primary key,
	Address varchar(200) not null,
	City varchar(200) not null,
	State varchar(200) not null,
	TypeId int not null foreign key (TypeId) references AddressType(TypeId),
	UserId int not null foreign key (UserId) references Users(UserId)
	)

--stored procedure for address--
--add address--
create proc spAddAddress(
	@Address varchar(200),
	@City varchar(200),
	@State varchar(200),
	@TypeId int,
	@UserId int
	)
as
begin
	insert into Address
	values(@Address,@City,@State,@TypeId,@UserId);
end

--update address--
create proc spUpdateAddress(
	@AddressId int,
	@Address varchar(200),
	@City varchar(200),
	@State varchar(200),
	@TypeId int,
	@UserId int
	)
as
begin
	update Address set
	Address=@Address,City=@City,State=@State,TypeId=@TypeId where UserId=@UserId and AddressId=@AddressId;
end

--delete address-- 
create proc spDeleteAddress(
	@AddressId int,
	@UserId int
	)
as
begin
	delete from Address where UserId=@UserId and AddressId=@AddressId;
end

--get address--
create proc spGetAddress(
	@AddressId int,
	@UserId int
	)
as
begin
	select * from Address where UserId=@UserId and AddressId=@AddressId;
end

--get all address of user--
create proc spGetAllAddress(
	@UserId int
	)
as
begin
	select * from Address where UserId=@UserId;
end