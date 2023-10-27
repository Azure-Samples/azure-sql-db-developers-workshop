create database testingDB;


EXEC sp_configure 'show advanced option', '1'; RECONFIGURE; EXEC sp_configure 'clr strict security', 0; RECONFIGURE; EXEC sp_configure 'clr enabled', 1; RECONFIGURE;