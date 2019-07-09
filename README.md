# NET_SqlTransaction_BUG

Repository to reproduce a bug in SqlTransaction class of .NET 

Validated with versions 4.7.x and 4.8 and .NET Core 3.0 preview 7

used SQL Server 2014

Collation: Latin1_General_100_CS_AS

In Program.cs

please define your connection string in "private static string ConnectionString"

Look on console messages

im my case

Test1

The CREATE UNIQUE INDEX statement terminated because a duplicate key was found for the object name 'dbo.TEST_SCHEME' and the index name 'C216_K1'. The duplicate key value is (Test).

The statement has been terminated.

Test2

Cannot insert duplicate key row in object 'dbo.TEST_SCHEME' with unique index 'C216_K1'. The duplicate key value is (Test).
The statement has been terminated.

Wrong behavior Exception :This SqlTransaction has completed; it is no longer usable.


.NET Core Results

Test1

The CREATE UNIQUE INDEX statement terminated because a duplicate key was found for the object name 'dbo.TEST_SCHEME' and the index name 'C216_K1'. The duplicate key value is (Test).

The statement has been terminated.

Wrong behavior Exception :This SqlTransaction has completed; it is no longer usable.

Test2

Cannot insert duplicate key row in object 'dbo.TEST_SCHEME' with unique index 'C216_K1'. The duplicate key value is (Test).

The statement has been terminated.

Wrong behavior Exception :This SqlTransaction has completed; it is no longer usable.

