using System;
using System.Data;
using System.Data.SqlClient;

namespace TestSqlTransaction
{
	class Program
	{
		private static string ConnectionString =
			"Data Source= . . . ";

		static void Main(string[] args)
		{
			Console.WriteLine("Test1");
			Test1();
			Console.WriteLine("");
			Console.WriteLine("Test2");
			Test2();
		}

		static void Test1()
		{
			SqlTransaction transaction = null;
			try
			{
				using (SqlConnection connection = new SqlConnection(ConnectionString))
				{
					connection.Open();
					transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

					using (SqlCommand cmd1 = connection.CreateCommand())
					{
						cmd1.Connection = connection;
						cmd1.Transaction = transaction;

						cmd1.CommandText =
							@"CREATE TABLE TEST_SCHEME(
								[NAME] [nvarchar](129) NULL,
								[INSTID] [int] NOT NULL)";

						cmd1.ExecuteNonQuery();
					}

					using (SqlCommand insertCmd = connection.CreateCommand())
					{
						insertCmd.Connection = connection;
						insertCmd.Transaction = transaction;
						for (int i = 0; i < 2; i++)
						{
							insertCmd.CommandText = $"INSERT INTO TEST_SCHEME (NAME, INSTID) VALUES ('Test', {i})";
							insertCmd.ExecuteNonQuery();
						}
					}

					using (SqlCommand cmdIndex = connection.CreateCommand())
					{
						cmdIndex.Connection = connection;
						cmdIndex.Transaction = transaction;
						cmdIndex.CommandText = "CREATE UNIQUE INDEX C216_K1 ON TEST_SCHEME (NAME ASC )";
						cmdIndex.ExecuteNonQuery();
					}

					transaction.Commit();
				}
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				Console.WriteLine(message);
				try
				{
					transaction?.Rollback();
				}
				catch (Exception sqlException)
				{
					string msg = sqlException.Message;
					Console.WriteLine($"Wrong behavior Exception :{msg}");
				}
			}
		}

		static void Test2()
		{
			SqlTransaction transaction = null;
			try
			{
				using (SqlConnection connection = new SqlConnection(ConnectionString))
				{
					connection.Open();
					transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

					using (SqlCommand cmd1 = connection.CreateCommand())
					{
						cmd1.Connection = connection;
						cmd1.Transaction = transaction;

						cmd1.CommandText =
							@"CREATE TABLE TEST_SCHEME(
								[NAME] [nvarchar](129) NULL,
								[INSTID] [int] NOT NULL)";

						cmd1.ExecuteNonQuery();
					}

					using (SqlCommand cmdIndex = connection.CreateCommand())
					{
						cmdIndex.Connection = connection;
						cmdIndex.Transaction = transaction;
						cmdIndex.CommandText = "CREATE UNIQUE INDEX C216_K1 ON TEST_SCHEME (NAME ASC )";
						cmdIndex.ExecuteNonQuery();
					}

					using (SqlCommand insertCmd = connection.CreateCommand())
					{
						insertCmd.Connection = connection;
						insertCmd.Transaction = transaction;
						for (int i = 0; i < 2; i++)
						{
							insertCmd.CommandText = $"INSERT INTO TEST_SCHEME (NAME,INSTID) VALUES ('Test', {i})";
							insertCmd.ExecuteNonQuery();
						}
					}	

					transaction.Commit();
				}
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				Console.WriteLine(message);
				try
				{
					transaction?.Rollback();
				}
				catch (Exception sqlException)
				{
					string msg = sqlException.Message;
					Console.WriteLine($"Wrong behavior Exception :{msg}");
				}
			}
		}

	}
}
