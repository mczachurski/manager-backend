# Issues

## Important

### During developing
* Can't download EntityFramework 7.0.0-beta5 (SunLine.Manager.Repository => dnu restore)
* Can't run: dnx . ef migration add InitializeMigration, exception occurs:
	```
    System.InvalidOperationException: Failed to resolve the following dependencies for target framework 'DNX,Version=v4.5.1':
      EntityFramework 7.0.0-beta5

    Searched Locations:
      /Volumes/Dane/Users/marcin/Projekty/Manager/Backend/{name}/project.json
      /Volumes/Dane/Users/marcin/.dnx/packages/{name}/{version}/{name}.nuspec
      /usr/local/Cellar/mono/4.0.1.44/lib/mono/4.5/{name}.dll
      /usr/local/Cellar/mono/4.0.1.44/lib/mono/4.5/Facades/{name}.dll

    Try running 'dnu restore'.

      at Microsoft.Framework.ApplicationHost.Program.Main (System.String[] args) [0x00000] in <filename unknown>:0 
      --- End of stack trace from previous location where exception was thrown ---
      at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw () [0x00000] in <filename unknown>:0 
      at Microsoft.Framework.Runtime.Common.EntryPointExecutor.Execute (System.Reflection.Assembly assembly, System.String[] args, IServiceProvider serviceProvider) [0x00000] in <filename unknown>:0 
      at dnx.host.Bootstrapper.RunAsync (System.Collections.Generic.List`1 args, IRuntimeEnvironment env) [0x00000] in <filename unknown>:0
	  ```
* How to set autoincrement for primary key (Id columm)

### During running 
* During read from DB exception occurs: 
  ```
    InvalidCastException: Cannot cast from source type to destination type.
    Microsoft.Data.Entity.Relational.RelationalObjectArrayValueReader.ReadValue[DateTime] (Int32 index) [0x00000] in <filename unknown>, line 0
	```

## Not important

* How to change default database schema (dbo)
* How to set up DatabaseContext without connection string in OnConfiguring method. Without this method DB migration throw exception.