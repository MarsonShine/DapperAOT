// Input code has 2 diagnostics from 'Samples/Sync/Parameters.input.cs':
// Samples/Sync/Parameters.input.cs(13,3): error CS0592: Attribute 'RecordsAffected' is not valid on this declaration type. It is only valid on 'property, indexer, field, parameter, return' declarations.
// Samples/Sync/Parameters.input.cs(18,72): error CS1003: Syntax error, ',' expected
// Output code has 5 diagnostics from 'Samples/Sync/Parameters.input.cs':
// Samples/Sync/Parameters.input.cs(10,21): error CS8795: Partial method 'Test.ReturnViaReturn(DbConnection, int, ref string, out DateTime)' must have an implementation part because it has accessibility modifiers.
// Samples/Sync/Parameters.input.cs(13,3): error CS0592: Attribute 'RecordsAffected' is not valid on this declaration type. It is only valid on 'property, indexer, field, parameter, return' declarations.
// Samples/Sync/Parameters.input.cs(14,21): error CS8795: Partial method 'Test.RecordsAffectedViaReturn(DbConnection, int, ref string, out DateTime)' must have an implementation part because it has accessibility modifiers.
// Samples/Sync/Parameters.input.cs(17,22): error CS8795: Partial method 'Test.ReturnAndRecordsAffectedViaOut(DbConnection, int, ref string, out DateTime, out int, out int)' must have an implementation part because it has accessibility modifiers.
// Samples/Sync/Parameters.input.cs(18,72): error CS1003: Syntax error, ',' expected
// Output code has 4 diagnostics from 'Dapper.AOT.Analyzers/Dapper.CodeAnalysis.CommandGenerator/Parameters.output.cs':
// Dapper.AOT.Analyzers/Dapper.CodeAnalysis.CommandGenerator/Parameters.output.cs(19,21): error CS0161: 'Test.ReturnViaReturn(DbConnection, int, string, DateTime)': not all code paths return a value
// Dapper.AOT.Analyzers/Dapper.CodeAnalysis.CommandGenerator/Parameters.output.cs(19,21): error CS0759: No defining declaration found for implementing declaration of partial method 'Test.ReturnViaReturn(DbConnection, int, string, DateTime)'
// Dapper.AOT.Analyzers/Dapper.CodeAnalysis.CommandGenerator/Parameters.output.cs(116,21): error CS0759: No defining declaration found for implementing declaration of partial method 'Test.RecordsAffectedViaReturn(DbConnection, int, string, DateTime)'
// Dapper.AOT.Analyzers/Dapper.CodeAnalysis.CommandGenerator/Parameters.output.cs(233,22): error CS0759: No defining declaration found for implementing declaration of partial method 'Test.ReturnAndRecordsAffectedViaOut(DbConnection, int, string, DateTime, int, int)'

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

	// available inactive command for ReturnViaReturn (interlocked)
	private static global::System.Data.Common.DbCommand? s___dapper__command_Samples_Sync_Parameters_input_cs_ReturnViaReturn_9;

	[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
	[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
	public partial int ReturnViaReturn(global::System.Data.Common.DbConnection connection, int id, string inputOutput, global::System.DateTime output)
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
			if ((__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Parameters_input_cs_ReturnViaReturn_9, null)) is null)
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
			__dapper__args[1].Value = global::Dapper.Internal.InternalUtilities.AsValue(inputOutput);
			__dapper__args[2].Value = global::Dapper.Internal.InternalUtilities.AsValue(output);
#pragma warning restore CS0618

			// execute non-query
			__dapper__command.ExecuteNonQuery();

			// TODO: post-process parameters

		}
		finally
		{
			// cleanup
			if (__dapper__command is not null)
			{
				__dapper__command.Connection = default;
				__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Parameters_input_cs_ReturnViaReturn_9, __dapper__command);
				__dapper__command?.Dispose();
			}
			if (connection is not null)
			{
				if (__dapper__close) connection.Close();
			}
		}

		// command factory for ReturnViaReturn
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
			p.ParameterName = @"inputOutput";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.String;
			p.Size = -1;
			args.Add(p);

			p = command.CreateParameter();
			p.ParameterName = @"output";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.DateTime;
			args.Add(p);

			return command;
		}
	}


	// available inactive command for RecordsAffectedViaReturn (interlocked)
	private static global::System.Data.Common.DbCommand? s___dapper__command_Samples_Sync_Parameters_input_cs_RecordsAffectedViaReturn_13;

	[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
	[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
	public partial int RecordsAffectedViaReturn(global::System.Data.Common.DbConnection connection, int id, string inputOutput, global::System.DateTime output)
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
			if ((__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Parameters_input_cs_RecordsAffectedViaReturn_13, null)) is null)
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
			__dapper__args[1].Value = global::Dapper.Internal.InternalUtilities.AsValue(inputOutput);
			__dapper__args[2].Value = global::Dapper.Internal.InternalUtilities.AsValue(output);
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
				__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Parameters_input_cs_RecordsAffectedViaReturn_13, __dapper__command);
				__dapper__command?.Dispose();
			}
			if (connection is not null)
			{
				if (__dapper__close) connection.Close();
			}
		}

		// command factory for RecordsAffectedViaReturn
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
			p.ParameterName = @"inputOutput";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.String;
			p.Size = -1;
			args.Add(p);

			p = command.CreateParameter();
			p.ParameterName = @"output";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.DateTime;
			args.Add(p);

			return command;
		}
	}


	// available inactive command for ReturnAndRecordsAffectedViaOut (interlocked)
	private static global::System.Data.Common.DbCommand? s___dapper__command_Samples_Sync_Parameters_input_cs_ReturnAndRecordsAffectedViaOut_16;

	[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
	[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
	public partial void ReturnAndRecordsAffectedViaOut(global::System.Data.Common.DbConnection connection, int id, string inputOutput, global::System.DateTime output, int count, int recordsAffected)
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
			if ((__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Parameters_input_cs_ReturnAndRecordsAffectedViaOut_16, null)) is null)
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
			__dapper__args[1].Value = global::Dapper.Internal.InternalUtilities.AsValue(inputOutput);
			__dapper__args[2].Value = global::Dapper.Internal.InternalUtilities.AsValue(output);
			__dapper__args[3].Value = global::Dapper.Internal.InternalUtilities.AsValue(count);
			__dapper__args[4].Value = global::Dapper.Internal.InternalUtilities.AsValue(recordsAffected);
#pragma warning restore CS0618

			// execute non-query
			__dapper__command.ExecuteNonQuery();

			// TODO: post-process parameters

		}
		finally
		{
			// cleanup
			if (__dapper__command is not null)
			{
				__dapper__command.Connection = default;
				__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Parameters_input_cs_ReturnAndRecordsAffectedViaOut_16, __dapper__command);
				__dapper__command?.Dispose();
			}
			if (connection is not null)
			{
				if (__dapper__close) connection.Close();
			}
		}

		// command factory for ReturnAndRecordsAffectedViaOut
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
			p.ParameterName = @"inputOutput";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.String;
			p.Size = -1;
			args.Add(p);

			p = command.CreateParameter();
			p.ParameterName = @"output";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.DateTime;
			args.Add(p);

			p = command.CreateParameter();
			p.ParameterName = @"count";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.Int32;
			args.Add(p);

			p = command.CreateParameter();
			p.ParameterName = @"recordsAffected";
			p.Direction = global::System.Data.ParameterDirection.Input;
			p.DbType = global::System.Data.DbType.Int32;
			args.Add(p);

			return command;
		}
	}


	// available inactive command for FineControl (interlocked)
	private static global::System.Data.Common.DbCommand? s___dapper__command_Samples_Sync_Parameters_input_cs_FineControl_21;

	[global::System.Diagnostics.DebuggerNonUserCodeAttribute]
	[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]
	public partial void FineControl(global::System.Data.Common.DbConnection connection, decimal value)
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
			if ((__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Parameters_input_cs_FineControl_21, null)) is null)
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
			__dapper__command.ExecuteNonQuery();

			// TODO: post-process parameters

		}
		finally
		{
			// cleanup
			if (__dapper__command is not null)
			{
				__dapper__command.Connection = default;
				__dapper__command = global::System.Threading.Interlocked.Exchange(ref s___dapper__command_Samples_Sync_Parameters_input_cs_FineControl_21, __dapper__command);
				__dapper__command?.Dispose();
			}
			if (connection is not null)
			{
				if (__dapper__close) connection.Close();
			}
		}

		// command factory for FineControl
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
