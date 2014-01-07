This contains the Avro tools needed for the code-gen phase than happens before your C# project gets compiled. 

Why do we need tools?
--
First off, relax. The tools are just a few files, nothing complicated. And if you download the GitHub code, you already have everything. Ok, so basically in Avro you need to

1. Express your data structure/"classes" in an IDL format. This is very similar to writing classes in C# or Java or other high level languages.
2. Compile the IDL => Schema via the **Avro IDL compiler**. This compiler tool is itself written in Java but it doesn't change a thing for your C# project
3. Compile the Schema => C# classes via the **Avro schema compiler**
4. Use these resulting C# `auto-gen`d classes in your C# project

As you can see, you need the tools in step 2 and 3.

Note: While it's possible to write the Avro schema directly "by hand", this is NOT recommended. It's much easier and less error prone to use the IDL compiler to do that for you.        

Updating your tools
--

1. IDL compiler 
 1. Download the Avro Java Tools (yes, the Java tools, don't panic) from [http://psg.mtu.edu/pub/apache/avro/stable/java/avro-tools-1.7.5.jar](http://psg.mtu.edu/pub/apache/avro/stable/java/avro-tools-1.7.5.jar). If that mirror is down, try another from [http://www.apache.org/dyn/closer.cgi/avro/](http://www.apache.org/dyn/closer.cgi/avro/)
 2. Save/Copy that `.jar` file into the `java` folder so you have `Avro\Tools\java\avro-tools-1.7.5.jar`

2. Schema compiler
 1. Download the Avro C# release from [http://psg.mtu.edu/pub/apache/avro/stable/avro-csharp-1.7.5.tar.gz](http://psg.mtu.edu/pub/apache/avro/stable/avro-csharp-1.7.5.tar.gz). If that mirror is down, try another from [http://www.apache.org/dyn/closer.cgi/avro/](http://www.apache.org/dyn/closer.cgi/avro/)
 2. Extract that tar.gz file
 3. Copy the `codegen\Release\` folder into this folder so you have `Avro\Tools\codegen\Release\*.*`