CREATE DATABASE IF NOT EXISTS privplanDB;
USE privplanDB;
CREATE TABLE IF NOT EXISTS AllowedAccounts(id INT NOT NULL UNIQUE); /* TODO: Add more columns*/
CREATE TABLE IF NOT EXISTS ServerOnlySecure(
    id INT NOT NULL UNIQUE,
    passwd VARCHAR(50) NOT NULL
);  
