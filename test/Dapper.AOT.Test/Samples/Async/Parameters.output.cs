// Input code has 1 diagnostics from 'Samples/Async/Parameters.input.cs':
// Samples/Async/Parameters.input.cs(13,16): warning CS8632: The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
// Output code has 1 diagnostics from 'Samples/Async/Parameters.input.cs':
// Samples/Async/Parameters.input.cs(13,16): warning CS8632: The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
// Output code has 1 diagnostics from 'Dapper.AOT.Analyzers/Dapper.CodeAnalysis.CommandGenerator/Parameters.output.cs':
// Dapper.AOT.Analyzers/Dapper.CodeAnalysis.CommandGenerator/Parameters.output.cs(20,69): error CS0161: 'Test.ReturnViaReturnAsync(DbConnection, Test.FooParams)': not all code paths return a value

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

	// available inactive command for ReturnViaReturnAsync (interlocked)
	private static global::System.Data.Common.DbCommand? s___dapper__command_Samples_Async_Parameters_input_cs_ReturnViaReturnAsync_19;

	[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
	[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
	[global::System.Runtime.CompilerServices.AsyncMethodBuilderAttribute(typeof(global::System.Runtime.CompilerServices.PoolingAsyncValueTaskMethodBuilder<>))]
	public async partial global::System.Threading.Tasks.ValueTask<int> ReturnViaReturnAsync(global::System.Data.Common.DbConnection connection, global::Test.FooParams parameters)
	{
		// locals
		global::System.Data.Common.DbCommand? __dapper__command = null;
		bool __dapper__close = false;
		try
		{
			// prepare connection
			if (connection!.State == global::System.Data.ConnectionState.Closed)
			{
				await connection!.OpenAsync(global::System.Threading.CancellationToken.None).ConfigureAwait(false);
				__dapper__close = true;
			}

			// prepare command (excluding parameter values)
			if ((__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Async_Parameters_input_cs_ReturnViaReturnAsync_19, null)) is null)
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
			__dapper__args[0].Value = global::Dapper.Internal.InternalUtilities.AsValue(parameters);
#pragma warning restore CS0618

			// execute non-query
			await __dapper__command.ExecuteNonQueryAsync(global::System.Threading.CancellationToken.None).ConfigureAwait(false);

			// TODO: post-process parameters

		}
		finally
		{
			// cleanup
			if (__dapper__command is not null)
			{
				__dapper__command.Connection = default;
				__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Async_Parameters_input_cs_ReturnViaReturnAsync_19, __dapper__command);
				if (__dapper__command is not null) await __dapper__command.DisposeAsync().ConfigureAwait(false);
			}
			if (connection is not null)
			{
				if (__dapper__close) await (connection.CloseAsync() ?? global::System.Threading.Tasks.Task.CompletedTask).ConfigureAwait(false);
			}
		}

		// command factory for ReturnViaReturnAsync
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
			p.ParameterName = @"parameters";
			p.Direction = global::System.Data.ParameterDirection.Input;
			args.Add(p);

			return command;
		}
	}


	// available inactive command for RecordsAffectedViaReturnAsync (interlocked)
	private static global::System.Data.Common.DbCommand? s___dapper__command_Samples_Async_Parameters_input_cs_RecordsAffectedViaReturnAsync_23;

	[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
	[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
	[global::System.Runtime.CompilerServices.AsyncMethodBuilderAttribute(typeof(global::System.Runtime.CompilerServices.PoolingAsyncValueTaskMethodBuilder<>))]
	public async partial global::System.Threading.Tasks.ValueTask<int> RecordsAffectedViaReturnAsync(global::System.Data.Common.DbConnection connection, global::Test.FooParams parameters)
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
				await connection!.OpenAsync(global::System.Threading.CancellationToken.None).ConfigureAwait(false);
				__dapper__close = true;
			}

			// prepare command (excluding parameter values)
			if ((__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Async_Parameters_input_cs_RecordsAffectedViaReturnAsync_23, null)) is null)
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
			__dapper__args[0].Value = global::Dapper.Internal.InternalUtilities.AsValue(parameters);
#pragma warning restore CS0618

			// execute reader
			const global::System.Data.CommandBehavior __dapper__behavior = global::System.Data.CommandBehavior.SequentialAccess | global::System.Data.CommandBehavior.SingleResult | global::System.Data.CommandBehavior.SingleRow;
			__dapper__reader = await __dapper__command.ExecuteReaderAsync(__dapper__close ? (__dapper__behavior | global::System.Data.CommandBehavior.CloseConnection) : __dapper__behavior, global::System.Threading.CancellationToken.None).ConfigureAwait(false);
			__dapper__close = false; // performed via CommandBehavior (if needed)

			// process single row
			int __dapper__result;
			if (__dapper__reader.HasRows && await __dapper__reader.ReadAsync(global::System.Threading.CancellationToken.None).ConfigureAwait(false))
			{
				__dapper__result = global::Dapper.Internal.__dapper__Run_TypeReaders.Int32.Instance.Read(__dapper__reader, ref __dapper__tokenBuffer);
			}
			else
			{
				__dapper__result = default!;
			}
			// consume additional results (ensures errors from the server are observed)
			while (await __dapper__reader.NextResultAsync(global::System.Threading.CancellationToken.None).ConfigureAwait(false)) { }
			return __dapper__result;

			// TODO: post-process parameters

		}
		finally
		{
			// cleanup
			global::Dapper.TypeReader.Return(ref __dapper__tokenBuffer);
			if (__dapper__reader is not null) await __dapper__reader.DisposeAsync().ConfigureAwait(false);
			if (__dapper__command is not null)
			{
				__dapper__command.Connection = default;
				__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Async_Parameters_input_cs_RecordsAffectedViaReturnAsync_23, __dapper__command);
				if (__dapper__command is not null) await __dapper__command.DisposeAsync().ConfigureAwait(false);
			}
			if (connection is not null)
			{
				if (__dapper__close) await (connection.CloseAsync() ?? global::System.Threading.Tasks.Task.CompletedTask).ConfigureAwait(false);
			}
		}

		// command factory for RecordsAffectedViaReturnAsync
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
			p.ParameterName = @"parameters";
			p.Direction = global::System.Data.ParameterDirection.Input;
			args.Add(p);

			return command;
		}
	}


	// available inactive command for ReturnAndRecordsAffectedViaOutAsync (interlocked)
	private static global::System.Data.Common.DbCommand? s___dapper__command_Samples_Async_Parameters_input_cs_ReturnAndRecordsAffectedViaOutAsync_35;

	[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
	[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
	[global::System.Runtime.CompilerServices.AsyncMethodBuilderAttribute(typeof(global::System.Runtime.CompilerServices.PoolingAsyncValueTaskMethodBuilder))]
	public async partial global::System.Threading.Tasks.ValueTask ReturnAndRecordsAffectedViaOutAsync(global::System.Data.Common.DbConnection connection, global::Test.BarParams parameters)
	{
		// locals
		global::System.Data.Common.DbCommand? __dapper__command = null;
		bool __dapper__close = false;
		try
		{
			// prepare connection
			if (connection!.State == global::System.Data.ConnectionState.Closed)
			{
				await connection!.OpenAsync(global::System.Threading.CancellationToken.None).ConfigureAwait(false);
				__dapper__close = true;
			}

			// prepare command (excluding parameter values)
			if ((__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Async_Parameters_input_cs_ReturnAndRecordsAffectedViaOutAsync_35, null)) is null)
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
			__dapper__args[0].Value = global::Dapper.Internal.InternalUtilities.AsValue(parameters);
#pragma warning restore CS0618

			// execute non-query
			await __dapper__command.ExecuteNonQueryAsync(global::System.Threading.CancellationToken.None).ConfigureAwait(false);

			// TODO: post-process parameters

		}
		finally
		{
			// cleanup
			if (__dapper__command is not null)
			{
				__dapper__command.Connection = default;
				__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Async_Parameters_input_cs_ReturnAndRecordsAffectedViaOutAsync_35, __dapper__command);
				if (__dapper__command is not null) await __dapper__command.DisposeAsync().ConfigureAwait(false);
			}
			if (connection is not null)
			{
				if (__dapper__close) await (connection.CloseAsync() ?? global::System.Threading.Tasks.Task.CompletedTask).ConfigureAwait(false);
			}
		}

		// command factory for ReturnAndRecordsAffectedViaOutAsync
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
			p.ParameterName = @"parameters";
			p.Direction = global::System.Data.ParameterDirection.Input;
			args.Add(p);

			return command;
		}
	}


	// available inactive command for FineControlAsync (interlocked)
	private static global::System.Data.Common.DbCommand? s___dapper__command_Samples_Async_Parameters_input_cs_FineControlAsync_38;

	[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
	[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
	[global::System.Runtime.CompilerServices.AsyncMethodBuilderAttribute(typeof(global::System.Runtime.CompilerServices.PoolingAsyncValueTaskMethodBuilder))]
	public async partial global::System.Threading.Tasks.ValueTask FineControlAsync(global::System.Data.Common.DbConnection connection, decimal value)
	{
		// locals
		global::System.Data.Common.DbCommand? __dapper__command = null;
		bool __dapper__close = false;
		try
		{
			// prepare connection
			if (connection!.State == global::System.Data.ConnectionState.Closed)
			{
				await connection!.OpenAsync(global::System.Threading.CancellationToken.None).ConfigureAwait(false);
				__dapper__close = true;
			}

			// prepare command (excluding parameter values)
			if ((__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Async_Parameters_input_cs_FineControlAsync_38, null)) is null)
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
			__dapper__args[0].Value = global::Dapper.Internal.InternalUtilities.AsValue(value);
#pragma warning restore CS0618

			// execute non-query
			await __dapper__command.ExecuteNonQueryAsync(global::System.Threading.CancellationToken.None).ConfigureAwait(false);

			// TODO: post-process parameters

		}
		finally
		{
			// cleanup
			if (__dapper__command is not null)
			{
				__dapper__command.Connection = default;
				__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Async_Parameters_input_cs_FineControlAsync_38, __dapper__command);
				if (__dapper__command is not null) await __dapper__command.DisposeAsync().ConfigureAwait(false);
			}
			if (connection is not null)
			{
				if (__dapper__close) await (connection.CloseAsync() ?? global::System.Threading.Tasks.Task.CompletedTask).ConfigureAwait(false);
			}
		}

		// command factory for FineControlAsync
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
			p.ParameterName = @"value";
			p.Direction = global::System.Data.ParameterDirection.Input;
			args.Add(p);

			return command;
		}
	}
}

namespace Dapper.Internal.__dapper__Run_TypeReaders
{
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
