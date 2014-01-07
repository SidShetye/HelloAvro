@echo off

if [%1]==[] (
	REM Manually called from command line, do nothing special ...
	) else (
	REM Being called from Visual studio, so change directory to project root
	REM To run this batch file everytime visual studio builds this project
	REM right click HelloAvro project => Properties => Go to "Build Events"
	REM then to pre-build and enter the following (without quotes)
	REM "$(ProjectDir)code-gen.bat $(ProjectDir)" 
	cd "%1"
)

@echo Cleaning existing auto-gen'd files
del /f /q Avro\Schema
del /f /q Avro\DTO

@echo Phase 1: Auto generating Avro schema from Avro IDL ...
@echo NOTE: You need java to run this step. Download and install if you don't
@echo       have it. You only need the runtime (JRE) not the entire SDK (JDK)
java -jar Avro\Tools\java\avro-tools-1.7.5.jar idl2schemata Avro\IDL\EmployeeDTO.avdl Avro\Schema

@echo Phase 2: Auto generating C# classes from Avro schema ...
Avro\Tools\codegen\Release\avrogen.exe -s Avro\Schema\EmployeeDTO.avsc ..
@echo Avro auto-gen phase done.