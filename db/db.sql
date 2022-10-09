-- Database generated with pgModeler (PostgreSQL Database Modeler).
-- pgModeler version: 0.9.4
-- PostgreSQL version: 13.0
-- Project Site: pgmodeler.io
-- Model Author: ---

-- Database creation must be performed outside a multi lined SQL file. 
-- These commands were put in this file only as a convenience.
-- 
-- object: monq | type: DATABASE --
-- DROP DATABASE IF EXISTS monq;
CREATE DATABASE monq
	ENCODING = 'UTF8'
	LC_COLLATE = 'Russian_Russia.1251'
	LC_CTYPE = 'Russian_Russia.1251'
	TABLESPACE = pg_default
	OWNER = postgres;
-- ddl-end --


-- object: main | type: SCHEMA --
-- DROP SCHEMA IF EXISTS main CASCADE;
CREATE SCHEMA main;
-- ddl-end --
ALTER SCHEMA main OWNER TO postgres;
-- ddl-end --

SET search_path TO pg_catalog,public,main;
-- ddl-end --

-- object: main.mail | type: TABLE --
-- DROP TABLE IF EXISTS main.mail CASCADE;
CREATE TABLE main.mail (
	id bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT BY 1 MINVALUE 1 MAXVALUE 9223372036854775807 START WITH 1 CACHE 1 ),
	creation_date timestamp with time zone NOT NULL DEFAULT now(),
	subject character varying,
	body character varying,
	recipients character varying[],
	result character varying,
	failed_message character varying,
	CONSTRAINT mail_pk PRIMARY KEY (id)
);
-- ddl-end --
ALTER TABLE main.mail OWNER TO postgres;
-- ddl-end --

-- -- object: main.mail_id_seq | type: SEQUENCE --
-- -- DROP SEQUENCE IF EXISTS main.mail_id_seq CASCADE;
-- CREATE SEQUENCE main.mail_id_seq
-- 	INCREMENT BY 1
-- 	MINVALUE 1
-- 	MAXVALUE 9223372036854775807
-- 	START WITH 1
-- 	CACHE 1
-- 	NO CYCLE
-- 	OWNED BY NONE;
-- 
-- -- ddl-end --
-- ALTER SEQUENCE main.mail_id_seq OWNER TO postgres;
-- -- ddl-end --
-- 

