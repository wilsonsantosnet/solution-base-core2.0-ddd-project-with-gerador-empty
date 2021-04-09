sqlcmd -S "(localdb)\mssqllocaldb" -i createdb-sample-seed.sql
sqlcmd -S "(localdb)\mssqllocaldb" -i Sample.Seed.sql
