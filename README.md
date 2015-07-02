# Issues

## Important

### During developing

* How to set autoincrement for primary key (Id columm)

### During running 

* During read from DB exception occurs (EF SQLServer): 
	```
    An unhandled exception occurred while processing the request.
    InvalidCastException: Cannot cast from source type to destination type.
	```

* During writing data to DB exception occurs (EF SQLServer):
	```
	[Microsoft.Data.Entity.DbContext] An exception occurred in the data store while saving changes.
		Microsoft.Data.Entity.Update.DbUpdateException: An error occurred while updating the entries. See the inner exception for details. ---> System.NotSupportedException: Unknown Type : datetime2
  		at Mono.Data.Tds.TdsMetaParameter.GetMetaType () [0x00000] in <filename unknown>:0 
  		at Mono.Data.Tds.Protocol.Tds70.ExecRPC (TdsRpcProcId rpcId, System.String sql, Mono.Data.Tds.TdsMetaParameterCollection parameters, Int32 timeout, Boolean wantResults) [0x00000] in <filename unknown>:0 
  		at Mono.Data.Tds.Protocol.Tds80.Execute (System.String commandText, Mono.Data.Tds.TdsMetaParameterCollection parameters, Int32 timeout, Boolean wantResults) [0x00000] in <filename unknown>:0 
  		at System.Data.SqlClient.SqlCommand.Execute (Boolean wantResults) [0x00000] in <filename unknown>:0 
  		at System.Data.SqlClient.SqlCommand.ExecuteReader (CommandBehavior behavior) [0x00000] in <filename unknown>:0 
  		--- End of inner exception stack trace ---
  		at Microsoft.Data.Entity.Relational.Update.ReaderModificationCommandBatch.Execute (Microsoft.Data.Entity.Relational.RelationalTransaction transaction, IRelationalTypeMapper typeMapper, Microsoft.Data.Entity.DbContext context, ILogger logger) [0x00000] in <filename unknown>:0 
  		at Microsoft.Data.Entity.Relational.Update.BatchExecutor.Execute (IEnumerable`1 commandBatches, IRelationalConnection connection) [0x00000] in <filename unknown>:0 
  		at Microsoft.Data.Entity.Relational.RelationalDataStore.SaveChanges (IReadOnlyList`1 entries) [0x00000] in <filename unknown>:0 
  		at Microsoft.Data.Entity.ChangeTracking.Internal.StateManager.SaveChanges (IReadOnlyList`1 entriesToSave) [0x00000] in <filename unknown>:0 
  		at Microsoft.Data.Entity.ChangeTracking.Internal.StateManager.SaveChanges (Boolean acceptAllChangesOnSuccess) [0x00000] in <filename unknown>:0
	``` 

## Not important

* How to change default database schema (dbo)
* How to set up DatabaseContext without connection string in OnConfiguring method. Without this method DB migration throw exception.