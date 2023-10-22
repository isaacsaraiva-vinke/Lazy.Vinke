
# Lazy.Vinke.Json
.Net Json Serializer & Deserializer<br>
<br>
- ### Built-in Serializers\Deserializers
  - Type (Based on .Net Assemblies and Namespaces)
  - DataSet (Typed data table collection)
  - DataTable (Typed columns preserving row state and values)
  - DateTime (Customizable format [default: yyyy-MM-ddTHH:mm:ss:fffZ])
  - Object (Wrap\Unwrap properties declared as "Object" by exporting its instance type)
  - Generic Dictionary\<T\>
  - Generic Tuple\<T\>
  - Generic List\<T\>
  <br>
- ### Supported features
  - ### Globally define Serializer\Deserializer for types just once
    LazyJsonSerializerOptions jsonSerializerOptions = new LazyJsonSerializerOptions();<br>
    jsonSerializerOptions.Item\<LazyJsonSerializerOptionsGlobal\>().Add\<YourTypeSerializer\>(typeof(YourType));<br>
    <br>
    LazyJsonDeserializerOptions jsonDeserializerOptions = new LazyJsonDeserializerOptions();<br>
    jsonDeserializerOptions.Item\<LazyJsonDeserializerOptionsGlobal\>().Add\<YourTypeDeserializer\>(typeof(YourType));<br>
    <br>
  - ### Support easily property ignore by adding attribute to property
    [LazyJsonAttributePropertyIgnore()]<br>
    public Int32 Internalid { get; set; }<br>
    <br>
  - ### Support easily property renaming by adding attribute to property
    [LazyJsonAttributePropertyRename("NewName")]<br>
    public Decimal Amount { get; set; }<br>
    <br>
  - ### Support specific Serializers\Deserializers by adding attribute to property
    [LazyJsonAttributeTypeSerializer(typeof(YourTypeSerializer))]<br>
    [LazyJsonAttributeTypeDeserializer(typeof(YourTypeDeserializer))]<br>
    public YourType YourProperty { get; set; }<br>
  <br>
- ### Usage
  - ### Reader
    LazyJson lazyJson = LazyJsonReader.Read(yourJson);<br>
    <br>
  - ### Writer
    String yourJson = LazyJsonWriter.Write(lazyJson);<br>
    <br>
  - ### Serialization
    String yourJson = LazyJsonSerializer.Serialize(yourObject);<br>
    LazyJsonToken jsonToken = LazyJsonSerializer.SerializeToken(yourObject);<br>
    <br>
  - ### Deserialization
    YourObject yourObject = LazyJsonDeserializer\<YourObject\>.Deserialize(yourJson);<br>
    YourObject yourObject = LazyJsonDeserializer\<YourObject\>.DeserializeToken(jsonToken);<br>
    <br>
