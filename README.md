Hello Avro
=========

A sample app showing how to use the awesome Apache Avro serializer as well as the Avro RPC in action. The Avro RPC uses the Avro Serializer underneath but also allows quick and easy client side execution of server methods i.e. RPC. The RPC parameters are passed over the network as Avro serialized objects.

To run this sample application

1. Download the git repository (duh!)
2. Open the solution (.sln) in visual studio 
3. Hit F5

## Key items ##

1. Avro data structure(s) are defined in `Avro\IDL\*.avdl` files
2. Running `code-gen.bat` compiles them into the C# class files (the code-gen tools use Java, so just for this stage, you need the JRE or JDK)
3. The main logic is in `SerializerAvro.cs`

Enjoy!