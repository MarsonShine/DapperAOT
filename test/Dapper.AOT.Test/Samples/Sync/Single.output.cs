// Output code has 2 diagnostics from 'Dapper.AOT.Analyzers/Dapper.CodeAnalysis.CommandGenerator/Single.output.cs':
// Dapper.AOT.Analyzers/Dapper.CodeAnalysis.CommandGenerator/Single.output.cs(691,21): error CS0161: 'Test.Scalar(DbConnection, int, string)': not all code paths return a value
// Dapper.AOT.Analyzers/Dapper.CodeAnalysis.CommandGenerator/Single.output.cs(722,11): error CS1029: #error: 'Scalar not implemented'

#nullable enable
//------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by:
//     Dapper.CodeAnalysis.CommandGenerator vN/A
// Changes to this file may cause incorrect behavior and
// will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#region Designer generated code
partial class Test
{

	// available inactive command for ImplicitComplex (interlocked)
	private static global::System.Data.Common.DbCommand? s___dapper__command_Samples_Sync_Single_input_cs_ImplicitComplex_6;

	[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
	[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
	public partial global::SomeType ImplicitComplex(global::System.Data.Common.DbConnection connection, int id, string name)
	{
		// locals
		global::System.Data.Common.DbCommand? __dapper__command = null;
		global::System.Data.Common.DbDataReader? __dapper__reader = null;
		bool __dapper__close = false;
		int[]? __dapper__tokenBuffer = null;
		try
		{
			// prepare connection
			if (connection!.State == global::System.Data.ConnectionState.Closed)
			{
				connection!.Open();
				__dapper__close = true;
			}

			// prepare command (excluding parameter values)
			if ((__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Single_input_cs_ImplicitComplex_6, null)) is null)
			{
				__dapper__command = __dapper__CreateCommand(connection!);
			}
			else
			{
				__dapper__command.Connection = connection;
			}

			// assign parameter values
			var __dapper__args = __dapper__command.Parameters;
#pragma warning disable CS0618
			__dapper__args[0].Value = global::Dapper.Internal.InternalUtilities.AsValue(id);
			__dapper__args[1].Value = global::Dapper.Internal.InternalUtilities.AsValue(name);
#pragma warning restore CS0618

			// execute reader
			const global::System.Data.CommandBehavior __dapper__behavior = global::System.Data.CommandBehavior.SequentialAccess | global::System.Data.CommandBehavior.SingleResult | global::System.Data.CommandBehavior.SingleRow;
			__dapper__reader = __dapper__command.ExecuteReader(__dapper__close ? (__dapper__behavior | global::System.Data.CommandBehavior.CloseConnection) : __dapper__behavior);
			__dapper__close = false; // performed via CommandBehavior (if needed)

			// process single row
			global::SomeType __dapper__result;
			if (__dapper__reader.HasRows && __dapper__reader.Read())
			{
				__dapper__result = global::Dapper.Internal.__dapper__Run_TypeReaders.SomeType.Instance.Read(__dapper__reader, ref __dapper__tokenBuffer);
			}
			else
			{
				__dapper__result = default!;
			}
			// consume additional results (ensures errors from the server are observed)
			while (__dapper__reader.NextResult()) { }
			return __dapper__result;

			// TODO: post-process parameters

		}
		finally
		{
			// cleanup
			global::Dapper.TypeReader.Return(ref __dapper__tokenBuffer);
			__dapper__reader?.Dispose();
			if (__dapper__command is not null)
			{
				__dapper__command.Connection = default;
				__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Single_input_cs_ImplicitComplex_6, __dapper__command);
				__dapper__command?.Dispose();
			}
			if (connection is not null)
			{
				if (__dapper__close) connection.Close();
			}
		}

		// command factory for ImplicitComplex
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
		[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
		static global::System.Data.Common.DbCommand __dapper__CreateCommand(global::System.Data.Common.DbConnection connection)
		{
			var command = connection.CreateCommand();
			if (command is global::Oracle.ManagedDataAccess.Client.OracleCommand typed0)
			{
				typed0.BindByName = true;
				typed0.InitialLONGFetchSize = -1;
			}
			command.CommandType = global::System.Data.CommandType.StoredProcedure;
			command.CommandText = @"sproc";
			var args = command.Parameters;

			var p = command.CreateParameter();
			p.ParameterName = @"id";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.Int32;
			args.Add(p);

			p = command.CreateParameter();
			p.ParameterName = @"name";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.String;
			p.Size = -1;
			args.Add(p);

			return command;
		}
	}


	// available inactive command for ImplicitScalar (interlocked)
	private static global::System.Data.Common.DbCommand? s___dapper__command_Samples_Sync_Single_input_cs_ImplicitScalar_9;

	[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
	[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
	public partial int ImplicitScalar(global::System.Data.Common.DbConnection connection, int id, string name)
	{
		// locals
		global::System.Data.Common.DbCommand? __dapper__command = null;
		global::System.Data.Common.DbDataReader? __dapper__reader = null;
		bool __dapper__close = false;
		int[]? __dapper__tokenBuffer = null;
		try
		{
			// prepare connection
			if (connection!.State == global::System.Data.ConnectionState.Closed)
			{
				connection!.Open();
				__dapper__close = true;
			}

			// prepare command (excluding parameter values)
			if ((__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Single_input_cs_ImplicitScalar_9, null)) is null)
			{
				__dapper__command = __dapper__CreateCommand(connection!);
			}
			else
			{
				__dapper__command.Connection = connection;
			}

			// assign parameter values
			var __dapper__args = __dapper__command.Parameters;
#pragma warning disable CS0618
			__dapper__args[0].Value = global::Dapper.Internal.InternalUtilities.AsValue(id);
			__dapper__args[1].Value = global::Dapper.Internal.InternalUtilities.AsValue(name);
#pragma warning restore CS0618

			// execute reader
			const global::System.Data.CommandBehavior __dapper__behavior = global::System.Data.CommandBehavior.SequentialAccess | global::System.Data.CommandBehavior.SingleResult | global::System.Data.CommandBehavior.SingleRow;
			__dapper__reader = __dapper__command.ExecuteReader(__dapper__close ? (__dapper__behavior | global::System.Data.CommandBehavior.CloseConnection) : __dapper__behavior);
			__dapper__close = false; // performed via CommandBehavior (if needed)

			// process single row
			int __dapper__result;
			if (__dapper__reader.HasRows && __dapper__reader.Read())
			{
				__dapper__result = global::Dapper.Internal.__dapper__Run_TypeReaders.Int32.Instance.Read(__dapper__reader, ref __dapper__tokenBuffer);
			}
			else
			{
				__dapper__result = default!;
			}
			// consume additional results (ensures errors from the server are observed)
			while (__dapper__reader.NextResult()) { }
			return __dapper__result;

			// TODO: post-process parameters

		}
		finally
		{
			// cleanup
			global::Dapper.TypeReader.Return(ref __dapper__tokenBuffer);
			__dapper__reader?.Dispose();
			if (__dapper__command is not null)
			{
				__dapper__command.Connection = default;
				__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Single_input_cs_ImplicitScalar_9, __dapper__command);
				__dapper__command?.Dispose();
			}
			if (connection is not null)
			{
				if (__dapper__close) connection.Close();
			}
		}

		// command factory for ImplicitScalar
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
		[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
		static global::System.Data.Common.DbCommand __dapper__CreateCommand(global::System.Data.Common.DbConnection connection)
		{
			var command = connection.CreateCommand();
			if (command is global::Oracle.ManagedDataAccess.Client.OracleCommand typed0)
			{
				typed0.BindByName = true;
				typed0.InitialLONGFetchSize = -1;
			}
			command.CommandType = global::System.Data.CommandType.StoredProcedure;
			command.CommandText = @"sproc";
			var args = command.Parameters;

			var p = command.CreateParameter();
			p.ParameterName = @"id";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.Int32;
			args.Add(p);

			p = command.CreateParameter();
			p.ParameterName = @"name";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.String;
			p.Size = -1;
			args.Add(p);

			return command;
		}
	}


	// available inactive command for First (interlocked)
	private static global::System.Data.Common.DbCommand? s___dapper__command_Samples_Sync_Single_input_cs_First_12;

	[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
	[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
	public partial global::SomeType First(global::System.Data.Common.DbConnection connection, int id, string name)
	{
		// locals
		global::System.Data.Common.DbCommand? __dapper__command = null;
		global::System.Data.Common.DbDataReader? __dapper__reader = null;
		bool __dapper__close = false;
		int[]? __dapper__tokenBuffer = null;
		try
		{
			// prepare connection
			if (connection!.State == global::System.Data.ConnectionState.Closed)
			{
				connection!.Open();
				__dapper__close = true;
			}

			// prepare command (excluding parameter values)
			if ((__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Single_input_cs_First_12, null)) is null)
			{
				__dapper__command = __dapper__CreateCommand(connection!);
			}
			else
			{
				__dapper__command.Connection = connection;
			}

			// assign parameter values
			var __dapper__args = __dapper__command.Parameters;
#pragma warning disable CS0618
			__dapper__args[0].Value = global::Dapper.Internal.InternalUtilities.AsValue(id);
			__dapper__args[1].Value = global::Dapper.Internal.InternalUtilities.AsValue(name);
#pragma warning restore CS0618

			// execute reader
			const global::System.Data.CommandBehavior __dapper__behavior = global::System.Data.CommandBehavior.SequentialAccess | global::System.Data.CommandBehavior.SingleResult | global::System.Data.CommandBehavior.SingleRow;
			__dapper__reader = __dapper__command.ExecuteReader(__dapper__close ? (__dapper__behavior | global::System.Data.CommandBehavior.CloseConnection) : __dapper__behavior);
			__dapper__close = false; // performed via CommandBehavior (if needed)

			// process single row
			global::SomeType __dapper__result;
			if (__dapper__reader.HasRows && __dapper__reader.Read())
			{
				__dapper__result = global::Dapper.Internal.__dapper__Run_TypeReaders.SomeType.Instance.Read(__dapper__reader, ref __dapper__tokenBuffer);
			}
			else
			{
#pragma warning disable CS0618
				global::Dapper.Internal.InternalUtilities.ThrowNone();
#pragma warning restore CS0618
				__dapper__result = default!;
			}
			// consume additional results (ensures errors from the server are observed)
			while (__dapper__reader.NextResult()) { }
			return __dapper__result;

			// TODO: post-process parameters

		}
		finally
		{
			// cleanup
			global::Dapper.TypeReader.Return(ref __dapper__tokenBuffer);
			__dapper__reader?.Dispose();
			if (__dapper__command is not null)
			{
				__dapper__command.Connection = default;
				__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Single_input_cs_First_12, __dapper__command);
				__dapper__command?.Dispose();
			}
			if (connection is not null)
			{
				if (__dapper__close) connection.Close();
			}
		}

		// command factory for First
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
		[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
		static global::System.Data.Common.DbCommand __dapper__CreateCommand(global::System.Data.Common.DbConnection connection)
		{
			var command = connection.CreateCommand();
			if (command is global::Oracle.ManagedDataAccess.Client.OracleCommand typed0)
			{
				typed0.BindByName = true;
				typed0.InitialLONGFetchSize = -1;
			}
			command.CommandType = global::System.Data.CommandType.StoredProcedure;
			command.CommandText = @"sproc";
			var args = command.Parameters;

			var p = command.CreateParameter();
			p.ParameterName = @"id";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.Int32;
			args.Add(p);

			p = command.CreateParameter();
			p.ParameterName = @"name";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.String;
			p.Size = -1;
			args.Add(p);

			return command;
		}
	}


	// available inactive command for FirstOrDefault (interlocked)
	private static global::System.Data.Common.DbCommand? s___dapper__command_Samples_Sync_Single_input_cs_FirstOrDefault_15;

	[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
	[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
	public partial global::SomeType FirstOrDefault(global::System.Data.Common.DbConnection connection, int id, string name)
	{
		// locals
		global::System.Data.Common.DbCommand? __dapper__command = null;
		global::System.Data.Common.DbDataReader? __dapper__reader = null;
		bool __dapper__close = false;
		int[]? __dapper__tokenBuffer = null;
		try
		{
			// prepare connection
			if (connection!.State == global::System.Data.ConnectionState.Closed)
			{
				connection!.Open();
				__dapper__close = true;
			}

			// prepare command (excluding parameter values)
			if ((__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Single_input_cs_FirstOrDefault_15, null)) is null)
			{
				__dapper__command = __dapper__CreateCommand(connection!);
			}
			else
			{
				__dapper__command.Connection = connection;
			}

			// assign parameter values
			var __dapper__args = __dapper__command.Parameters;
#pragma warning disable CS0618
			__dapper__args[0].Value = global::Dapper.Internal.InternalUtilities.AsValue(id);
			__dapper__args[1].Value = global::Dapper.Internal.InternalUtilities.AsValue(name);
#pragma warning restore CS0618

			// execute reader
			const global::System.Data.CommandBehavior __dapper__behavior = global::System.Data.CommandBehavior.SequentialAccess | global::System.Data.CommandBehavior.SingleResult | global::System.Data.CommandBehavior.SingleRow;
			__dapper__reader = __dapper__command.ExecuteReader(__dapper__close ? (__dapper__behavior | global::System.Data.CommandBehavior.CloseConnection) : __dapper__behavior);
			__dapper__close = false; // performed via CommandBehavior (if needed)

			// process single row
			global::SomeType __dapper__result;
			if (__dapper__reader.HasRows && __dapper__reader.Read())
			{
				__dapper__result = global::Dapper.Internal.__dapper__Run_TypeReaders.SomeType.Instance.Read(__dapper__reader, ref __dapper__tokenBuffer);
			}
			else
			{
				__dapper__result = default!;
			}
			// consume additional results (ensures errors from the server are observed)
			while (__dapper__reader.NextResult()) { }
			return __dapper__result;

			// TODO: post-process parameters

		}
		finally
		{
			// cleanup
			global::Dapper.TypeReader.Return(ref __dapper__tokenBuffer);
			__dapper__reader?.Dispose();
			if (__dapper__command is not null)
			{
				__dapper__command.Connection = default;
				__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Single_input_cs_FirstOrDefault_15, __dapper__command);
				__dapper__command?.Dispose();
			}
			if (connection is not null)
			{
				if (__dapper__close) connection.Close();
			}
		}

		// command factory for FirstOrDefault
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
		[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
		static global::System.Data.Common.DbCommand __dapper__CreateCommand(global::System.Data.Common.DbConnection connection)
		{
			var command = connection.CreateCommand();
			if (command is global::Oracle.ManagedDataAccess.Client.OracleCommand typed0)
			{
				typed0.BindByName = true;
				typed0.InitialLONGFetchSize = -1;
			}
			command.CommandType = global::System.Data.CommandType.StoredProcedure;
			command.CommandText = @"sproc";
			var args = command.Parameters;

			var p = command.CreateParameter();
			p.ParameterName = @"id";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.Int32;
			args.Add(p);

			p = command.CreateParameter();
			p.ParameterName = @"name";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.String;
			p.Size = -1;
			args.Add(p);

			return command;
		}
	}


	// available inactive command for Single (interlocked)
	private static global::System.Data.Common.DbCommand? s___dapper__command_Samples_Sync_Single_input_cs_Single_18;

	[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
	[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
	public partial global::SomeType Single(global::System.Data.Common.DbConnection connection, int id, string name)
	{
		// locals
		global::System.Data.Common.DbCommand? __dapper__command = null;
		global::System.Data.Common.DbDataReader? __dapper__reader = null;
		bool __dapper__close = false;
		int[]? __dapper__tokenBuffer = null;
		try
		{
			// prepare connection
			if (connection!.State == global::System.Data.ConnectionState.Closed)
			{
				connection!.Open();
				__dapper__close = true;
			}

			// prepare command (excluding parameter values)
			if ((__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Single_input_cs_Single_18, null)) is null)
			{
				__dapper__command = __dapper__CreateCommand(connection!);
			}
			else
			{
				__dapper__command.Connection = connection;
			}

			// assign parameter values
			var __dapper__args = __dapper__command.Parameters;
#pragma warning disable CS0618
			__dapper__args[0].Value = global::Dapper.Internal.InternalUtilities.AsValue(id);
			__dapper__args[1].Value = global::Dapper.Internal.InternalUtilities.AsValue(name);
#pragma warning restore CS0618

			// execute reader
			const global::System.Data.CommandBehavior __dapper__behavior = global::System.Data.CommandBehavior.SequentialAccess | global::System.Data.CommandBehavior.SingleResult;
			__dapper__reader = __dapper__command.ExecuteReader(__dapper__close ? (__dapper__behavior | global::System.Data.CommandBehavior.CloseConnection) : __dapper__behavior);
			__dapper__close = false; // performed via CommandBehavior (if needed)

			// process single row
			global::SomeType __dapper__result;
			if (__dapper__reader.HasRows && __dapper__reader.Read())
			{
				__dapper__result = global::Dapper.Internal.__dapper__Run_TypeReaders.SomeType.Instance.Read(__dapper__reader, ref __dapper__tokenBuffer);
#pragma warning disable CS0618
				if (__dapper__reader.Read()) global::Dapper.Internal.InternalUtilities.ThrowMultiple();
#pragma warning restore CS0618
			}
			else
			{
#pragma warning disable CS0618
				global::Dapper.Internal.InternalUtilities.ThrowNone();
#pragma warning restore CS0618
				__dapper__result = default!;
			}
			// consume additional results (ensures errors from the server are observed)
			while (__dapper__reader.NextResult()) { }
			return __dapper__result;

			// TODO: post-process parameters

		}
		finally
		{
			// cleanup
			global::Dapper.TypeReader.Return(ref __dapper__tokenBuffer);
			__dapper__reader?.Dispose();
			if (__dapper__command is not null)
			{
				__dapper__command.Connection = default;
				__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Single_input_cs_Single_18, __dapper__command);
				__dapper__command?.Dispose();
			}
			if (connection is not null)
			{
				if (__dapper__close) connection.Close();
			}
		}

		// command factory for Single
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
		[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
		static global::System.Data.Common.DbCommand __dapper__CreateCommand(global::System.Data.Common.DbConnection connection)
		{
			var command = connection.CreateCommand();
			if (command is global::Oracle.ManagedDataAccess.Client.OracleCommand typed0)
			{
				typed0.BindByName = true;
				typed0.InitialLONGFetchSize = -1;
			}
			command.CommandType = global::System.Data.CommandType.StoredProcedure;
			command.CommandText = @"sproc";
			var args = command.Parameters;

			var p = command.CreateParameter();
			p.ParameterName = @"id";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.Int32;
			args.Add(p);

			p = command.CreateParameter();
			p.ParameterName = @"name";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.String;
			p.Size = -1;
			args.Add(p);

			return command;
		}
	}


	// available inactive command for SingleOrDefault (interlocked)
	private static global::System.Data.Common.DbCommand? s___dapper__command_Samples_Sync_Single_input_cs_SingleOrDefault_21;

	[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
	[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
	public partial global::SomeType SingleOrDefault(global::System.Data.Common.DbConnection connection, int id, string name)
	{
		// locals
		global::System.Data.Common.DbCommand? __dapper__command = null;
		global::System.Data.Common.DbDataReader? __dapper__reader = null;
		bool __dapper__close = false;
		int[]? __dapper__tokenBuffer = null;
		try
		{
			// prepare connection
			if (connection!.State == global::System.Data.ConnectionState.Closed)
			{
				connection!.Open();
				__dapper__close = true;
			}

			// prepare command (excluding parameter values)
			if ((__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Single_input_cs_SingleOrDefault_21, null)) is null)
			{
				__dapper__command = __dapper__CreateCommand(connection!);
			}
			else
			{
				__dapper__command.Connection = connection;
			}

			// assign parameter values
			var __dapper__args = __dapper__command.Parameters;
#pragma warning disable CS0618
			__dapper__args[0].Value = global::Dapper.Internal.InternalUtilities.AsValue(id);
			__dapper__args[1].Value = global::Dapper.Internal.InternalUtilities.AsValue(name);
#pragma warning restore CS0618

			// execute reader
			const global::System.Data.CommandBehavior __dapper__behavior = global::System.Data.CommandBehavior.SequentialAccess | global::System.Data.CommandBehavior.SingleResult;
			__dapper__reader = __dapper__command.ExecuteReader(__dapper__close ? (__dapper__behavior | global::System.Data.CommandBehavior.CloseConnection) : __dapper__behavior);
			__dapper__close = false; // performed via CommandBehavior (if needed)

			// process single row
			global::SomeType __dapper__result;
			if (__dapper__reader.HasRows && __dapper__reader.Read())
			{
				__dapper__result = global::Dapper.Internal.__dapper__Run_TypeReaders.SomeType.Instance.Read(__dapper__reader, ref __dapper__tokenBuffer);
#pragma warning disable CS0618
				if (__dapper__reader.Read()) global::Dapper.Internal.InternalUtilities.ThrowMultiple();
#pragma warning restore CS0618
			}
			else
			{
				__dapper__result = default!;
			}
			// consume additional results (ensures errors from the server are observed)
			while (__dapper__reader.NextResult()) { }
			return __dapper__result;

			// TODO: post-process parameters

		}
		finally
		{
			// cleanup
			global::Dapper.TypeReader.Return(ref __dapper__tokenBuffer);
			__dapper__reader?.Dispose();
			if (__dapper__command is not null)
			{
				__dapper__command.Connection = default;
				__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Single_input_cs_SingleOrDefault_21, __dapper__command);
				__dapper__command?.Dispose();
			}
			if (connection is not null)
			{
				if (__dapper__close) connection.Close();
			}
		}

		// command factory for SingleOrDefault
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
		[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
		static global::System.Data.Common.DbCommand __dapper__CreateCommand(global::System.Data.Common.DbConnection connection)
		{
			var command = connection.CreateCommand();
			if (command is global::Oracle.ManagedDataAccess.Client.OracleCommand typed0)
			{
				typed0.BindByName = true;
				typed0.InitialLONGFetchSize = -1;
			}
			command.CommandType = global::System.Data.CommandType.StoredProcedure;
			command.CommandText = @"sproc";
			var args = command.Parameters;

			var p = command.CreateParameter();
			p.ParameterName = @"id";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.Int32;
			args.Add(p);

			p = command.CreateParameter();
			p.ParameterName = @"name";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.String;
			p.Size = -1;
			args.Add(p);

			return command;
		}
	}


	// available inactive command for Scalar (interlocked)
	private static global::System.Data.Common.DbCommand? s___dapper__command_Samples_Sync_Single_input_cs_Scalar_24;

	[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
	[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
	public partial int Scalar(global::System.Data.Common.DbConnection connection, int id, string name)
	{
		// locals
		global::System.Data.Common.DbCommand? __dapper__command = null;
		bool __dapper__close = false;
		try
		{
			// prepare connection
			if (connection!.State == global::System.Data.ConnectionState.Closed)
			{
				connection!.Open();
				__dapper__close = true;
			}

			// prepare command (excluding parameter values)
			if ((__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Single_input_cs_Scalar_24, null)) is null)
			{
				__dapper__command = __dapper__CreateCommand(connection!);
			}
			else
			{
				__dapper__command.Connection = connection;
			}

			// assign parameter values
			var __dapper__args = __dapper__command.Parameters;
#pragma warning disable CS0618
			__dapper__args[0].Value = global::Dapper.Internal.InternalUtilities.AsValue(id);
			__dapper__args[1].Value = global::Dapper.Internal.InternalUtilities.AsValue(name);
#pragma warning restore CS0618

			#error Scalar not implemented

			// TODO: post-process parameters

		}
		finally
		{
			// cleanup
			if (__dapper__command is not null)
			{
				__dapper__command.Connection = default;
				__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Single_input_cs_Scalar_24, __dapper__command);
				__dapper__command?.Dispose();
			}
			if (connection is not null)
			{
				if (__dapper__close) connection.Close();
			}
		}

		// command factory for Scalar
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
		[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
		static global::System.Data.Common.DbCommand __dapper__CreateCommand(global::System.Data.Common.DbConnection connection)
		{
			var command = connection.CreateCommand();
			if (command is global::Oracle.ManagedDataAccess.Client.OracleCommand typed0)
			{
				typed0.BindByName = true;
				typed0.InitialLONGFetchSize = -1;
			}
			command.CommandType = global::System.Data.CommandType.StoredProcedure;
			command.CommandText = @"sproc";
			var args = command.Parameters;

			var p = command.CreateParameter();
			p.ParameterName = @"id";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.Int32;
			args.Add(p);

			p = command.CreateParameter();
			p.ParameterName = @"name";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.String;
			p.Size = -1;
			args.Add(p);

			return command;
		}
	}
}

namespace Dapper.Internal.__dapper__Run_TypeReaders
{
	[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
	[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
	file sealed class SomeType : global::Dapper.TypeReader<global::SomeType>
	{
		private SomeType() { }
		public static readonly SomeType Instance = new();

		/// <inheritdoc/>
		public override int GetToken(int token, global::System.Type type, bool isNullable) => token;

		/// <inheritdoc/>
		public override global::SomeType Read(global::System.Data.Common.DbDataReader reader, global::System.ReadOnlySpan<int> tokens, int columnOffset = 0)
		{
			global::SomeType obj = new();
			return obj;
		}
	}
	[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
	[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
	file sealed class Int32 : global::Dapper.TypeReader<int>
	{
		private Int32() { }
		public static readonly Int32 Instance = new();

		/// <inheritdoc/>
		public override int GetToken(int token, global::System.Type type, bool isNullable) => token;

		/// <inheritdoc/>
		public override int Read(global::System.Data.Common.DbDataReader reader, global::System.ReadOnlySpan<int> tokens, int columnOffset = 0)
		{
			int obj = new();
			return obj;
		}
	}
}
#endregion
