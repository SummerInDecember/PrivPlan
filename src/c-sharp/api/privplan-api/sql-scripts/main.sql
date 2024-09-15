CREATE DATABASE IF NOT EXISTS privplanDB;
USE privplanDB;
CREATE TABLE IF NOT EXISTS AllowedDevices(id INT NOT NULL); /* TODO: Add more columns*/
CREATE TABLE IF NOT EXISTS ServerOnlySecure(
    passwd VARCHAR(50) NOT NULL
);  
